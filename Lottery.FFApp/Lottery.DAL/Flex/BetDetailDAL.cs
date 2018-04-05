// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.BetDetailDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Lottery.DAL.Flex
{
    public static class BetDetailDAL
    {
        private static string Folder = Const.BetUrl;

        public static void SetBetDetail(string STime, string UserId, string BetId, string Detail)
        {
            BetDetailDAL.SaveContentFile(Detail, STime + "\\" + UserId + "\\" + BetId + ".js");
        }

        public static string GetBetDetail2(string STime, string UserId, string BetId)
        {
            if (string.IsNullOrEmpty(BetDetailDAL.ReadContentFile(STime + "\\" + UserId + "\\" + BetId + ".js")))
            {
                string str = "";
                using (DbOperHandler dbOperHandler = new ComData().Doh())
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select [Detail] from N_UserBet where Id=" + BetId;
                    DataTable dataTable = dbOperHandler.GetDataTable();
                    str = string.Concat(dataTable.Rows[0]["Detail"]);
                    dataTable.Clear();
                    dataTable.Dispose();
                }
                return BetDetailDAL.ReadContentFile(STime + "\\" + UserId + "\\" + str + ".js");
            }
            return BetDetailDAL.ReadContentFile(STime + "\\" + UserId + "\\" + BetId + ".js");
        }

        public static string GetBetDetail(string STime, string UserId, string BetId)
        {
            if (string.IsNullOrEmpty(BetDetailDAL.ReadContentFile(STime + "\\" + UserId + "\\" + BetId + ".js")))
            {
                string str = "";
                using (DbOperHandler dbOperHandler = new ComData().Doh())
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select [Detail] from N_UserBet where Id=" + BetId;
                    DataTable dataTable = dbOperHandler.GetDataTable();
                    str = string.Concat(dataTable.Rows[0]["Detail"]);
                    dataTable.Clear();
                    dataTable.Dispose();
                }
                return BetDetailDAL.ReadContentFile(STime + "\\" + UserId + "\\" + str + ".js");
            }
            return BetDetailDAL.ReadContentFile(STime + "\\" + UserId + "\\" + BetId + ".js");
        }

        public static void SetYouleDetail(string STime, string UserId, string BetId, string Detail)
        {
            BetDetailDAL.SaveContentFile(Detail, STime + "\\" + UserId + "\\" + BetId + ".js");
        }

        public static string GetYouleDetail(string STime, string UserId, string BetId)
        {
            Random random = new Random();
            return HtmlOperate.GetHtml("http://192.168.0.51:999/" + STime + "/" + UserId + "/" + BetId + ".js?" + (object)random.Next(1, 1000));
        }

        public static bool connectState(string path)
        {
            return BetDetailDAL.connectState(path, "", "");
        }

        public static bool connectState(string path, string userName, string passWord)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string str = "net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                process.StandardInput.WriteLine(str);
                process.StandardInput.WriteLine("exit");
                while (!process.HasExited)
                    process.WaitForExit(1000);
                string end = process.StandardError.ReadToEnd();
                process.StandardError.Close();
                if (string.IsNullOrEmpty(end))
                    return true;
                throw new Exception(end);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                process.Close();
                process.Dispose();
            }
        }

        public static void ReadFiles(string path)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string str;
                    while ((str = streamReader.ReadLine()) != null)
                        Console.WriteLine(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }

        public static void WriteFiles(string path)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    streamWriter.Write("This is the ");
                    streamWriter.WriteLine("header for the file.");
                    streamWriter.WriteLine("-------------------");
                    streamWriter.Write("The date is: ");
                    streamWriter.WriteLine((object)DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }

        private static string ReadContentFile2(string fileName)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(new DirectoryInfo("\\\\192.168.0.51\\Bets").ToString() + "\\" + fileName, Encoding.UTF8))
                    return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                new LogExceptionDAL().Save("派奖异常", ex.Message);
                return "";
            }
        }

        private static bool FileExists(string file)
        {
            return File.Exists(file);
        }

        private static string ReadContentFile(string fileName)
        {
            string str = BetDetailDAL.Folder + fileName;
            if (!BetDetailDAL.FileExists(str))
                return "";
            try
            {
                StreamReader streamReader = new StreamReader(str, Encoding.UTF8);
                string end = streamReader.ReadToEnd();
                streamReader.Close();
                return end;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void SaveContentFile(string TxtStr, string fileName)
        {
            BetDetailDAL.SaveContentFile(TxtStr, fileName, "2");
        }

        private static void SaveContentFile(string TxtStr, string fileName, string Edcode)
        {
            string str = BetDetailDAL.Folder + fileName;
            Encoding encoding = Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    encoding = Encoding.Unicode;
                    break;
                case "2":
                    encoding = Encoding.UTF8;
                    break;
                case "1":
                    encoding = Encoding.GetEncoding("GB2312");
                    break;
            }
            DirFile.CreateFolder(DirFile.GetFolderPath(false, str));
            StreamWriter streamWriter = new StreamWriter(str, false, encoding);
            streamWriter.Write(TxtStr);
            streamWriter.Close();
        }

        public static void WriteFilesOfWangluo(string path, string content)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(path))
                    streamWriter.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
