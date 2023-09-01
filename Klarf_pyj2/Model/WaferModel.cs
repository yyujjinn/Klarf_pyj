using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Klarf
{
    public class WaferModel
    {
        public int width, height;
        public List<string> xIndex, yIndex;
        public List<int> xIndices, yIndices;

        //public void GetPartsWaferIndex(string filePath, int partIndex)
        //{
        //    List<string> partsWaferIndex = new List<string>();
        //    bool startReading = false;

        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        string line;

        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            if (line.Contains("SampleTestPlan"))
        //            {
        //                startReading = true;
        //                continue;
        //            }

        //            if (startReading)
        //            {
        //                if (line.Contains("AreaPerTest"))
        //                {
        //                    break;
        //                }
        //                string[] parts = line.Split(' ');
        //                partsWaferIndex.Add(parts[partIndex]);
        //            }
        //        }
        //    }
        //}

        //public void GetWaferIndex(string filePath)
        //{
        //    List<string> XIndex = new List<string>();
        //    List<string> YIndex = new List<string>();

        //    for (int i = 0; i < 2; i++)
        //    {
        //        this.GetPartsWaferIndex(filePath, i);
        //    }
        //}
    }
}
