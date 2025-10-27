# Configuration

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Configuration](#configuration)
  - [Max retry attempts](#max-retry-attempts)
  - [Custom retry strategy](#custom-retry-strategy)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Max retry attempts

The default maximum number of retries in case of failed API call is 5.
To change this number you should initialize `BoxRetryStrategy` with the new value and pass it to `NetworkSession`.

```c#
BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth("DEVELOPER_TOKEN");
NetworkSession networkSession = new NetworkSession() { RetryStrategy = new BoxRetryStrategy(5) };
BoxClient client = new BoxClient(auth: auth, networkSession: networkSession);
```

## Custom retry strategy

You can also implement your own retry strategy by subclassing `RetryStrategy` and overriding `ShouldRetryAsync` and `RetryAfter` methods.
This example shows how to set custom strategy that retries on 5xx status codes and waits 1 second between retries.

```c#
public class CustomRetryStrategy : IRetryStrategy
{
    public Task<bool> ShouldRetryAsync(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber)
    {
        return Task.FromResult(fetchResponse.Status >= 500);
    }

    public double RetryAfter(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber)
    {
        return 1.0;
    }
}

BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth("DEVELOPER_TOKEN");
NetworkSession networkSession = new NetworkSession() { RetryStrategy = new CustomRetryStrategy() };
BoxClient client = new BoxClient(auth: auth, networkSession: networkSession);
```

As you can see, in this example we based our decision to retry solely on the status code of the response.
However, you can use any information available in `fetchOptions` and `fetchResponse` to make a more informed decision.
