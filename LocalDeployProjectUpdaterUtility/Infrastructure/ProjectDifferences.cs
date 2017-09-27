using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class ProjectDifferences
    {
        public IEnumerable<String> Additions { get; set; }
        public IEnumerable<String> Deletions { get; set; }
    }
}
