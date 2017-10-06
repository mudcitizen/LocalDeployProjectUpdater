using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdaterUtility
{
    public interface IValidator
    {
        String Validate(String value);
    }
}
