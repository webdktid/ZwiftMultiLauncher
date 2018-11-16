using System.Configuration;

namespace ZwiftLauncher
{
    public class ConfigSettings
    {

        public static int Number => GetAppsettingValueInt("number");
        public static bool Resize => GetAppsettingValueBool("resize");
        public static int AutoClose => GetAppsettingValueInt("autoclose");
        public static string Resolution => GetAppsettingValueString("resolution");
        public static string UserFolder => GetAppsettingValueString("usersfolder");
        public static string ZwiftLocation => GetAppsettingValueString("zwiftlocation");

        private static string GetAppsettingValueString(string key)
        {
            AppSettingsReader reader = new AppSettingsReader();
            return (string)reader.GetValue(key, typeof(string));
        }

        private static int GetAppsettingValueInt(string key)
        {
            AppSettingsReader reader = new AppSettingsReader();
            return (int)reader.GetValue(key, typeof(int));
        }

        private static bool GetAppsettingValueBool(string key)
        {
            AppSettingsReader reader = new AppSettingsReader();
            return (bool)reader.GetValue(key, typeof(bool));
        }

    }
}
