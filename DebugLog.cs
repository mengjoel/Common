
    public class DebugLog
    {
        string _key = "";
        Stopwatch stw = null;
        double totalMilliseconds = 0;
        string msg = "";
        private bool Check()
        {
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "DebugLog\\";
            return System.IO.Directory.Exists(dir);
        }
        public DebugLog(string key)
        {
            if (!Check())
                return;

            stw = new Stopwatch();
            _key = key;
        }

        public void Start(string flag)
        {
            if (!Check())
                return;
            msg = "";
            totalMilliseconds = 0;
            msg += "Start:" + flag + " at " + DateTime.Now.ToString() + "\r\n";
            stw.Start();
        }
        public void Record(string flag)
        {
            if (!Check() || stw == null)
                return;
            stw.Stop();
            totalMilliseconds += stw.Elapsed.TotalMilliseconds;
            msg += flag + ":" + stw.Elapsed.TotalMilliseconds.ToString() + "\r\n";
            stw.Restart();
        }
        public void Stop(string flag)
        {
            if (!Check() || stw == null)
                return;
            stw.Stop();
            totalMilliseconds += stw.Elapsed.TotalMilliseconds;
            msg += flag + ":" + stw.Elapsed.TotalMilliseconds.ToString() + "\r\n";
            msg += "Total milliseconds:" + totalMilliseconds.ToString() + "\r\n";
            msg += "Stop:" + flag + " at " + DateTime.Now.ToString() + "\r\n";
            //
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "DebugLog\\";
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            string filename = DateTime.Today.ToString("yyyy-MM-dd") + "_" + _key + ".txt";

            string strFilePath = dir + filename;
            object lockObj = GetLockObject(strFilePath);
            lock (lockObj)
            {
                WriteFile(msg, strFilePath);
                return;
            }
            //SaveLog(strFilePath, msg);
        }
      
        public static void WriteLog(string msg, string key = "")
        {
            //获取文件全名（包括路径）
            string filename = DateTime.Now.ToString("yyyy-MM-dd") + (key != "" ? ("_" + key) : "");
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "Log\\";
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            string strFilePath = dir + filename + ".txt";
            string msg2 = string.Format("{0} : {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
            object lockObj = GetLockObject(strFilePath);
            lock (lockObj)
            {
                WriteFile(msg2, strFilePath);
            }
        }

        public static void WriteLog(Exception ex, string title = "")
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyy-MM-dd");
                string dir = System.AppDomain.CurrentDomain.BaseDirectory + "ExceptionsLog\\";
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                string strFilePath = dir + filename + ".txt";
                //
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(title))
                    sb.AppendLine(title);
                sb.AppendLine(string.Format("{0} : [{1}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Catch Exception:"));
                sb.AppendLine(string.Format("    [Exception Type-{0}]", ex.GetType()));
                sb.AppendLine("");
                sb.AppendLine(string.Format("    [Exception Message-{0}]", ex.Message));
                try
                {
                    sb.AppendLine("    [Exception Stack-" + ex.StackTrace + "]");
                }
                catch { sb.AppendLine("      [Exception Stack-获取ex.StackTrace失败]"); }
                if (ex.InnerException != null)
                {
                    try
                    {
                        sb.AppendLine("      [InnerException Stack-" + ex.InnerException.StackTrace + "]");
                    }
                    catch { sb.AppendLine("      [InnerException Stack-获取ex.InnerException.StackTrace失败]"); }

                }
                //写入操作
                object lockObj = GetLockObject(strFilePath);
                lock (lockObj)
                {
                    WriteFile(sb.ToString(), strFilePath);
                }
            }
            catch (Exception ex2)
            {
                DebugLog.WriteLog("WriteLog调用失败,ex.message:" + ex2.Message, "DebugLog_Error");
            }
        }
        static void WriteFile(string msg, string strFilePath)
        {
            System.IO.FileStream fs = null;
            System.IO.StreamWriter sw = null;
            try
            {
                fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Append);
                sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
                sw.WriteLine(msg);
            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                    sw.Close();
                if (fs != null)
                    fs.Close();
            }

        }
    }
}
