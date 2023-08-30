using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace Klarf
{
    public class DefectModel
    {
        public List<string> defectID, xRel, yRel, xIndex, yIndex, xSize, ySize, defectArea, dSize, classNumber, test, clusterNumber, roughBinNumber, fineBinNumber, reviewSample, imageCount;
        //public void GetPartsDefectList(string filePath, int partIndex)
        //{
        //    List<string> partsDefectList = new List<string>();
        //    bool startReading = false;

        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        string line;

        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            if (line.Contains("DefectList"))
        //            {
        //                startReading = true;
        //                continue;
        //            }

        //            if (startReading)
        //            {
        //                if (line.Contains("SummarySpec"))
        //                {
        //                    break;
        //                }
        //                string[] parts = line.Split(' ');
        //                partsDefectList.Add(parts[partIndex]);
        //            }
        //        }
        //    }
        //}

        //public void SavePartsDefectList(string filePath)
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        this.GetPartsDefectList(filePath, i);
        //    }
        //}

    }
}
