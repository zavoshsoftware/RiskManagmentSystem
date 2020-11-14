using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiskManagementSystem.ViewModels
{
    public class RiskHistoryViewModel
    {
        public string OperationGroupTitle { get; set; }
        public string OperationTitle { get; set; }
        public string ActTitle { get; set; }
        public string StageTitle { get; set; }
        public int StageId { get; set; }
        public int UserRiskId { get; set; }

        public string RiskTitle { get; set; }
    }
}