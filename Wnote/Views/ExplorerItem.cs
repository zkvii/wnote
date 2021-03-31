using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Wnote.Views
{
    public class ExplorerItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public enum ExplorerItemType
        {
            Folder,
            File
        };

        public string Name { get; set; }
        public ExplorerItemType Type { get; set; }
        private ObservableCollection<ExplorerItem> _mChildren;

        public ObservableCollection<ExplorerItem> Children
        {
            get => _mChildren ?? (_mChildren = new ObservableCollection<ExplorerItem>());
            set => _mChildren = value;
        }

        private bool _mIsExpanded;

        public bool IsExpanded
        {
            get => _mIsExpanded;
            set
            {
                if (_mIsExpanded == value) return;
                _mIsExpanded = value;
                NotifyPropertyChanged("IsExpanded");
            }
        }

        private bool _mIsSelected;

        public bool IsSelected
        {
            get => _mIsSelected;

            set
            {
                if (_mIsSelected == value) return;
                _mIsSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            // Check for same reference  
            if (ReferenceEquals(this, obj))
                return true;
            var genItem = (ExplorerItem) obj;
            return this.Name == genItem.Name && this.Type == genItem.Type;
        }
    }
}
