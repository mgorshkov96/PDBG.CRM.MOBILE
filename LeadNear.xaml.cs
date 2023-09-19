namespace PDBG.CRM.MOBILE;

public partial class LeadNear : ContentPage
{
	private int leadId;
	private int userId;
	public LeadNear(int leadId, int userId)
	{
		InitializeComponent();
		this.leadId = leadId;
		this.userId = userId;
	}

	private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		try
		{
			var lead = await PDBGApiService.GetLeadByIdAsync(leadId);
			Header.Text = $"Заявка № {lead.Id}";
			Created.Children.Add(new Label { Text = $"{lead.Created.ToString("dd.MM.yyyy HH:mm")}" });
			Disp.Children.Add(new Label { Text = $"{lead.Disp.Name}" });
			Client.Children.Add(new Label { Text = $"{lead.Client.Name}" });
			Dead.Children.Add(new Label { Text = $"{lead.Dead}" });
			Address.Children.Add(new Label { Text = $"{lead.Address}" });
			NoteToAddress.Children.Add(new Label { Text = $"{lead.NoteToAddress}" });
			Comment.Children.Add(new Label { Text = $"{lead.Comment}" });
		}
		catch (HttpRequestException ex)
		{
			await DisplayAlert("Уведомление об ошибке", $"При загрузке страницы произошла ошибка. Подробности: {ex.Message}", "Ок");
		}		
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		var isSuccess = await PDBGApiService.AppointLead(userId, leadId);

		if (isSuccess)
		{
			await Navigation.PopAsync();
		}
		else
		{
			await DisplayAlert("Уведомление", "Не удалось принять заявку", "ОK");
		}
	}
}