using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseCounter.Models
{
    public class DownloadData
    {
        public string FileName { get; set; }

        public string Url { get; set; }

        public int DownloadCount { get; set; }
    }
}
