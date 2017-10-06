using System;
using Microsoft.Build.Evaluation;

namespace LocalDeployProjectUpdater
{
    public class ProjectFileValidator : IValidator
    {
        public string Validate(string value)
        {
            String message;
            Project proj;
            try
            {
                proj = new Project(value);
                message = String.Empty;
            }
            catch (Exception ex)
            {
                message = Constants.MessageText.NotAValidProjectFile + " - " + value + " " + ex.ToString();
            }

            return message;
            
        }
    }
}
