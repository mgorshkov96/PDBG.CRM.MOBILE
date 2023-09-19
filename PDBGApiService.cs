using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PDBG.CRM.MOBILE.Models;

namespace PDBG.CRM.MOBILE
{
	public static class PDBGApiService
	{
		private static HttpClient httpClient = new HttpClient();

		public static async Task<Employee> LoginAsync(string login, string password)
		{
			var agent = await httpClient.GetFromJsonAsync<Employee>($"https://pdbg-crm.ru/auth?login={login}&password={password}");

			return agent;
		}

		public static async Task<List<AgentSearch>>? GetLeadsNearAsync(int id)
		{
			var leads = await httpClient.GetFromJsonAsync<List<AgentSearch>>($"https://pdbg-crm.ru/api/leads/near?agentId={id}");
			return leads;
		}

		public static async Task<List<Lead>>? GetLeadsInWorkAsync(int id)
		{
			var leads = await httpClient.GetFromJsonAsync<List<Lead>>($"https://pdbg-crm.ru/api/leads/inwork?agentId={id}");
			return leads;
		}

		public static async Task<Lead>? GetLeadByIdAsync(int id)
		{
			var lead = await httpClient.GetFromJsonAsync<Lead>($"https://pdbg-crm.ru/api/leads?id={id}");
			return lead;
		}

		public static async Task<bool> AppointLead(int agentId, int leadId)
		{
			var result = await httpClient.GetAsync($"https://pdbg-crm.ru/api/leads/appoint?agentId={agentId}&leadId={leadId}");

			if (!result.IsSuccessStatusCode)
			{
				return false;
			}
			
			return true;
		}

		public static async Task<bool> CloseLead(Lead lead)
		{
			JsonContent content = JsonContent.Create(lead);

			var result = await httpClient.PutAsync($"https://pdbg-crm.ru/api/leads", content);

			if (!result.IsSuccessStatusCode)
			{
				return false;
			}

			return true;
		}

		public static async Task<bool> AddLocation(LocationLog locationLog)
		{
			JsonContent content = JsonContent.Create(locationLog);

			var result = await httpClient.PostAsync($"https://pdbg-crm.ru/api/location", content);

			if (!result.IsSuccessStatusCode)
			{
				return false;
			}

			return true;
		}

		public static async Task ChangeAgentStatus(Employee agent)
		{
			JsonContent content = JsonContent.Create(agent);
			await httpClient.PutAsync($"https://pdbg-crm.ru/api/employees", content);
		}
	}
}
