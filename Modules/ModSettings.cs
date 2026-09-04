using BasicItemSync.Modules.Mocks;
using BepInEx.Configuration;

namespace BasicItemSync.Modules
{
    internal class ModSettings : IModSettings
    {
        public bool InstanceDebugPlayerData => DebugPlayerData;
        public static bool DebugPlayerData => _debugPlayerData?.Value ?? false;
        static ConfigEntry<bool> _debugPlayerData;

        public bool InstanceDebugLogs => DebugLogs;
        public static bool DebugLogs => _debugLogs?.Value ?? false;
        static ConfigEntry<bool> _debugLogs;

        public static void Init(ConfigFile config)
        {
            _debugLogs = config.Bind("Debug", "Enable debug logs", true);
            _debugPlayerData = config.Bind("Debug", "Enable PlayerData logs", false);
        }
    }
}
