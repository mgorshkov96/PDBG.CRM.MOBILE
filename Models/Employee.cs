using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDBG.CRM.MOBILE.Models
{
	public class Employee
	{
		public int Id { get; set; }
		
		public int? AmoId { get; set; }
		
		public int RoleId { get; set; }

		public string Name { get; set; }

		public string? Phone { get; set; }
		
		public int? StatusId { get; set; }

		public int Access { get; set; }
	}
}
