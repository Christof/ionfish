using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class PicasaImageUploader
    {
        [Serializable]
        public class PicasaSettings
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string AlbumOwner { get; set; }
            public string AlbumId { get; set; }
        }

        private readonly PicasaSettings mPicasaSettings;
        private string mAuthenticationToken;

        public PicasaImageUploader()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Ionfish");
            var path = Path.Combine(folder, "picasa_settings.xml");
            if (File.Exists(path))
            {
                mPicasaSettings = Serializer.DeserializeXml<PicasaSettings>(path);
            }
        }

        private void Authenticate()
        {
            var webRequest = WebRequest.Create("https://www.google.com/accounts/ClientLogin");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            var postBytes = Encoding.ASCII.GetBytes(
                string.Format("accountType=HOSTED_OR_GOOGLE&Email={0}&Passwd={1}&service=lh2&source=bla",
                mPicasaSettings.Username, mPicasaSettings.Password));
            webRequest.ContentLength = postBytes.Length;

            using (var requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(postBytes, 0, postBytes.Length);
            }
            var response = (HttpWebResponse)webRequest.GetResponse();

            var responseText = GetStreamContent(response.GetResponseStream());

            var startIndex = responseText.IndexOf("Auth=") + 5;
            mAuthenticationToken = responseText.Substring(startIndex, responseText.IndexOf("\n", startIndex) - startIndex);
        }

        private static string GetStreamContent(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public void UploadImage(string filename)
        {
            if (mPicasaSettings == null)
            {
                return;
            }

            var task = new Task(() => UploadImageBySendingWebRequest(filename));
            task.Start();
            task.ContinueWith(x => x.Dispose());
        }

        private void UploadImageBySendingWebRequest(string filename)
        {
            if (string.IsNullOrEmpty(mAuthenticationToken))
            {
                Authenticate();
            }

            string url = string.Format("http://picasaweb.google.com/data/feed/api/user/{0}/albumid/{1}", mPicasaSettings.AlbumOwner, mPicasaSettings.AlbumId);
            var uploadRequest = WebRequest.Create(url);

            uploadRequest.Method = "POST";
            uploadRequest.Headers.Add(string.Format("Authorization: GoogleLogin auth={0}", mAuthenticationToken));
            uploadRequest.Headers.Add(string.Format("slug: {0}", filename));
            uploadRequest.ContentType = "image/png";

            var binaryPic = ReadImage(filename);
            uploadRequest.ContentLength = binaryPic.Length;
            using (var uploadStream = uploadRequest.GetRequestStream())
            {
                uploadStream.Write(binaryPic, 0, binaryPic.Length);
            }

            var uploadResponse = (HttpWebResponse)uploadRequest.GetResponse();

            Console.WriteLine(uploadResponse.StatusDescription);
        }

        private static byte[] ReadImage(string filename)
        {
            using (var fileStream = new FileStream(filename, FileMode.Open))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    return reader.ReadBytes((int)new FileInfo(filename).Length);
                }
            }
        }
    }
}