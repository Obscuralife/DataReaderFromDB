using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HostedService
{
    /// <summary>
    /// Represents application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets json data base file name.
        /// </summary>
        public string LocationsFileName { get; set; }

        /// <summary>
        /// Gets path to json data base.
        /// </summary>
        public string PathToLocationsFile { get => GetPath(); }

        private string GetPath()
        {
            var baseDir = Environment.CurrentDirectory;
            var rootFolder = baseDir.Remove(baseDir.IndexOf("HostedService"));
            return Path.Combine(rootFolder, LocationsFileName);
        }
    }
}
