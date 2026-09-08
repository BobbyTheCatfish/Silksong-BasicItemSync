using BasicItemSync.Modules.Mocks;
using BepInEx.Configuration;
using UnityEngine;

namespace BasicItemSync.Modules
{
    internal class ModSettings : IModSettings
    {
        public bool InstanceDebugPlayerData => DebugPlayerData;
        public static bool DebugPlayerData => _debugPlayerData?.Value ?? false;
        static ConfigEntry<bool>? _debugPlayerData;

        public static bool DebugPersistentData => _debugPersistentData?.Value ?? false;
        static ConfigEntry<bool>? _debugPersistentData;

        public bool InstanceDebugLogs => DebugLogs;
        public static bool DebugLogs => _debugLogs?.Value ?? false;
        static ConfigEntry<bool>? _debugLogs;

        public static SystemLanguage PreferredLanguage => _preferredLanguage?.Value ?? SystemLanguage.Unknown;
        static ConfigEntry<SystemLanguage>? _preferredLanguage;

        public static void Init(ConfigFile config)
        {
            _preferredLanguage = config.Bind("Language", "Preferred Language", SystemLanguage.Unknown);
            _debugLogs = config.Bind("Logs", "Debug Logs", true);
            _debugPlayerData = config.Bind("Logs", "PlayerData Logs", false);
            _debugPersistentData = config.Bind("Logs", "PersistentData Logs", false);
        }
    }
}
