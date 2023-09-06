using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PDBG.CRM.MOBILE.Models
{
	public class Lead
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		
		public int StatusId { get; set; }
		
		public int DispId { get; set; }
		
		public int? AgentId { get; set; }
		
		public int ClientId { get; set; }

		public string? Dead { get; set; }

		public string? Address { get; set; }

		public decimal? Lat { get; set; }

		public decimal? Lng { get; set; }
		
		public string? NoteToAddress { get; set; }

		public string? Comment { get; set; }

		public decimal? Sum { get; set; }
		
		public string? RejectionReason { get; set; }

		public Employee? Disp { get; set; }
		public Employee? Agent { get; set; }		
		public Client? Client { get; set; }
	}
}
