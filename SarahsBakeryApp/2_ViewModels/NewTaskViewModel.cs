using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SarahsBakeryApp._0_Models;
using System.Collections.ObjectModel;

namespace SarahsBakeryApp._2_ViewModels
{
    public partial class NewTaskViewModel : ObservableObject
    {
        private MainViewModel mainVM = new MainViewModel();

        public NewTaskViewModel()
        {
        }

        [ObservableProperty]
        ObservableCollection<TheTasks> tasks;
        [ObservableProperty]
        string task;

        [ObservableProperty]
        ObservableCollection<Category> categories;
        [ObservableProperty]
        Category selectedCategory;
        [ObservableProperty]
        string categoryName;
        [ObservableProperty]
        string categoryColor;
        [ObservableProperty]
        bool isSelected;

        [RelayCommand]
        public async void AddTask()
        {
            if (string.IsNullOrEmpty(Task))
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Task", "Please enter your task", "OK");
            }
            else
            {
                var selectedCategory = Categories.Where(x => x.IsSelected).FirstOrDefault();
                CategoryColor = selectedCategory.CategoryColor;

                if (selectedCategory == null)
                {
                    await App.Current.MainPage.DisplayAlert("Invalid Selection", "Please select a category", "OK");
                }
                else
                {
                    var newTask = new TheTasks
                    {
                        TaskName = Task,
                        IsCompleted = false,
                        CategoryId = selectedCategory.Id,
                        TaskColor = selectedCategory.CategoryColor
                    };
                    Tasks.Add(newTask);
                    mainVM.UpdateData();
                    Task = string.Empty;

                    // navigate back to main page
                    var navigation = Application.Current.MainPage.Navigation;
                    await navigation.PopAsync();
                }
            }
        }

        [RelayCommand]
        public async void AddCategory()
        {
            CategoryName = await Application.Current.MainPage.DisplayPromptAsync("New category", "Write your category name", maxLength: 50, keyboard: Keyboard.Text);

            if (string.IsNullOrEmpty(CategoryName)) return;
            else
            {
                //create random instance for generating color
                var random = new Random();

                var newCategory = new Category
                {
                    Id = Categories.Max(x => x.Id) + 1,
                    CategoryColor = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)).ToHex(),
                    CategoryName = CategoryName
                };
                Categories.Add(newCategory);
                mainVM.UpdateData();
            }
        }
    }
}
