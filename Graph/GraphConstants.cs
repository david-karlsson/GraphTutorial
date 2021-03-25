namespace GraphTutorial
{
    public static class GraphConstants
    {
        // Defines the permission scopes used by the app
        public readonly static string[] Scopes =
        {
            "User.ReadWrite",
             "User.ReadWrite.All",
            "MailboxSettings.Read",
            "Calendars.ReadWrite",
            "Mail.ReadWrite",
            "Directory.ReadWrite.All",
            "Files.ReadWrite",
            // "Directory.AccessAsUser.All"

        };
    }
}