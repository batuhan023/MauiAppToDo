using CommunityToolkit.Maui.Views;
using MauiAppToDo.Models;
using MauiAppToDo.Services;
using MauiAppToDo.ViewModel;
using MauiToolkitPopupSample;

namespace MauiAppToDo;

public partial class ToDoPage : ContentPage
{

    public ToDoPage(MainViewModel viewModel)
	{
		InitializeComponent();
        BindingContext= viewModel;

        viewModel.InitializeAsync();


        //preferences de kaydolan veriler çaðýrýlýr 
        string  userName = Preferences.Get("UserName",string.Empty);
        string userEmail = Preferences.Get("UserEmail", string.Empty);
           
        LblUserName.Text = userName;  
        LblUserEmail.Text = userEmail;
    }
    //private async void BtnClickedPopup(object sender, EventArgs e)
    //{
    //    var popupPage = new PopupPageAddToDo();
    //    //await this.ShowPopupAsync(popupPage);
    //    await Navigation.(popupPage);
    //}
    private void BtnClickedPopup(object sender, EventArgs e)
    {
        this.ShowPopup(new PopupPageAddToDo());
    }
}