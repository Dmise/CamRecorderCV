using CameraRecordLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CameraRecordLib
{
    /// <summary>
    /// Appsettings read values from file
    /// </summary>
    public class Settings
    {        
        public string saveTo;

        public Resolution resolution;

        /// <summary>
        /// Init class from txt file
        /// </summary> 
        /// <param name="filename">file should be in the same directory with exe</param>
        public void InitFromFile(string filename = "settings.json")
        {
            var text = File.ReadAllText(filename);
            var set = JsonConvert.DeserializeObject<Settings>(text);
            if (set != null)
            {
                saveTo = set.saveTo;
                resolution = set.resolution;
            }
            else
            {
                throw new DataException("Did not recognize Setting class from json settings file");
            }
        }

        public void SaveToFile(string filename = "settings.json")
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(filename, String.Empty);
            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                sw.WriteLine(json);
            }
        }
    }
}