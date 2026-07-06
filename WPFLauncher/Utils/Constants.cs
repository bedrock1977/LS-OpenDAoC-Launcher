namespace WPFLauncher
{
    internal class Constants
    {
        public const string LauncherVersion = "1.0.0";

        #region Patcher

        public const string LauncherUpdaterName = "LastStandLauncher_Update.exe";
        // public const string AppData = "D:\\Program Files (x86)"; // @"%AppData%\" "D:\\Program Files (x86)"
        // public const string UserPath = "\\Electronic Arts\\LastStand\\user.dat";
        public const string UserPath = "D:\\Program Files (x86)\\Electronic Arts\\LastStand\\user.dat";

        public const string
            RemoteVersionUrl = "https://patch.laststand.net/version-new.txt";
        public const string
            RemoteFileList = "https://patch.laststand.net/patchlist-new.txt";
        
        public static string RemoteFilePath;

        #endregion

        #region gameserver

        public const string LiveIP = "livelaststand.ddns.net";
        public const string PtrIP = "ptr.livelaststand.ddns.net";
        public const string QueueApiIP = "https://queue.laststand.net";

        #endregion

        #region Player Urls

        public const string RegisterUrl = "https://atlasl.ink/register";
        public const string LinkUrl = "https://atlasl.ink/link-discord";
        public const string PatchNotesUrl = "https://atlasl.ink/patch-notes";
        
        #endregion
        
        #region Messages

        public const string MessageDownloadError = "Error downloading files. Please try again later.";
        public const string MessageInvalidCredentials = "An account with these credentials could not be found. Invalid account name or password.";
        public const string MessageNotInQueue = "Your account is not in the queue. Please re-open the launcher!";
        public const string MessageQueueError = "Error communicating with Queue Service. Please try again later. If this continues to occur please contact Last Stand staff.";
        public const string MessageNoCredentials = "Please enter your account and password.";
        
        public const string DiscordMessage = "Linking the account to Discord is now required to play on Last Stand. Would you like to do this now?";
        public const string DiscordCaption = "Game account not linked to Discord";
        public const string DiscordError = "You won't be able to play on Last Stand without linking your account to Discord";

        public const string MessageReviewInstallation =
            "There was an error launching the game; please review your installation.";

        #endregion
    }
}