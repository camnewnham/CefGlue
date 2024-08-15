
using Eto.Forms;

namespace Xilium.CefGlue.EtoForms
{
    /// <summary>
    /// Performs key code convertions.
    /// </summary>
    internal static class KeyInterop
    {
        private const int VK_CANCEL = 0x03;

        private const int VK_BACK = 0x08;

        private const int VK_CLEAR = 0x0C;

        private const int VK_RETURN = 0x0D;

        private const int VK_PAUSE = 0x13;

        private const int VK_CAPITAL = 0x14;

        private const int VK_KANA = 0x15;

        private const int VK_HANGEUL = 0x15;

        private const int VK_HANGUL = 0x15;

        private const int VK_JUNJA = 0x17;

        private const int VK_FINAL = 0x18;

        private const int VK_HANJA = 0x19;

        private const int VK_KANJI = 0x19;

        private const int VK_ESCAPE = 0x1B;

        private const int VK_CONVERT = 0x1C;

        private const int VK_NONCONVERT = 0x1D;

        private const int VK_ACCEPT = 0x1E;

        private const int VK_MODECHANGE = 0x1F;

        private const int VK_SPACE = 0x20;

        private const int VK_PRIOR = 0x21;

        private const int VK_NEXT = 0x22;

        private const int VK_END = 0x23;

        private const int VK_HOME = 0x24;

        private const int VK_LEFT = 0x25;

        private const int VK_UP = 0x26;

        private const int VK_RIGHT = 0x27;

        private const int VK_DOWN = 0x28;

        private const int VK_SELECT = 0x29;

        private const int VK_PRINT = 0x2A;

        private const int VK_EXECUTE = 0x2B;

        private const int VK_SNAPSHOT = 0x2C;

        private const int VK_INSERT = 0x2D;

        private const int VK_DELETE = 0x2E;

        private const int VK_HELP = 0x2F;

        private const int VK_0 = 0x30;

        private const int VK_1 = 0x31;

        private const int VK_2 = 0x32;

        private const int VK_3 = 0x33;

        private const int VK_4 = 0x34;

        private const int VK_5 = 0x35;

        private const int VK_6 = 0x36;

        private const int VK_7 = 0x37;

        private const int VK_8 = 0x38;

        private const int VK_9 = 0x39;

        private const int VK_A = 0x41;

        private const int VK_B = 0x42;

        private const int VK_C = 0x43;

        private const int VK_D = 0x44;

        private const int VK_E = 0x45;

        private const int VK_F = 0x46;

        private const int VK_G = 0x47;

        private const int VK_H = 0x48;

        private const int VK_I = 0x49;

        private const int VK_J = 0x4A;

        private const int VK_K = 0x4B;

        private const int VK_L = 0x4C;

        private const int VK_M = 0x4D;

        private const int VK_N = 0x4E;

        private const int VK_O = 0x4F;

        private const int VK_P = 0x50;

        private const int VK_Q = 0x51;

        private const int VK_R = 0x52;

        private const int VK_S = 0x53;

        private const int VK_T = 0x54;

        private const int VK_U = 0x55;

        private const int VK_V = 0x56;

        private const int VK_W = 0x57;

        private const int VK_X = 0x58;

        private const int VK_Y = 0x59;

        private const int VK_Z = 0x5A;

        private const int VK_LWIN = 0x5B;

        private const int VK_RWIN = 0x5C;

        private const int VK_APPS = 0x5D;

        private const int VK_POWER = 0x5E;

        private const int VK_SLEEP = 0x5F;

        private const int VK_NUMPAD0 = 0x60;

        private const int VK_NUMPAD1 = 0x61;

        private const int VK_NUMPAD2 = 0x62;

        private const int VK_NUMPAD3 = 0x63;

        private const int VK_NUMPAD4 = 0x64;

        private const int VK_NUMPAD5 = 0x65;

        private const int VK_NUMPAD6 = 0x66;

        private const int VK_NUMPAD7 = 0x67;

        private const int VK_NUMPAD8 = 0x68;

        private const int VK_NUMPAD9 = 0x69;

        private const int VK_MULTIPLY = 0x6A;

        private const int VK_ADD = 0x6B;

        private const int VK_SEPARATOR = 0x6C;

        private const int VK_SUBTRACT = 0x6D;

        private const int VK_DECIMAL = 0x6E;

        private const int VK_DIVIDE = 0x6F;

        private const int VK_F1 = 0x70;

        private const int VK_F2 = 0x71;

        private const int VK_F3 = 0x72;

        private const int VK_F4 = 0x73;

        private const int VK_F5 = 0x74;

        private const int VK_F6 = 0x75;

        private const int VK_F7 = 0x76;

        private const int VK_F8 = 0x77;

        private const int VK_F9 = 0x78;

        private const int VK_F10 = 0x79;

        private const int VK_F11 = 0x7A;

        private const int VK_F12 = 0x7B;

        private const int VK_F13 = 0x7C;

        private const int VK_F14 = 0x7D;

        private const int VK_F15 = 0x7E;

        private const int VK_F16 = 0x7F;

        private const int VK_F17 = 0x80;

        private const int VK_F18 = 0x81;

        private const int VK_F19 = 0x82;

        private const int VK_F20 = 0x83;

        private const int VK_F21 = 0x84;

        private const int VK_F22 = 0x85;

        private const int VK_F23 = 0x86;

        private const int VK_F24 = 0x87;

        private const int VK_NUMLOCK = 0x90;

        private const int VK_SCROLL = 0x91;


        private const int VK_RSHIFT = 0xA1;

        private const int VK_BROWSER_BACK = 0xA6;

        private const int VK_BROWSER_FORWARD = 0xA7;

        private const int VK_BROWSER_REFRESH = 0xA8;

        private const int VK_BROWSER_STOP = 0xA9;

        private const int VK_BROWSER_SEARCH = 0xAA;

        private const int VK_BROWSER_FAVORITES = 0xAB;

        private const int VK_BROWSER_HOME = 0xAC;

        private const int VK_VOLUME_MUTE = 0xAD;

        private const int VK_VOLUME_DOWN = 0xAE;

        private const int VK_VOLUME_UP = 0xAF;

        private const int VK_MEDIA_NEXT_TRACK = 0xB0;

        private const int VK_MEDIA_PREV_TRACK = 0xB1;

        private const int VK_MEDIA_STOP = 0xB2;

        private const int VK_MEDIA_PLAY_PAUSE = 0xB3;

        private const int VK_LAUNCH_MAIL = 0xB4;

        private const int VK_LAUNCH_MEDIA_SELECT = 0xB5;

        private const int VK_LAUNCH_APP1 = 0xB6;

        private const int VK_LAUNCH_APP2 = 0xB7;

        private const int VK_PROCESSKEY = 0xE5;

        private const int VK_PACKET = 0xE7;

        private const int VK_ATTN = 0xF6;

        private const int VK_CRSEL = 0xF7;

        private const int VK_EXSEL = 0xF8;

        private const int VK_EREOF = 0xF9;

        private const int VK_PLAY = 0xFA;

        private const int VK_ZOOM = 0xFB;

        private const int VK_NONAME = 0xFC;

        private const int VK_PA1 = 0xFD;

        private const int VK_OEM_CLEAR = 0xFE;

        private const int VK_TAB = 0x09;
        private const int VK_SHIFT = 0x10;
        private const int VK_CONTROL = 0x11;
        private const int VK_MENU = 0x12;

        private const int VK_LSHIFT = 0xA0;
        private const int VK_RMENU = 0xA5;
        private const int VK_LMENU = 0xA4;
        private const int VK_LCONTROL = 0xA2;
        private const int VK_RCONTROL = 0xA3;
        private const int VK_LBUTTON = 0x01;
        private const int VK_RBUTTON = 0x02;
        private const int VK_MBUTTON = 0x04;
        private const int VK_XBUTTON1 = 0x05;
        private const int VK_XBUTTON2 = 0x06;

        private const int VK_OEM_1 = 0xBA;
        private const int VK_OEM_PLUS = 0xBB;
        private const int VK_OEM_COMMA = 0xBC;
        private const int VK_OEM_MINUS = 0xBD;
        private const int VK_OEM_PERIOD = 0xBE;
        private const int VK_OEM_2 = 0xBF;
        private const int VK_OEM_3 = 0xC0;
        private const int VK_C1 = 0xC1;   // Brazilian ABNT_C1 key (not defined in winuser.h).
        private const int VK_C2 = 0xC2;   // Brazilian ABNT_C2 key (not defined in winuser.h).
        private const int VK_OEM_4 = 0xDB;
        private const int VK_OEM_5 = 0xDC;
        private const int VK_OEM_6 = 0xDD;
        private const int VK_OEM_7 = 0xDE;
        private const int VK_OEM_8 = 0xDF;
        private const int VK_OEM_AX = 0xE1;
        private const int VK_OEM_102 = 0xE2;
        private const int VK_OEM_RESET = 0xE9;
        private const int VK_OEM_JUMP = 0xEA;
        private const int VK_OEM_PA1 = 0xEB;
        private const int VK_OEM_PA2 = 0xEC;
        private const int VK_OEM_PA3 = 0xED;
        private const int VK_OEM_WSCTRL = 0xEE;
        private const int VK_OEM_CUSEL = 0xEF;
        private const int VK_OEM_ATTN = 0xF0;
        private const int VK_OEM_FINISH = 0xF1;
        private const int VK_OEM_COPY = 0xF2;
        private const int VK_OEM_AUTO = 0xF3;
        private const int VK_OEM_ENLW = 0xF4;
        private const int VK_OEM_BACKTAB = 0xF5;


        /// <summary>
        /// Convert Key into a VirtualKey.
        /// </summary>
        public static int VirtualKeyFromKey(Keys key)
        {
            switch (key)
            {
                case Keys.Tab:
                    return VK_TAB;

                case Keys.Clear:
                    return VK_CLEAR;

                case Keys.Enter:
                    return VK_RETURN;

                case Keys.Pause:
                    return VK_PAUSE;

                case Keys.CapsLock:
                    return VK_CAPITAL;

                case Keys.Escape:
                    return VK_ESCAPE;

                case Keys.Space:
                    return VK_SPACE;

                case Keys.End:
                    return VK_END;

                case Keys.Home:
                    return VK_HOME;

                case Keys.Left:
                    return VK_LEFT;

                case Keys.Up:
                    return VK_UP;

                case Keys.Right:
                    return VK_RIGHT;

                case Keys.Down:
                    return VK_DOWN;

                case Keys.Insert:
                    return VK_INSERT;

                case Keys.Delete:
                    return VK_DELETE;

                case Keys.Help:
                    return VK_HELP;

                case Keys.D0:
                    return VK_0;

                case Keys.D1:
                    return VK_1;

                case Keys.D2:
                    return VK_2;

                case Keys.D3:
                    return VK_3;

                case Keys.D4:
                    return VK_4;

                case Keys.D5:
                    return VK_5;

                case Keys.D6:
                    return VK_6;

                case Keys.D7:
                    return VK_7;

                case Keys.D8:
                    return VK_8;

                case Keys.D9:
                    return VK_9;

                case Keys.A:
                    return VK_A;

                case Keys.B:
                    return VK_B;

                case Keys.C:
                    return VK_C;

                case Keys.D:
                    return VK_D;

                case Keys.E:
                    return VK_E;

                case Keys.F:
                    return VK_F;

                case Keys.G:
                    return VK_G;

                case Keys.H:
                    return VK_H;

                case Keys.I:
                    return VK_I;

                case Keys.J:
                    return VK_J;

                case Keys.K:
                    return VK_K;

                case Keys.L:
                    return VK_L;

                case Keys.M:
                    return VK_M;

                case Keys.N:
                    return VK_N;

                case Keys.O:
                    return VK_O;

                case Keys.P:
                    return VK_P;

                case Keys.Q:
                    return VK_Q;

                case Keys.R:
                    return VK_R;

                case Keys.S:
                    return VK_S;

                case Keys.T:
                    return VK_T;

                case Keys.U:
                    return VK_U;

                case Keys.V:
                    return VK_V;

                case Keys.W:
                    return VK_W;

                case Keys.X:
                    return VK_X;

                case Keys.Y:
                    return VK_Y;

                case Keys.Z:
                    return VK_Z;

                case Keys.Multiply:
                    return VK_MULTIPLY;

                case Keys.Add:
                    return VK_ADD;

                case Keys.Subtract:
                    return VK_SUBTRACT;

                case Keys.Decimal:
                    return VK_DECIMAL;

                case Keys.Divide:
                    return VK_DIVIDE;

                case Keys.F1:
                    return VK_F1;

                case Keys.F2:
                    return VK_F2;

                case Keys.F3:
                    return VK_F3;

                case Keys.F4:
                    return VK_F4;

                case Keys.F5:
                    return VK_F5;

                case Keys.F6:
                    return VK_F6;

                case Keys.F7:
                    return VK_F7;

                case Keys.F8:
                    return VK_F8;

                case Keys.F9:
                    return VK_F9;

                case Keys.F10:
                    return VK_F10;

                case Keys.F11:
                    return VK_F11;

                case Keys.F12:
                    return VK_F12;

                case Keys.F13:
                    return VK_F13;

                case Keys.F14:
                    return VK_F14;

                case Keys.F15:
                    return VK_F15;

                case Keys.F16:
                    return VK_F16;

                case Keys.F17:
                    return VK_F17;

                case Keys.F18:
                    return VK_F18;

                case Keys.F19:
                    return VK_F19;

                case Keys.F20:
                    return VK_F20;

                case Keys.F21:
                    return VK_F21;

                case Keys.F22:
                    return VK_F22;

                case Keys.F23:
                    return VK_F23;

                case Keys.F24:
                    return VK_F24;

                case Keys.NumberLock:
                    return VK_NUMLOCK;

                case Keys.ScrollLock:
                    return VK_SCROLL;

                case Keys.LeftShift:
                    return VK_LSHIFT;

                case Keys.RightShift:
                    return VK_RSHIFT;

                case Keys.LeftControl:
                    return VK_LCONTROL;

                case Keys.RightControl:
                    return VK_RCONTROL;

                case Keys.LeftAlt:
                    return VK_LMENU;

                case Keys.RightAlt:
                    return VK_RMENU;

                case Keys.Semicolon:
                    return VK_OEM_1;

                case Keys.Equal:
                    return VK_OEM_PLUS;

                case Keys.Comma:
                    return VK_OEM_COMMA;

                case Keys.Minus:
                    return VK_OEM_MINUS;

                case Keys.Period:
                    return VK_OEM_PERIOD;

                case Keys.Slash:
                    return VK_OEM_2;

                case Keys.LeftBracket:
                    return VK_OEM_4;

                case Keys.Backslash:
                    return VK_OEM_5;

                case Keys.RightBracket:
                    return VK_OEM_6;

                case Keys.Quote:
                    return VK_OEM_7;

                default:
                    return 0;
            }
        }
    }
}
