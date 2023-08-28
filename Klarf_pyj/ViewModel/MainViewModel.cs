using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klarf
{
    class MainViewModel
    {
        FileListViewModel fileListViewModel = new FileListViewModel();
        DefectInfoViewModel defectInfoViewModel = new DefectInfoViewModel();
        DefectImageViewModel defectImageViewModel = new DefectImageViewModel();
        WaferMapViewModel waferMapViewModel = new WaferMapViewModel();
    }
}
