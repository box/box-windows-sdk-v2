
using System;

namespace Box.Sdk.Gen.Internal
{
  internal class Result<T>
  {
    public T? Value { get; }
    public bool IsSuccess { get; }
    public Exception? Exception { get; }
    public bool IsRetryable { get; }

    private Result(bool isSuccess, T? value = default, Exception? exception = default, bool isRetryable = true)
    {
      IsSuccess = isSuccess;
      Value = value;
      Exception = exception;
      IsRetryable = isRetryable;
    }

    internal static Result<T> Ok(T value)
    {
      return new Result<T>(true, value);
    }

    internal static Result<T> Fail(Exception ex, bool isRetryable = true)
    {
      return new Result<T>(false, default(T), ex, isRetryable);
    }
  }
}
