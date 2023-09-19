using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDBG.CRM.MOBILE.ViewModels
{
    public class CloseLeadPopupVM
    {
        public int IndexId { get; set; }
        public string RejectionReason { get; set; }
        public decimal? Sum { get; set; }

        public CloseLeadPopupVM(int indexId)
        {
            this.IndexId = indexId;            
        }		
	}
}
