using CommunityToolkit.Maui.Views;
using PDBG.CRM.MOBILE.ViewModels;
using PDBG.CRM.MOBILE.Models;

namespace PDBG.CRM.MOBILE;

public partial class LeadInWork : ContentPage
{
	private int leadId;
	private int userId;
	private Lead pageLead;

	public LeadInWork(int leadId, int userId)
	{
		InitializeComponent();
		this.leadId = leadId;
		this.userId = userId;
	}

	private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		var lead = await PDBGApiService.GetLeadByIdAsync(leadId);
		pageLead = lead;
		Header.Text = $"������ � {lead.Id}";
		Created.Children.Add(new Label { Text = $"{lead.Created.ToString("dd.MM.yyyy HH:mm")}" });
		Disp.Children.Add(new Label { Text = $"{lead.Disp.Name}" });
		Client.Children.Add(new Label { Text = $"{lead.Client.Name}" });
		ClientPhone.Children.Add(new Label { Text = $"+{lead.Client.Phone}" });
		Dead.Children.Add(new Label { Text = $"{lead.Dead}" });
		Address.Children.Add(new Label { Text = $"{lead.Address}" });
		NoteToAddress.Children.Add(new Label { Text = $"{lead.NoteToAddress}" });
		Comment.Children.Add(new Label { Text = $"{lead.Comment}" });
	}

	private async void CloseLead_Clicked(object sender, EventArgs e)
	{
		var popup = new CloseLeadPopup();
		var resultData = await this.ShowPopupAsync(popup);
		var result = (CloseLeadPopupVM)resultData;

		switch (result.IndexId)
		{
			case -1:
				{
					await DisplayAlert("����������� �� ������", "�� ������� ��������� ������, �.�. ����� ���� ������� � �������� �������.", "�K");
					break;
				}
			case 0:
				{
					pageLead.Sum = result.Sum;
					pageLead.StatusId = 2;
					//await DisplayAlert("����������� �� ������", result.Sum.ToString(), "�K");
					break;
				}
			case 1:
				{
					pageLead.RejectionReason = result.RejectionReason;
					pageLead.StatusId = 3;
					//await DisplayAlert("����������� �� ������", result.RejectionReason, "�K");
					break;
				}
		}

		var IsSaveSuccess = await PDBGApiService.CloseLead(pageLead);

		if (!IsSaveSuccess)
		{
			await DisplayAlert("����������� �� ������", "�� ������� ��������� ������, ���������� ��� ���.", "�K");
			return;
		}

		await Navigation.PopAsync();
	}
}