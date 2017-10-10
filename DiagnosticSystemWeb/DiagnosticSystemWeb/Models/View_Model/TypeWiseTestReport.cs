using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticSystemWeb.Models.View_Model
{
    public class TypeWiseTestReport
    {
        public string TypeName { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalFee { get; set; }

    }
}