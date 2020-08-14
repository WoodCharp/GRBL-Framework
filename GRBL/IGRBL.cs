namespace GRBL
{
    public interface IGRBL
    {
        GRBLManager GRBLFramework { get; }
    }

    public enum eAxis
    {
        X,
        Y,
        Z
    }

    /// <summary>
    /// Machine states
    /// </summary>
    public enum eMachineState
    {
        Idle,
        Run,
        Hold_0,
        Hold_1,
        Jog,
        Alarm,
        Door_0,
        Door_1,
        Door_2,
        Door_3,
        Check,
        Home,
        Sleep,
        Unknown,
        Locked
    }

    /// <summary>
    /// Work coordinate system spaces
    /// </summary>
    public enum eP
    {
        P1,
        P2,
        P3,
        P4,
        P5,
        P6
    }

    public enum ePositioning
    {
        Absolute,
        Incremental
    }
}
