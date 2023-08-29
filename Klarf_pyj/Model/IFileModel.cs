using System.Collections.Generic;
using System.IO;

namespace Klarf
{
    public interface IFileModel
    {
        List<FileInfo> LoadFileList();
        List<string> LoadFileDateList();
        string LoadFile(string filePath, string targetExtension);
        List<string> GetFileInfo(string filePath);

    }
}
