using System.IO;

namespace Klarf_pyj.Model
{
    class FileModel
    {
        public void LoadFile()
        {
            string folderPath = @"C:\Users\yjyu\Desktop\IPP 과제\Klarf\Klarf";

            string[] fileNames = Directory.GetFiles(folderPath);
        }
    }
}
