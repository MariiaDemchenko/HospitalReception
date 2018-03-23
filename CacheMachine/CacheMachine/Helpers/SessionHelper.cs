using System.Collections.Generic;
using System.Web;

namespace CacheMachine.Helpers
{
    public class SessionHelper
    {
        public Dictionary<string, int> InvalidPinCodes
        {
            get => HttpContext.Current.Session["InvalidPinCodes"] as Dictionary<string, int>;
            set => HttpContext.Current.Session["InvalidPinCodes"] = value;
        }

        public bool IsAuthorized
        {
            get => (bool)HttpContext.Current.Session["IsAuthorized"];
            set => HttpContext.Current.Session["IsAuthorized"] = value;
        }

        public string CardNumber
        {
            get => HttpContext.Current.Session["CardNumber"] as string;
            set => HttpContext.Current.Session["CardNumber"] = value;
        }
    }
}