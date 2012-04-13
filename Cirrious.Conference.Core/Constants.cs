using System;
using System.Net;

namespace Cirrious.Conference.Core
{
    public static class Constants
    {
        public static readonly TimeSpan MaxTimeBetweenUpdates = TimeSpan.FromDays(1); 
        public const string SettingsFileName = "Settings.txt";
        public const string TempSessionsFileName = "Temp.Sessions.txt";
        public const string SessionsFileName = "Sessions.txt";
        public const string FavoritesFileName = "Favorites.txt";
        public const string RootFolderForResources = "ConfResources/Text";
        public const string GeneralNamespace = "Conference";
        public const string Shared = "Shared";
        public const string SessionDataEndPoint = "http://www.pocketddd.com/Pocket/GetViewModel/5";
    }
}
