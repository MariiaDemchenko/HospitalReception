namespace CacheMachine.Common
{
    public static class Constants
    {
        public const string CardNumMask = "0000-0000-0000-0000";
        public const string PinCodeMask = "0000";
        public const string Dash = "-";
        public const string Empty = "";

        public const int ValidPinCodeTriesCount = 3;
        public const int SaltSize = 24;

        public const int KeyCode0 = 48;
        public const int KeyCode9 = 57;
        public const int KeyCode0NumPad = 96;
        public const int KeyCode9NumPad = 105;
    }
}