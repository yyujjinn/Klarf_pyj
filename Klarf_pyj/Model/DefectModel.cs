using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Klarf
{
    public class DefectModel
    {
        //public List<string> GetDefectList(string filePath)
        //{
        //    List<string> lines = new List<string>();

        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        string line;

        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            string[] parts = line.Split(' ');
        //        }
        //    }
        //}

        public static List<string> GetDefectID(string filePath)
        {
            List<string> lines = new List<string>();
            List<string> defectID = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');

                    defectID.Add(parts[0]);
                }
            }
            return defectID;
        }

        //public List<string> GetXRel(string filePath)
        //{
        //    List<string> lines = new List<string>();
        //    List<string> XRel = new List<string>();

        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        string line;

        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            string[] parts = line.Split(' ');

        //           XRel.Add(parts[1]);
        //        }
        //    }
        //}
    }
}
