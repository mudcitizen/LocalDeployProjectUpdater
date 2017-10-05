using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{

    public class HostModuleParametersProvider : IModuleParametersProvider
    {
        public ModuleParameters GetModuleParameters()
        {
            ModuleParameters parms = new ModuleParameters();
            parms.ContentSubFolder = "H";

            IEnumerable<String> excludedFiles = new List<String>() {
                "IN-FOLHD.FXP",
                "IN-FOLLN.FXP",
                "IN-FOLFT.FXP",
                "IN-PRG.FXP",
                "IN-PLBL.FXP",
                "IN-MSGFM.FXP",
                "IN-FCRCT.FXP",
                "IN-FOLSM.FXP",
                "GC-GIFRM.FXP",
                "IN-KLOCK.FXP",
                "IN-VCHFM.FXP",
                "BC-VOUC.FXP",
                "RS-ZVCR.FXP"
            };

            parms.ExcludedFiles = excludedFiles.OrderBy(fileName => fileName).ToList();
            parms.ProductName = "Host";
            return parms;
        }

    }
}
