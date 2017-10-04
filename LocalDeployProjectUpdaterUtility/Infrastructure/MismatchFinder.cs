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

            /* We want things that are in Set1 that are not in Set2
             * 
             */
            

            if ((set1 == null) && (set2 == null))
                return new List<String>();

            if ((set1 != null) && (set2 == null))
                return set1;

            if (set2 == null) {
                if (set1 == null)
                    return new List<String>();
                else
                    return set1;
            }

            // set2 is not null
            if (set1 == null)
                return new List<String>();
            
            return set1.Where(s1 => !set2.Contains(s1, StringComparer.CurrentCultureIgnoreCase));
        }
    }
}
