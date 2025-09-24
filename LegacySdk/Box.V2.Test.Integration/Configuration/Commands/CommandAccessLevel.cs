namespace Box.V2.Test.Integration.Configuration.Commands
{
    /// <summary>
    /// Defines with which BoxClient command will be executed. Possible values are User and Admin.
    /// User - BoxClient with "user" scope (Managed User)
    /// Admin - BoxClient with "enterprise" scope (ServiceAccount).
    /// </summary>
    public enum CommandAccessLevel
    {
        User,
        Admin
    }
}
