
namespace KidsTodo.Common.Signals
{
    /// <summary>
    /// Login signal.
    /// </summary>
    class LoginSignal : ASignal<string, string> { }

    /// <summary>
    /// Register signal.
    /// </summary>
    class RegisterSignal : ASignal<string, int, int> { }
}

