using System.IO;
using System.Threading.Tasks;

namespace Memoirs.Common.Video
{
    public interface ILoginVideoProvider
    {
        Task WriteToStream(Stream outputStream);
        string GetMediaType();
    }
}
