
using CommunityToolkit.Maui.Views;
using MauiAppToDo.ViewModel;


namespace MauiToolkitPopupSample;

public partial class PopupPageAddToDo :Popup
{

    public PopupPageAddToDo()
	{
        InitializeComponent();
        //BindingContext = viewModel;

        //viewModel.InitializeAsync();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
	Close();	
    }
}