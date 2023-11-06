using PropertyChanged;
using System.Collections.ObjectModel;

namespace SarahsBakeryApp._0_Models
{
    [AddINotifyPropertyChangedInterface]
    public class Category
    {
        // establish the variables needed for ViewModel
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryColor { get; set; }
        public bool IsSelected { get; set; }
        public ObservableCollection<TheTasks> Tasks { get; set; }
        public float Percentage { get; set; }
        public int CompletedTask { get; set; }

        //store the actual value of TotalTasks.
        //This allows access and modification control of the value
        private int totalTasks;
        public int TotalTasks
        {
            get { return totalTasks; }
            set
            {
                if (totalTasks != value)
                {
                    totalTasks = value;
                }
            }
        }
        public void UpdateTotalTasks()
        {
            TotalTasks = Tasks.Count;
        }

        public Category()
        {
            //initialise Task collection
            Tasks = new ObservableCollection<TheTasks>();
            UpdateTotalTasks();
        }

        public void AddTask(TheTasks task)
        {
            Tasks.Add(task);
            UpdateTotalTasks();
        }
    }
}
