
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

public static class CustomAsserts
{
    public static async System.Threading.Tasks.Task IsExceptionAsync(this Assert assert, Func<System.Threading.Tasks.Task> action)
    {
        try
        {
            await action();
        }
        catch
        {
            return;
        }

        throw new AssertFailedException($"Expected exception to be thrown.");
    }
}
