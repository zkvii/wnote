using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Wnote.Helpers;
using TreeView = Microsoft.UI.Xaml.Controls.TreeView;
using TreeViewItemInvokedEventArgs = Microsoft.UI.Xaml.Controls.TreeViewItemInvokedEventArgs;
using TreeViewNode = Microsoft.UI.Xaml.Controls.TreeViewNode;


namespace Wnote.Views
{
    // public class NavLink
    // {
    //     public string Label { get; set; }
    //     public Symbol Symbol { get; set; }

    //
    // }
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
      
   
        // public ObservableCollection<NavLink> NavLinks { get; } = new ObservableCollection<NavLink>()
        // {
        //     new NavLink() { Label = "NoteOne", Symbol = Symbol.Folder  },
        //     new NavLink() { Label = "NoteTwo", Symbol = Symbol.Folder },
        //     new NavLink() { Label = "NoteThree", Symbol = Symbol.Folder },
        //     new NavLink() { Label = "NoteFour", Symbol = Symbol.Folder },
        // };

        public MainPage()
        {
            InitializeComponent();
            SplitPropertyHelper.Initialize(this,LeftPaneView,RightPaneView);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }
        // private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        // {
        //     content.Text = (e.ClickedItem as NavLink)?.Label + " Page";
        // }


        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void TabView_AddButtonClick(TabView sender, object args)
        {
            throw new NotImplementedException();
        }

        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            throw new NotImplementedException();
        }

    }

}
