using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility      
{
    public class ProjectDifferencesProvider : IProjectDifferencesProvider
    {
        IMismatchFinder _MismatchFinder;

        public ProjectDifferencesProvider() : this(null) { }

        public ProjectDifferencesProvider(IMismatchFinder mismatchFinder) {
            if (mismatchFinder != null)
                _MismatchFinder = mismatchFinder;
            else
                _MismatchFinder = new MismatchFinder();
        }

        public ProjectDifferences GetDifferences(IEnumerable<string> csProjectFileNames, IEnumerable<string> vfpProjectFileNames)
        {
            IEnumerable<String> vfpFiles = ScrubFileNames(vfpProjectFileNames);
            IEnumerable<String> csFiles = ScrubFileNames(csProjectFileNames);
            ProjectDifferences projDiff = new ProjectDifferences();
            projDiff.Additions = _MismatchFinder.GetMismatches(vfpFiles, csFiles);
            projDiff.Deletions = _MismatchFinder.GetMismatches(csFiles,vfpFiles);
            return projDiff;
        }

        IEnumerable<String> ScrubFileNames(IEnumerable<String> fileNames) {
            return fileNames.Select(fileName => Path.GetFileName(fileName));
        }


    }
}
