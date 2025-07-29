
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RetryableTestAttribute : TestMethodAttribute
{
    public int MaxRetries { get; set; }

    public RetryableTestAttribute(int maxRetries = 3)
    {
        MaxRetries = maxRetries;
    }

    public override TestResult[] Execute(ITestMethod testMethod)
        {
            TestResult[] result = null!;
            for (int attempt = 1; attempt <= MaxRetries; attempt++)
            {
                result = base.Execute(testMethod);
                var failed = result[0].Outcome != UnitTestOutcome.Passed;

                if (!failed)
                {
                    if (attempt > 1)
                        Console.WriteLine($"Test passed on retry #{attempt}.");
                    return result;
                }
                Console.WriteLine($"Test failed on attempt #{attempt}.");
            }

            Console.WriteLine($"Test failed after {MaxRetries} attempts.");
            return result;
        }
}
