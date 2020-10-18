
using Newtonsoft.Json;
using RestSharp;
using ShadowScannerService.Model.API_Jotti.Entities;
using ShadowScannerService.Model.API_Jotti.Helper;
using ShadowScannerService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ShadowScannerService.Model.API_Jotti
{
    public class JottiServer
    {
        readonly string Url = "https://virusscan.jotti.org";
        readonly Cyber20ShadowEntities cyber20ShadowEntities;
        public JottiServer()
        {
            cyber20ShadowEntities = new Cyber20ShadowEntities();
        }

        public bool GetFileInfoMultiple()
        {
            var settings = new JsonSerializerSettings { Converters = new JsonConverter[] { new JsonGenericDictionaryOrArrayConverter() } };

            var lastOriginTablID = cyber20ShadowEntities.Jottis.Select(x => x.OriginTableID).Take(1).OrderByDescending(x => x.Value).FirstOrDefault();

            if (lastOriginTablID == null) lastOriginTablID = 0;

            IEnumerable<OriginTable> originTables = cyber20ShadowEntities.OriginTables.Where(o => o.ID > lastOriginTablID).OrderBy(x => x.ID).Take(10).ToList();
            string[] hashes = originTables.Select(x => x.ApplicationMD5).ToArray();
            if (originTables.Any())
            {
                IEnumerable<Jotti> jottis = null;
                try
                {
                    var client = new RestClient(Url);
                    var req = new RestRequest($"/api/filescanjob/getfileinfomultiple", Method.POST) { RequestFormat = DataFormat.Json };
                    req.AddParameter("Authorization", "key 4sTAMC2nqJjLwsBN", ParameterType.HttpHeader);
                    req.AddHeader("Accept", "application/json");
                    req.AddJsonBody(new { hashes });
                    var response = JsonConvert.DeserializeObject<Response>(client.Execute(req).Content, settings);

                    jottis = CreateJottiObjectWithValue(response.Results.Values, originTables);
                }
                catch (Exception ex)
                {
                    WriteToFile(ex.Message + " - " + ex.StackTrace);
                    throw;
                }

                try
                {
                    foreach (Jotti jotti in jottis)
                    {
                        cyber20ShadowEntities.Jottis.Add(jotti);
                    }
                    cyber20ShadowEntities.SaveChanges();
                }
                catch (Exception ex)
                {
                    WriteToFile(ex.Message + " - " + ex.StackTrace);
                    throw;
                }

                return true;
            }
            else
                return false;
        }



        private IEnumerable<Jotti> CreateJottiObjectWithValue(IEnumerable<Result> results, IEnumerable<OriginTable> originTables)
        {
            Jotti jotti = null;
            List<Jotti> Jottis = new List<Jotti>();

            foreach (OriginTable originTable in originTables)
            {
                var r = results.Where(x => x.File.Hashes.MD5.Contains(originTable.ApplicationMD5.ToLower())).FirstOrDefault();
                if (r != null)
                {
                    jotti = new Jotti
                    {
                        CreateDate = DateTime.Now,
                        Name = r.File?.Name,
                        WebUrl = r.MostRecentScanJob?.WebUrl,
                        ScannersDetected = r.MostRecentScanJob.ScannersDetected,
                        OriginTableID = originTable.ID
                    };

                    if (r.MostRecentScanJob?.ScannersDetected > 0)
                    {
                        List<string> q = new List<string>();
                        foreach (ScannerResult scannerResult in r.MostRecentScanJob.ScannerResults)
                        {
                            if (scannerResult.MalwareName != "")
                            {
                                q.Add($"[{scannerResult.ScannerName}:{scannerResult.MalwareName}]");
                            }
                        }
                        jotti.MalwareName = string.Join(",", q.ToArray());
                    }

                }
                else
                {
                    jotti = new Jotti
                    {
                        CreateDate = DateTime.Now,
                        Name = originTable.ApplicationName,
                        ScannersDetected = 0,
                        OriginTableID = originTable.ID,
                        MalwareName = "Unknown"
                    };
                }

                Jottis.Add(jotti);
            }


            return Jottis;
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
