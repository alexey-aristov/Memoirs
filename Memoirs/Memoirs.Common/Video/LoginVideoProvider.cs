using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Common.Video
{
    public class LoginVideoProvider: ILoginVideoProvider
    {
        private string _loginVideoPathKey = "login_video_path";
        public async Task WriteToStream(Stream outputStream)
        {
            try
            {
                var buffer = new byte[65536];

                using (var video = File.OpenRead(ConfigurationManager.AppSettings.Get(_loginVideoPathKey)))
                {
                    var length = (int)video.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = video.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch (Exception ex)
            {
                //todo: log!
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }

        public string GetMediaType()
        {
            return "video/mp4";
        }
    }
}
