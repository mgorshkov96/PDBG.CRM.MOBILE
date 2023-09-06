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

	protected override void OnAppearing()
	{
		this.Focus();		
	}

	public async Task UpdateLeads()
	{
		leadsNear.Children.Clear();
		leadsInWork.Children.Clear();
		var nearestLeads = await PDBGApiService.GetLeadsNearAsync(User.Id);
		var workingLeads = await PDBGApiService.GetLeadsInWorkAsync(User.Id);

		foreach (var lead in nearestLeads)
		{
			var vertLayout = new VerticalStackLayout
			{
				BackgroundColor = Colors.LightSalmon,
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

		foreach (var lead in workingLeads)
		{
			var vertLayout = new VerticalStackLayout
			{
				BackgroundColor = Colors.LightSalmon,
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

	private void BtnWorkLead_Clicked(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		int id = Int32.Parse(btn.ClassId);
		LeadInWork lead = new LeadInWork(id);
		Navigation.PushAsync(lead);
	}
	private void BtnNearLead_Clicked(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		int id = Int32.Parse(btn.ClassId);
		LeadInWork lead = new LeadInWork(id);
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
}

