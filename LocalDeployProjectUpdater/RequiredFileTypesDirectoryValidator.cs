﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeployProjectUpdater
{
    public class RequiredFileTypesDirectoryValidator : IValidator
    {

        IEnumerable<String> _RequiredFileTypes;
        public RequiredFileTypesDirectoryValidator(IEnumerable<String> requiredFileTypes)
        {
            _RequiredFileTypes = requiredFileTypes;
        }
        public string Validate(string value)
        {
            String msg = String.Empty;
            IEnumerable<String> fileTypes = Directory
                .GetFiles(value)
                .Select(fileName => Path.GetExtension(fileName).ToUpper())
                .Distinct()
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (String fileType in _RequiredFileTypes)
                if (!fileTypes.Contains("."+fileType, StringComparer.CurrentCultureIgnoreCase))
                    sb.Append(fileType + " ");

            String missingFileTypes = sb.ToString();

            if (!String.IsNullOrEmpty(missingFileTypes))
                msg = Constants.MessageText.RequiredFileTypesNotPresent + " - " + missingFileTypes;

            return msg;
        }
    }
}
