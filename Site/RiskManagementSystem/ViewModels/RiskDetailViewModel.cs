using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiskManagementSystem.ViewModels
{
    public class RiskDetailViewModel
    {
        public int UniqueId { get; set; }
        public string RiskTitle { get; set; }
        public string RiskIntensityTitle { get; set; }
        public string RiskProbabilityTitle { get; set; }
        public string RiskEvaluationTitle { get; set; }
        public string StatusTitle { get; set; }
        public DateTime SubmitDate { get; set; }

        public string RiskAfterProbabilityTitle { get; set; }
        public string RiskAfterIntensityTitle { get; set; }
        public string RiskAfterEvaluationTitle { get; set; }
        public string CompanyName { get; set; }
        public int UserRiskID { get; set; }


    }
}