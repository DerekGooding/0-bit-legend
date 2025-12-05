using System.Runtime.InteropServices;

namespace _0_Bit_Legend;

public partial class InputController
{
    // Win32 API to check key state
    [LibraryImport("user32.dll")]
    private static partial short GetAsyncKeyState(int vKey);

    // WASD keys
    const int VK_W = 0x57;
    const int VK_A = 0x41;
    const int VK_S = 0x53;
    const int VK_D = 0x44;
    const int VK_P = 0x50;

    // Arrow keys
    const int VK_UP = 0x26;
    const int VK_LEFT = 0x25;
    const int VK_DOWN = 0x28;
    const int VK_RIGHT = 0x27;

    // Attack keys
    const int VK_LSHIFT = 0xA0;
    const int VK_RSHIFT = 0xA1;
    const int VK_SPACE = 0x20;

    private readonly Dictionary<int, InputType> _mapping = new()
    {
        { VK_W, InputType.Up },     { VK_UP, InputType.Up },
        { VK_A, InputType.Left },   { VK_LEFT, InputType.Left },
        { VK_S, InputType.Down },   { VK_DOWN, InputType.Down },
        { VK_D, InputType.Right },  { VK_RIGHT, InputType.Right },

        { VK_LSHIFT, InputType.Attack },
        { VK_RSHIFT, InputType.Attack },
        { VK_SPACE, InputType.Attack  },

        { VK_P, InputType.DebugWall },
    };

    public InputType GetInputState()
    {
        var result = InputType.None;

        foreach (var pair in _mapping)
        {
            if ((GetAsyncKeyState(pair.Key) & 0x8000) != 0)
                result |= pair.Value;
        }

        return result;
    }
}
