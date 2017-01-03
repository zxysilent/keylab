using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keylab.Utils {
    public class Strings {
        public const string TokenKeyAdmin = "keylabtokenadmin";
        public const string TokenKeyGuest = "keylabtokenguest";
        public static string Rand() {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
