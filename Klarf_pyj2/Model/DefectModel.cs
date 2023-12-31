﻿using System.Collections.Generic;

namespace Klarf
{
    public class DefectModel
    {
        public string folderPath;
        public string waferID, lotID, fileTimestamp, deviceID;
        public int currentImageIndex;
        public List<string> defectID, xRel, yRel, xIndex, yIndex, xSize, ySize, defectArea, dSize, classNumber, test, clusterNumber, roughBinNumber, fineBinNumber, reviewSample, imageCount;
        public List<int> xIndices, yIndices;
    }
}
