using PropertyChanged;

namespace SarahsBakeryApp._0_Models
{
    [AddINotifyPropertyChangedInterface]
    public class TheTasks
    {
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        public string TaskColor { get; set; }
    }
}
