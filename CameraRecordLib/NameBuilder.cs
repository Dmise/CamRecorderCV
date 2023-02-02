using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraRecordLib
{
    /// <summary>
    /// Generate name for files
    /// </summary>
    public class NameBuilder
    {
        private static string format = ".mp4";
        public static string GetDutyVideoName()
        {
            var date = DateTime.Now;
            return date.Day + "_" + date.Month + "_" + "duty_" + date.Hour + format;
           
        }

        public static string GetFragmentVideoName()
        {
            var date = DateTime.Now;
            return date.Day + "_" + date.Month + "_" + "frag_" + date.Hour + "_" + date.Minute + "_" + date.Second + format;
        }
    }
}
