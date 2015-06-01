using System.IO;
using System.Net;

namespace Mega.Synchronizer.Common.Utilities
{
    public static class FtpProvider
    {
        public static string Download(string user, string password, string ftpFileUrl, string localFileName, bool usePassive)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpFileUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = false;
            request.UsePassive = usePassive;

            if (!string.IsNullOrEmpty(user))
            {
                request.Credentials = new NetworkCredential(user, password);
            }

            var response = (FtpWebResponse)request.GetResponse();

            var responseStream = response.GetResponseStream();

            var outputStream = new FileStream(localFileName, FileMode.Create);

            const int bufferSize = 2048;
            var buffer = new byte[bufferSize];

            var readCount = responseStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                outputStream.Write(buffer, 0, readCount);
                readCount = responseStream.Read(buffer, 0, bufferSize);
            }

            responseStream.Close();
            outputStream.Close();
            response.Close();

            return response.StatusDescription;
        }

        public static void Upload(string user, string password, string ftpFileUrl, string localFileName, bool usePassive)
        {
            var request = (FtpWebRequest)FtpWebRequest.Create(ftpFileUrl);

            if (!string.IsNullOrEmpty(user))
            {
                request.Credentials = new NetworkCredential(user, password);
            }

            request.KeepAlive = true;
            request.UseBinary = true;
            request.UsePassive = usePassive;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            var fs = File.OpenRead(localFileName);
            var buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            var ftpstream = request.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();
        }

        public static void Delete(string user, string password, string ftpFileUrl, bool usePassive)
        {
            var request = (FtpWebRequest)FtpWebRequest.Create(ftpFileUrl);

            if (!string.IsNullOrEmpty(user))
            {
                request.Credentials = new NetworkCredential(user, password);
            }

            request.KeepAlive = true;
            request.UseBinary = true;
            request.UsePassive = usePassive;
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.GetResponse().Close();
        }
    }
}
