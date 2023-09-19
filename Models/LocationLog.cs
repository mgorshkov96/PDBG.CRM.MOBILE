using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDBG.CRM.MOBILE.Models
{
	public class LocationLog
	{
		public int EmployeeId { get; set; }
		public decimal Lat { get; set; }
		public decimal Lng { get; set; }

		public LocationLog(int employeeId, decimal lat, decimal lng)
		{
			EmployeeId = employeeId;
			Lat = lat;
			Lng = lng;
		}
	}	
}
