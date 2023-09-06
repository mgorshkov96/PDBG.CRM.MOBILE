using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDBG.CRM.MOBILE.Models
{
    public class AgentSearch
    {
        public int LeadId { get; set; }
        public int AgentId { get; set; }
        public DateTime SearchTime { get; set; }
        public double Distance { get; set; }
        public Lead? Lead { get; set; }
    }
}
