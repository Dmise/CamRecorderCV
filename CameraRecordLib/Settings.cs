using CameraRecordLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace CameraRecordLib
{
    /// <summary>
    /// Appsettings read values from file
    /// </summary>
    public class Settings
    {        
        public string SaveTo { get; set; }

        public Resolution Resolution { get; set; }
    }
}