using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public  interface IMismatchFinder
    {
        IEnumerable<String> GetMismatches(IEnumerable<String> set1, IEnumerable<String> set2);
    }
}
