using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraRecordLib
{
    public interface IRecordManager
    {
        public string SaveTo { get; set; }
        public void InitFromFile();
        public void Start(RecordType type); 

    }
}
