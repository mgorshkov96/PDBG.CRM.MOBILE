using Microsoft.Maui.Graphics;
using PDBG.CRM.MOBILE.Models;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PDBG.CRM.MOBILE;

public partial class MainPage : ContentPage
{
	public Employee User { get; set; }
	public MainPage()
	{
		InitializeComponent();		
		LoginPage login = new LoginPage(this);
		Navigation.PushModalAsync(login);
	}	

	public async Task UpdateLeads()
	{
		leadsNear.Children.Clear();
		leadsInWork.Children.Clear();

		try
		{
			var nearestLeads = await PDBGApiService.GetLeadsNearAsync(User.Id);
			var workingLeads = await PDBGApiService.GetLeadsInWorkAsync(User.Id);

			if (nearestLeads.Count == 0)
			{
				leadsNear.Children.Add(new Label { Text = "Заявок рядом не найдено" });
			}
			else
			{
				foreach (var lead in nearestLeads)
				{
					var vertLayout = new VerticalStackLayout
					{
						BackgroundColor = Colors.LightGray,
						Padding = 8,
						Margin = new Thickness(0, 5, 0, 10),
						Children = {
					new Label {Text = $"Заявка № {lead.Lead.Id} от {lead.Lead.Created.ToString("dd.MM.yyyy HH:mm")}"},
					new Label {Text = $"Адрес: {lead.Lead.Address}"}
				}
					};
					var btn = new Button();
					btn.Clicked += BtnNearLead_Clicked;
					btn.Text = "Подробнее";
					btn.VerticalOptions = LayoutOptions.Start;
					btn.Margin = 5;
					btn.ClassId = lead.Lead.Id.ToString();
					vertLayout.Children.Add(btn);
					leadsNear.Children.Add(vertLayout);
				}
			}

			if (workingLeads.Count == 0)
			{
				leadsInWork.Children.Add(new Label { Text = "Заявки в работе отсутствуют" });
			}
			else
			{
				foreach (var lead in workingLeads)
				{
					var vertLayout = new VerticalStackLayout
					{
						BackgroundColor = Colors.LightGray,
						Padding = 8,
						Margin = new Thickness(0, 5, 0, 10),
						Children = {
					new Label { Text = $"Заявка № {lead.Id} от {lead.Created.ToString("dd.MM.yyyy HH:mm")}" },
					new Label { Text = $"Умерший: {lead.Dead}" },
				}
					};
					var btn = new Button();
					btn.Clicked += BtnWorkLead_Clicked;
					btn.Text = "Подробнее";
					btn.VerticalOptions = LayoutOptions.Start;
					btn.Margin = 5;
					btn.ClassId = lead.Id.ToString();
					vertLayout.Children.Add(btn);
					leadsInWork.Children.Add(vertLayout);
				}
			}
		}
		catch (HttpRequestException ex)
		{
			await DisplayAlert("Уведомление об ошибке", $"При загрузке заявок произошла ошибка. Подробности: {ex.Message}", "Ок");
		}
					
	}

	private void BtnWorkLead_Clicked(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		int id = Int32.Parse(btn.ClassId);
		LeadInWork lead = new LeadInWork(id, User.Id);
		Navigation.PushAsync(lead);
	}
	private void BtnNearLead_Clicked(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		int id = Int32.Parse(btn.ClassId);
		LeadNear lead = new LeadNear(id, User.Id);
		Navigation.PushAsync(lead);
	}

	private async void Refresh_Clicked(object sender, EventArgs e)
	{
		await UpdateLeads();
	}

	private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		await UpdateLeads();
	}

	private async void agentStatus_Toggled(object sender, ToggledEventArgs e)
	{			
		try
		{
			if (agentStatus.IsToggled)
			{
				User.StatusId = 1;
				await PDBGApiService.ChangeAgentStatus(User);

				while (agentStatus.IsToggled)
				{
					GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(30));
					Location location = await Geolocation.Default.GetLocationAsync(request);
					Decimal lat = (Decimal)location.Latitude;
					Decimal lng = (Decimal)location.Longitude;
					LocationLog locationLog = new LocationLog(User.Id, lat, lng);
					await PDBGApiService.AddLocation(locationLog);

					await Task.Delay(30000);
				}
			}
			else
			{
				User.StatusId = 2;
				await PDBGApiService.ChangeAgentStatus(User);
			}			
		}
		catch (HttpRequestException ex)
		{
			await DisplayAlert("Уведомление об ошибке", $"При отправке местоположения произошла ошибка. Подробности: {ex.Message}", "Ок");
		}
	}	
}

