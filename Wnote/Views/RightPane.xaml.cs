using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Wnote.Views
{
    public sealed partial class RightPane : UserControl
    {
        public RightPane()
        {
            this.InitializeComponent();
        }
        private void TabView_AddButtonClick(TabView sender, object args)
        {
    

            sender.TabItems.Add(CreateNewTab(null));

        }

        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        public void OpenFile(ExplorerItem explorerItem)
        {
           TabViewPane.TabItems.Insert(0,CreateNewTab(explorerItem));
        }
        private TabViewItem CreateNewTab(ExplorerItem item)
        {
            TabViewItem newItem = new TabViewItem();

            newItem.Header = "New Document";
            newItem.IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Document };

            // The content of the tab is often a frame that contains a page, though it could be any UIElement.
            Frame frame = new Frame();
            if (item != null) newItem.Header = item.Name;

            newItem.Content = frame;

            return newItem;
        }
    }
}
