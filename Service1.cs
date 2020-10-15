using ShadowScannerService.Model.API_Jotti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ShadowScannerService
{
    public partial class Service1 : ServiceBase
    {
        readonly Timer timer = new Timer();
        readonly JottiServer jottiServer = new JottiServer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 1000 * 60 * 1;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped");
        }


        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall");
            try
            {
                jottiServer.GetFileInfoMultiple();
            }
            catch (Exception ex)
            {
                WriteToFile(ex.Message + " - " + ex.StackTrace);
                throw;
            }

        }


        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!System.IO.File.Exists(filepath))
            {
                FileInfo fi = new FileInfo(filepath);
                if (DateTime.Now.AddDays(-7).Date < fi.CreationTime)
                {
                    System.IO.File.Delete(filepath);
                }
                // Create a file to write to. 
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine($"[{DateTime.Now.ToString()}] - {Message}");
                }
            }
            else
            {
                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine($"[{DateTime.Now.ToString()}] - {Message}");
                }
            }
        }
    }
}
