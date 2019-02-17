using System.Collections.ObjectModel;

namespace NetCoreIntro
{
    public class LocalStore
    {
        public ObservableCollection<string> Messages {get; set; }

        public LocalStore()
        {
            Messages = new ObservableCollection<string>();
        }
    }
}