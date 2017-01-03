using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Keylab.Utils {
    public class Log {
        public static void Write(string msg, Exception ex) {
            msg += ex.Message + ".\nException\t:" + ex.StackTrace;
            if (ex.InnerException != null) {
                msg += ex.InnerException.Message + ";\nInnerException\t:" + ex.InnerException.StackTrace;
            }
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Err\");
            if (!Directory.Exists(logDir)) {
                Directory.CreateDirectory(logDir);
            }
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            using (StreamWriter sw = new StreamWriter(logDir + fileName, true)) {
                sw.WriteLine("\n\n\n--------------------------------------------------------- BEGIN ---------------------------------------------------------");
                sw.WriteLine("\n\nTime\t:" + DateTime.Now.ToString());
                sw.WriteLine("Message\t:" + msg);
            }
        }
        public static void Write(string msg) {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\");
            if (!Directory.Exists(logDir)) {
                Directory.CreateDirectory(logDir);
            }
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            using (StreamWriter sw = new StreamWriter(logDir + fileName, true)) {
                sw.WriteLine("\n\n\n--------------------------------------------------------- BEGIN ---------------------------------------------------------");
                sw.WriteLine("Time\t:" + DateTime.Now.ToString());
                sw.WriteLine("Message\t:" + msg);
            }
        }
    }
}
