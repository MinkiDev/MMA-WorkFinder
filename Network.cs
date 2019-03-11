using System.IO;
using System.Net;
using System.Text;

namespace MMA_WorkFinder
{
    internal class Network
    {
        public static string Request(string uri)
        {
            string res = null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.Method = "GET";
                httpWebRequest.Proxy = (IWebProxy)null;
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.KeepAlive = true;
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                TextReader textReader = (TextReader)new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                res = textReader.ReadToEnd();
                response.Close();
                textReader.Close();
            }
            catch { }
            return res;
        }
    }
}