using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PropertyChanged;
using SarahsBakeryApp._0_Models;
using System.Collections.ObjectModel;

namespace SarahsBakeryApp._2_ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            // hardcoded data to test the features
            DummyData();

            //initialise task collection within each categories
            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<TheTasks>(Tasks.Where(task => task.CategoryId == category.Id));
                category.UpdateTotalTasks();
            }
        }

        // get all the variables from the models
        [ObservableProperty]
        ObservableCollection<Category> categories;
        [ObservableProperty]
        Category selectedCategory;

        [ObservableProperty]
        ObservableCollection<TheTasks> tasks;

        [ObservableProperty]
        string categoryName;
        [ObservableProperty]
        string categoryColor;
        [ObservableProperty]
        int completedTask;
        [ObservableProperty]
        int totalTasks;
        [ObservableProperty]
        bool isSelected;
        [ObservableProperty]
        float percentage;

        [ObservableProperty]
        string taskName;
        [ObservableProperty]
        bool isCompleted;
        [ObservableProperty]
        int categoryId;
        [ObservableProperty]
        string taskColor;

        //updating data everytime changes are made
        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.CategoryId == c.Id
                            select t;

                var completed = from t in tasks
                                where t.IsCompleted == true
                                select t;

                var noCompleted = from t in tasks
                                  where t.IsCompleted == false
                                  select t;
                c.CompletedTask = completed.Count();
                c.TotalTasks = tasks.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor =
                    (
                        from c in Categories
                        where c.Id == t.CategoryId
                        select c.CategoryColor
                    ).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }

        // editing selected task
        [RelayCommand]
        async void EditTask(TheTasks task)
        {
            if (Tasks.Contains(task))
            {
                string editLabel = await App.Current.MainPage.DisplayPromptAsync("Edit", "Write the new task name");
                task.TaskName = editLabel;
                UpdateData();
            }
        }

        // deleting selected task
        [RelayCommand]
        void DeleteTask(TheTasks task)
        {
            if (Tasks.Contains(task))
            {
                Tasks.Remove(task);
                UpdateData();
            }
        }

        void DummyData()
        {
            Categories = new ObservableCollection<Category>()
            {
                new Category
                {
                    Id = 0,
                    CategoryName = "Baked goods",
                    CategoryColor = "#00A36C"
                },
                new Category
                {
                    Id = 1,
                    CategoryName = "Daily tasks",
                    CategoryColor = "#8F00FF"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Paperwork",
                    CategoryColor = "#72674e"
                },
            };

            Tasks = new ObservableCollection<TheTasks>()
            {
                new TheTasks
                {
                    TaskName = "25 fairy bread",
                    IsCompleted = false,
                    CategoryId = 0,
                },
                new TheTasks
                {
                    TaskName = "10 sourdough",
                    IsCompleted = false,
                    CategoryId = 0,
                },
                new TheTasks
                {
                    TaskName = "Clean table in the south section",
                    IsCompleted = false,
                    CategoryId = 1,
                },
                new TheTasks
                {
                    TaskName = "15 brioche",
                    IsCompleted = false,
                    CategoryId = 0,
                },
                new TheTasks
                {
                    TaskName = "Talk to Sarah about the monthly report",
                    IsCompleted = false,
                    CategoryId = 2,
                },
                new TheTasks
                {
                    TaskName = "Rearrange the interior decoration",
                    IsCompleted = false,
                    CategoryId = 1,
                },
            };
            UpdateData();
        }
    }
}
