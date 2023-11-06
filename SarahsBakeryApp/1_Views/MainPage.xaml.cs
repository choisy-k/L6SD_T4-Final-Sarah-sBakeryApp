using SarahsBakeryApp._0_Models;
using SarahsBakeryApp._1_Views;
using SarahsBakeryApp._2_ViewModels;

namespace SarahsBakeryApp
{
    public partial class MainPage : ContentPage
    {
        // initialise the MainViewModel
        MainViewModel vm = new MainViewModel();
        public MainPage()
        {
            InitializeComponent();
            //create a binding context for this page
            BindingContext = vm;
        }

        private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            TheTasks task = (TheTasks)checkBox.BindingContext;

            // Update the IsCompleted property based on the CheckBox state
            task.IsCompleted = e.Value;

            //calling the updating function
            vm.UpdateData();
        }

        private void addBtn_Clicked(object sender, EventArgs e)
        {
            // navigate to the second page, binding the page with NewTaskViewModel
            var taskView = new NewTaskPage()
            {
                BindingContext = new NewTaskViewModel
                {
                    Tasks = vm.Tasks,
                    Categories = vm.Categories
                }
            };
            //navigation
            if (taskView != null)
            {
                Navigation.PushAsync(taskView);
            }
            else
            {
                DisplayAlert("Error!", "Loading this page", "Ok");
            }
        }
    }
}