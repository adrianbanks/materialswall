using System.Configuration;

namespace Granta.MaterialsWall
{
    public interface IAppSettingsProvider
    {
        string GetSetting(string name);
        int GetIntegerSetting(string name, int defaultValue);
    }

    public sealed class AppSettingsProvider : IAppSettingsProvider
    {
        public string GetSetting(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : ConfigurationManager.AppSettings[name];
        }

        public int GetIntegerSetting(string name, int defaultValue)
        {
            string setting = GetSetting(name);

            if (setting == null)
            {
                return defaultValue;
            }

            int value;
            return int.TryParse(setting, out value) ? value : defaultValue;
        }
    }
}
