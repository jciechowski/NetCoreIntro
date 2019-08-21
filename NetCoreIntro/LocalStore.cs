using System.Collections.ObjectModel;

namespace NetCoreIntro
{
    public static class LocalStore
    {
        public static ObservableCollection<string> Messages {get; } = new ObservableCollection<string>();
    }
}