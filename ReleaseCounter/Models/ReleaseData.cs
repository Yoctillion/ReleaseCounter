using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseCounter.Models
{
    public class ReleaseData
    {
        public string TagName { get; set; }

        public string ReleaseName { get; set; }

        public string Author { get; set; }

        public string AuthorUrl { get; set; }

        public string ReleaseTime { get; set; }

        public DownloadData[] Downloads { get; set; }
    }
}
