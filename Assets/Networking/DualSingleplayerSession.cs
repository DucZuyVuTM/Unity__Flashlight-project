public static class DualSingleplayerSession
{
    public static bool IsActive { get; private set; }

    public static void Enable()
    {
        IsActive = true;
    }

    public static void Disable()
    {
        IsActive = false;
    }
}
