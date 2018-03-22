using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheMachine
{
    public static class Constants
    {
        public const string CardNumMask = "0000-0000-0000-0000";
        public const string PinCodeMask = "0000";
        public const int ValidPinCodeTriesCount = 3;
        public const int SaltSize = 24;
    }
}