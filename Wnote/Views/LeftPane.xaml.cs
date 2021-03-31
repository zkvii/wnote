using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Wnote.Helpers;
using TreeView = Microsoft.UI.Xaml.Controls.TreeView;
using TreeViewItemInvokedEventArgs = Microsoft.UI.Xaml.Controls.TreeViewItemInvokedEventArgs;
using TreeViewNode = Microsoft.UI.Xaml.Controls.TreeViewNode;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Wnote.Views
{
    public sealed partial class LeftPane : UserControl
    {
        private TreeViewNode _personalFolder;

        // private TreeViewNode personalFolder1;
        private ObservableCollection<ExplorerItem> _dataSource;

        public LeftPane()
        {
            this.InitializeComponent();
            this.DataContext = this;
            _dataSource = GetData();
            InitializeNotesTreeView();
            // InitializeSampleTreeView2();
        }

        private void InitializeNotesTreeView()
        {
            var workFolder = new TreeViewNode {Content = "Work Documents", IsExpanded = true};

            workFolder.Children.Add(new TreeViewNode {Content = "XYZ Functional Spec"});
            workFolder.Children.Add(new TreeViewNode {Content = "Feature Schedule"});
            workFolder.Children.Add(new TreeViewNode {Content = "Overall Project Plan"});
            workFolder.Children.Add(new TreeViewNode {Content = "Feature Resources Allocation"});

            var remodelFolder = new TreeViewNode {Content = "Home Remodel", IsExpanded = true};

            var item = new TreeViewNode {Content = "Contractor Contact Info"};
            remodelFolder.Children.Add(item);
            remodelFolder.Children.Add(new TreeViewNode {Content = "Paint Color Scheme"});
            remodelFolder.Children.Add(new TreeViewNode {Content = "Flooring woodgrain type"});
            remodelFolder.Children.Add(new TreeViewNode {Content = "Kitchen cabinet style"});

            _personalFolder = new TreeViewNode {Content = "Personal Documents", IsExpanded = true};
            _personalFolder.Children.Add(remodelFolder);

            NotesTreeView.RootNodes.Add(workFolder);
            NotesTreeView.RootNodes.Add(_personalFolder);
        }
        /*private void InitializeSampleTreeView2()
        {
            TreeViewNode workFolder = new TreeViewNode() { Content = "Work Documents" };
            workFolder.IsExpanded = true;

            workFolder.Children.Add(new TreeViewNode() { Content = "XYZ Functional Spec" });
            workFolder.Children.Add(new TreeViewNode() { Content = "Feature Schedule" });
            workFolder.Children.Add(new TreeViewNode() { Content = "Overall Project Plan" });
            workFolder.Children.Add(new TreeViewNode() { Content = "Feature Resources Allocation" });

            TreeViewNode remodelFolder = new TreeViewNode {Content = "Home Remodel", IsExpanded = true};

            remodelFolder.Children.Add(new TreeViewNode() { Content = "Contractor Contact Info" });
            remodelFolder.Children.Add(new TreeViewNode { Content = "Paint Color Scheme" });
            remodelFolder.Children.Add(new TreeViewNode() { Content = "Flooring woodgrain type" });
            remodelFolder.Children.Add(new TreeViewNode() { Content = "Kitchen cabinet style" });

            personalFolder = new TreeViewNode {Content = "Personal Documents", IsExpanded = true};
            personalFolder.Children.Add(remodelFolder);

            sampleTreeView.RootNodes.Add(workFolder);
            sampleTreeView.RootNodes.Add(personalFolder);
        }*/


        private ObservableCollection<ExplorerItem> GetData()
        {
            var list = new ObservableCollection<ExplorerItem>();
            var folder1 = new ExplorerItem()
            {
                Name = "Work Documents",
                Type = ExplorerItem.ExplorerItemType.Folder,
                Children =
                {
                    new ExplorerItem()
                    {
                        Name = "Functional Specifications",
                        Type = ExplorerItem.ExplorerItemType.Folder,
                        Children =
                        {
                            new ExplorerItem()
                            {
                                Name = "TreeView spec",
                                Type = ExplorerItem.ExplorerItemType.File,
                            }
                        }
                    },
                    new ExplorerItem()
                    {
                        Name = "Feature Schedule",
                        Type = ExplorerItem.ExplorerItemType.File,
                    },
                    new ExplorerItem()
                    {
                        Name = "Overall Project Plan",
                        Type = ExplorerItem.ExplorerItemType.File,
                    },
                    new ExplorerItem()
                    {
                        Name = "Feature Resources Allocation",
                        Type = ExplorerItem.ExplorerItemType.File,
                    }
                }
            };
            var folder2 = new ExplorerItem()
            {
                Name = "Personal Folder",
                Type = ExplorerItem.ExplorerItemType.Folder,
                Children =
                {
                    new ExplorerItem()
                    {
                        Name = "Home Remodel Folder",
                        Type = ExplorerItem.ExplorerItemType.Folder,
                        Children =
                        {
                            new ExplorerItem()
                            {
                                Name = "Contractor Contact Info",
                                Type = ExplorerItem.ExplorerItemType.File,
                            },
                            new ExplorerItem()
                            {
                                Name = "Paint Color Scheme",
                                Type = ExplorerItem.ExplorerItemType.File,
                            },
                            new ExplorerItem()
                            {
                                Name = "Flooring Woodgrain type",
                                Type = ExplorerItem.ExplorerItemType.File,
                            },
                            new ExplorerItem()
                            {
                                Name = "Kitchen Cabinet Style",
                                Type = ExplorerItem.ExplorerItemType.File,
                            }
                        }
                    }
                }
            };

            list.Add(folder1);
            list.Add(folder2);
            return list;
        }

        private void ItemClicked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var node = args.InvokedItem as ExplorerItem;
            if (node.Type == ExplorerItem.ExplorerItemType.File)
            {
                SplitPropertyHelper.openFileInRightPane(node);
            }
        }

        private void rightClicked(object sender, RightTappedRoutedEventArgs e)
        {
            // MenuFlyout myFlyout = new MenuFlyout();
            // MenuFlyoutItem firstItem = new MenuFlyoutItem { Text = "OneIt" };
            // MenuFlyoutItem secondItem = new MenuFlyoutItem { Text = "TwoIt" };
            // myFlyout.Items.Add(firstItem);
            // myFlyout.Items.Add(secondItem);
            //
            // //if you only want to show in left or buttom 
            // //myFlyout.Placement = FlyoutPlacementMode.Left;
            //
            // FrameworkElement senderElement = sender as FrameworkElement;
            //the code can show the flyout in your mouse click
            var item = (e.OriginalSource as FrameworkElement).DataContext as ExplorerItem;
            item.IsSelected = true;
            NotesTreeView.SelectedItem = item;
            RightMenu.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }


        private void MenuCopy(object sender, RoutedEventArgs e)
        {
        }

        private void MenuDelete(object sender, RoutedEventArgs e)
        {
            DeleteSelectItem( _dataSource);
        }

        private void DeleteSelectItem( ObservableCollection<ExplorerItem> dataCollection)
        {
            foreach (var item in dataCollection)
            {
               
                if (item.IsSelected==true)
                {
                    Debug.WriteLine("selecyed "+item.Name);
                    // memory protect
                    NotesTreeView.SelectedItems.Clear();
                    dataCollection.Remove(item);
                    return;
                }

                DeleteSelectItem(item.Children);
            }
        }
    }


    public class ExplorerItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FolderTemplate { get; set; }
        public DataTemplate FileTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            var explorerItem = (ExplorerItem) item;
            return explorerItem.Type == ExplorerItem.ExplorerItemType.Folder ? FolderTemplate : FileTemplate;
        }
    }
}
