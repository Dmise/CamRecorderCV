using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CameraRecorderCV
{
    /// <summary>
    /// Appsettings
    /// </summary>
    internal class Settings
    {
        public string saveTo;

        public Resolution resolution = new Resolution();

        /// <summary>
        /// Init class from txt file
        /// </summary> 
        public void InitFromFile()
        {
            var text = File.ReadAllText("settings.json");
            var set =  JsonConvert.DeserializeObject<Settings>(text);
            
            saveTo = set.saveTo;
            resolution = set.resolution;
        }

        public void SaveToFile() 
        {
            var json = JsonConvert.SerializeObject(this);
            using (StreamWriter sw = new StreamWriter("settings.json", false))
            {
                sw.WriteLine(json);
            }
        }
    }  
}
