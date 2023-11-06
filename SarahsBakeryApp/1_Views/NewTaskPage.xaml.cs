using SarahsBakeryApp._2_ViewModels;

namespace SarahsBakeryApp._1_Views;

public partial class NewTaskPage : ContentPage
{
	public NewTaskPage()
	{
		InitializeComponent();
		BindingContext = new NewTaskViewModel();
	}
}