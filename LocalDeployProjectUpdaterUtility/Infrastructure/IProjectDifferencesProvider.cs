using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public interface IProjectDifferencesProvider
    {
        ProjectDifferences GetDifferences(IEnumerable<String> csProjectFileNames, IEnumerable<String> vfpProjectFileNames);
    }
}
