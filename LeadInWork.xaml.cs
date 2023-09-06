namespace PDBG.CRM.MOBILE;

public partial class LeadInWork : ContentPage
{
	private int leadId;
	public LeadInWork(int leadId)
	{
		InitializeComponent();
		this.leadId = leadId;		
	}

	private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
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
}