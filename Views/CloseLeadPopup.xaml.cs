using CommunityToolkit.Maui.Views;
using PDBG.CRM.MOBILE.ViewModels;

namespace PDBG.CRM.MOBILE;

public partial class CloseLeadPopup : Popup
{
	public CloseLeadPopup()
	{
		InitializeComponent();
	}

	private void selectStatus_SelectedIndexChanged(object sender, EventArgs e)
	{		
		switch (selectStatus.SelectedIndex)
		{
			case 0:
				{
					this.sumSpace.IsVisible = true;
					this.reasonSpace.IsVisible = false;
					break;
				}
			case 1:
				{
					this.sumSpace.IsVisible = false;
					this.reasonSpace.IsVisible = true;
					break;					
				}
		}		
	}

	private async void closePopup_Clicked(object sender, EventArgs e)
	{
		await CloseAsync();
	}

	private void SaveLeadPopup_Clicked(object sender, EventArgs e)
	{
		var popupVM = new CloseLeadPopupVM(selectStatus.SelectedIndex);

		switch (selectStatus.SelectedIndex)			
		{
			case 0:
				{
					decimal sum;
					bool IsDecimal = Decimal.TryParse(LeadPopupSum.Text, out sum);
					if (IsDecimal)
					{
						popupVM.Sum = sum;
					}
					else
					{
						popupVM.IndexId = -1;
						Close(popupVM);
						return;
					}					
					break;
				}
			case 1:
				{
					popupVM.RejectionReason = LeadPopupRejReason.Text; 
					break;
				}			
		}

		Close(popupVM);
	}
}