# Configuration

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Retry Strategy](#retry-strategy)
  - [Overview](#overview)
  - [Default Configuration](#default-configuration)
  - [Retry Decision Flow](#retry-decision-flow)
  - [Exponential Backoff Algorithm](#exponential-backoff-algorithm)
    - [Example Delays (with default settings)](#example-delays-with-default-settings)
  - [Retry-After Header](#retry-after-header)
  - [Network Exception Handling](#network-exception-handling)
  - [Customizing Retry Parameters](#customizing-retry-parameters)
  - [Custom Retry Strategy](#custom-retry-strategy)
- [Timeouts](#timeouts)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Retry Strategy

### Overview

The SDK ships with a built-in retry strategy (`BoxRetryStrategy`) that implements the `IRetryStrategy` interface. The `BoxNetworkClient`, which serves as the default network client, uses this strategy to automatically retry failed API requests with exponential backoff.

The retry strategy exposes two methods:

- **`ShouldRetryAsync`** — Determines whether a failed request should be retried based on the HTTP status code, response headers, attempt count, and authentication state.
- **`RetryAfter`** — Computes the delay (in seconds) before the next retry attempt, using either the server-provided `Retry-After` header or an exponential backoff formula.

### Default Configuration

| Parameter                  | Default      | Description                                                                                                                                              |
| -------------------------- | ------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `MaxAttempts`              | `5`          | Maximum number of retry attempts for HTTP error responses (status 4xx/5xx).                                                                              |
| `RetryBaseInterval`        | `1` (second) | Base interval used in the exponential backoff calculation.                                                                                               |
| `RetryRandomizationFactor` | `0.5`        | Jitter factor applied to the backoff delay. The actual delay is multiplied by a random value between `1 - factor` and `1 + factor`.                      |
| `MaxRetriesOnException`    | `2`          | Maximum number of retries for network-level exceptions (connection failures, timeouts). These are tracked by a separate counter from HTTP error retries. |

### Retry Decision Flow

The following diagram shows how `BoxRetryStrategy.ShouldRetryAsync` decides whether to retry a request:

```
                    ShouldRetryAsync(fetchOptions, fetchResponse, attemptNumber)
                                        |
                                        v
                             +-----------------------+
                             | Status == 0           |     Yes
                             | (network exception)?  |----------> attemptNumber <= MaxRetriesOnException?
                             +-----------------------+               |            |
                                        | No                        Yes          No
                                        v                            |            |
                             +-----------------------+           [RETRY]      [NO RETRY]
                             | attemptNumber >=      |
                             | MaxAttempts?          |
                             +-----------------------+
                                  |            |
                                 Yes          No
                                  |            |
                             [NO RETRY]        v
                             +-----------------------+
                             | Status == 202 AND     |     Yes
                             | Retry-After header?   |----------> [RETRY]
                             +-----------------------+
                                        | No
                                        v
                             +-----------------------+
                             | Status >= 500         |     Yes
                             | (server error)?       |----------> [RETRY]
                             +-----------------------+
                                        | No
                                        v
                             +-----------------------+
                             | Status == 429         |     Yes
                             | (rate limited)?       |----------> [RETRY]
                             +-----------------------+
                                        | No
                                        v
                             +-----------------------+
                             | Status == 401 AND     |     Yes
                             | auth available?       |----------> Refresh token, then [RETRY]
                             +-----------------------+
                                        | No
                                        v
                                   [NO RETRY]
```

### Exponential Backoff Algorithm

When the response does not include a `Retry-After` header, the retry delay is computed using exponential backoff with randomized jitter:

```
delay = 2^attemptNumber * RetryBaseInterval * random(1 - factor, 1 + factor)
```

Where:

- `attemptNumber` is the current attempt (1-based)
- `RetryBaseInterval` defaults to `1` second
- `factor` is `RetryRandomizationFactor` (default `0.5`)
- `random(min, max)` returns a uniformly distributed value in `[min, max]`

#### Example Delays (with default settings)

| Attempt | Base Delay | Min Delay (factor=0.5) | Max Delay (factor=0.5) |
| ------- | ---------- | ---------------------- | ---------------------- |
| 1       | 2s         | 1.0s                   | 3.0s                   |
| 2       | 4s         | 2.0s                   | 6.0s                   |
| 3       | 8s         | 4.0s                   | 12.0s                  |
| 4       | 16s        | 8.0s                   | 24.0s                  |

### Retry-After Header

When the server includes a `Retry-After` header in the response, the SDK uses the header value directly as the delay in seconds instead of computing an exponential backoff delay. This applies to any retryable response that includes the header, including:

- `202 Accepted` with `Retry-After` (long-running operations)
- `429 Too Many Requests` with `Retry-After`
- `5xx` server errors with `Retry-After`

The header value is parsed as a floating-point number representing seconds.

### Network Exception Handling

Network-level failures (connection refused, DNS resolution errors, timeouts, TLS errors) are represented internally as responses with status `0`. These exceptions are tracked by a **separate counter** (`MaxRetriesOnException`, default `2`) from the regular HTTP error retry counter (`MaxAttempts`).

This means:

- Network exception retries are tracked independently from HTTP error retries, each with their own counter and backoff progression.
- A request can fail up to `MaxRetriesOnException` times due to network exceptions, but each exception retry also increments the overall attempt counter, so the total number of retries across both exception and HTTP error types is bounded by `MaxAttempts`.

### Customizing Retry Parameters

You can customize all retry parameters by initializing `BoxRetryStrategy` with the desired values and passing it to `NetworkSession`:

```c#
BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth("DEVELOPER_TOKEN");
NetworkSession networkSession = new NetworkSession()
{
    RetryStrategy = new BoxRetryStrategy(
        maxAttempts: 3,
        retryBaseInterval: 2,
        retryRandomizationFactor: 0.3,
        maxRetriesOnException: 1
    )
};
BoxClient client = new BoxClient(auth: auth, networkSession: networkSession);
```

### Custom Retry Strategy

You can implement your own retry strategy by implementing the `IRetryStrategy` interface and providing `ShouldRetryAsync` and `RetryAfter` methods:

```c#
public class CustomRetryStrategy : IRetryStrategy
{
    public Task<bool> ShouldRetryAsync(
        FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber)
    {
        return Task.FromResult(fetchResponse.Status >= 500 && attemptNumber < 3);
    }

    public double RetryAfter(
        FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber)
    {
        return 1.0;
    }
}

BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth("DEVELOPER_TOKEN");
NetworkSession networkSession = new NetworkSession()
{
    RetryStrategy = new CustomRetryStrategy()
};
BoxClient client = new BoxClient(auth: auth, networkSession: networkSession);
```

## Timeouts

You can configure request timeout with `TimeoutConfig` on `NetworkSession`.
`TimeoutMs` is in milliseconds and applies to each HTTP request attempt.

```c#
BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth("DEVELOPER_TOKEN");
NetworkSession networkSession = new NetworkSession()
{
    TimeoutConfig = new TimeoutConfig(timeoutMs: 30000)
};
BoxClient client = new BoxClient(auth: auth, networkSession: networkSession);
```

How timeout handling works:

- If `TimeoutConfig` is not provided (or `TimeoutMs` is `null`), the SDK uses the default timeout of `100000` ms (100 seconds).
- To disable the SDK HTTP request timeout, set `TimeoutMs` to `0` or a negative value.
- Timeout failures are handled as request exceptions, then retry behavior is controlled by the configured retry strategy.
- If all retry attempts are exhausted after HTTP request timeout errors, the SDK throws a timeout-related `BoxSdkException`.
- Timeout applies to a single HTTP request attempt to the Box API (not the total time across all retries).
