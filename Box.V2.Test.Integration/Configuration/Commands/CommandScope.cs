namespace Box.V2.Test.Integration.Configuration.Commands
{
    /// <summary>
    /// Defines in which scope command will call Dispose method. Possible values are Test, Class and Assembly.
    /// Test - Dispose will be called when the test is completed.
    /// Class - Dispose will be called after all tests in the class have completed.
    /// Assembly - Dispose will be called after all tests in the project have completed.
    /// </summary>
    public enum CommandScope
    {
        Test,
        Class,
        Assembly
    }
}
