using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public class MismatchFinder : IMismatchFinder
    {
        public IEnumerable<string> GetMismatches(IEnumerable<string> set1, IEnumerable<string> set2)
        {
            return set1.Where(s1 => !set2.Contains(s1, StringComparer.CurrentCultureIgnoreCase));
        }
    }
}
