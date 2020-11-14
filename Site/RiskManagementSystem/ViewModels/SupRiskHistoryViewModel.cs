using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiskManagementSystem.ViewModels
{
    public class SupRiskHistoryViewModel
    {
        public string OperationGroupTitle { get; set; }
        public string OperationTitle { get; set; }
        public string ActTitle { get; set; }
        public string StageTitle { get; set; }
        public int StageId { get; set; }

        public string CompanyTitle { get; set; }
        public int CompanyUserId { get; set; }
        public string Type { get; set; }
    }
}