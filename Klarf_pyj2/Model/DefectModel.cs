using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace Klarf
{
    public class DefectModel
    {
        public string waferID, lotID, fileTimestamp, deviceID;
        public List<string> defectID, xRel, yRel, xIndex, yIndex, xSize, ySize, defectArea, dSize, classNumber, test, clusterNumber, roughBinNumber, fineBinNumber, reviewSample, imageCount;
        public List<int> xIndices, yIndices;
    }
}
