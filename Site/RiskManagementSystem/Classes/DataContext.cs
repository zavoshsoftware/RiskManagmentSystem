using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Classes
{
    public class DataContext
    {
        public static RiskManagementEntities Context = new RiskManagementEntities();
    }
}