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
        
        public static string GetDutyVideoName(string suffix = ".mp4")
        {
            var date = DateTime.Now;
            return date.Day + "_" + date.Month + "_" + "duty_" + date.Hour + "_" + date.Minute + suffix;
           
        }

        public static string GetFragmentVideoName(string suffix = ".mp4")
        {
            var date = DateTime.Now;
            return date.Day + "_" + date.Month + "_" + "frag_" + date.Hour + "_" + date.Minute + "_" + date.Second + suffix;
        }
    }
}
