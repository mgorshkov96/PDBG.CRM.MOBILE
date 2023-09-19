using System.Net.Http;
using PDBG.CRM.MOBILE.Models;

namespace PDBG.CRM.MOBILE;

public partial class LoginPage : ContentPage
{
	private MainPage _main;
	public LoginPage(MainPage main)
	{
		InitializeComponent();	
		_main = main;
	}

	private async void EnterBtnClicked(object sender, EventArgs e)
	{
		string login = LoginEntry.Text;
		string password = PasswordEntry.Text;

		try
		{
			var agent = await PDBGApiService.LoginAsync(login, password);
			_main.User = agent;
			await Navigation.PopModalAsync();
		}
		catch (HttpRequestException)
		{
			await DisplayAlert("Ошибка авторизации", "Войти не удалось, проверьте логин и пароль.", "Ок");
		}				
	}
}

