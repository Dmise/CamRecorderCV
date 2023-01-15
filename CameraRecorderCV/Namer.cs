using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraRecorderCV
{
    /// <summary>
    /// Generate name for files
    /// </summary>
    internal class Namer
    {
        public static string GetDutyVideoName()
        {
            var date = DateTime.Now;
            return date.Day + "_" + date.Month + "_" + "duty_" + date.Hour;
           
        }

        public static string GetFragmentVideoName()
        {
            var date = DateTime.Now;
            return date.Day + "_" + date.Month + "_" + "frag_" + date.Hour + "_" + date.Minute + "_" + date.Second;
        }
    }
}
