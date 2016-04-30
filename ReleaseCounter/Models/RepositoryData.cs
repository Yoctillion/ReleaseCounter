using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseCounter.Models
{
    public class RepositoryData
    {
        public string Author { get; set; }

        public string Name { get; set; }

        public ReleaseData[] Releases { get; set; }

        public int DownloadCount { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public string ToFullName()
        {
            return $"{this.Author}.{this.Name}";
        }
    }
}
