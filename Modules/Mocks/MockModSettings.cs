namespace BasicItemSync.Modules.Mocks;


internal interface IModSettings
{
    public bool InstanceDebugPlayerData { get; }
    public bool InstanceDebugLogs { get; }
}

internal class MockModSettings : IModSettings
{
    public bool InstanceDebugPlayerData => false;
    public bool InstanceDebugLogs => true;
}