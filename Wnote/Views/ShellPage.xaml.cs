using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

using Wnote.Core.Helpers;
using Wnote.Core.Services;
using Wnote.Helpers;
using Wnote.Models;
using Wnote.Services;
using static Windows.UI.Colors;

namespace Wnote.Views
{
    // TODO WTS: You can edit the text for the menu in String/en-US/Resources.resw
    // You can show pages in different ways (update main view, navigate, right pane, new windows or dialog) using MenuNavigationHelper class.
    // Read more about MenuBar project type here:
    // https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/projectTypes/menubar.md
    public sealed partial class ShellPage : Page, INotifyPropertyChanged
    {
        private readonly KeyboardAccelerator _altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
        private readonly KeyboardAccelerator _backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);

        private bool _isLoggedIn;
        private bool _isAuthorized;

        private IdentityService IdentityService => Singleton<IdentityService>.Instance;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => Set(ref _isLoggedIn, value);
        }

        public bool IsAuthorized
        {
            get => _isAuthorized;
            set => Set(ref _isAuthorized, value);
        }


        public ShellPage()
        {
            InitializeComponent();
            NavigationService.Frame = ShellFrame;
            MenuNavigationHelper.Initialize(SplitView, RightFrame);
            IdentityService.LoggedIn += OnLoggedIn;
            IdentityService.LoggedOut += OnLoggedOut;
            this.SetTitlebar();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            KeyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            KeyboardAccelerators.Add(_backKeyboardAccelerator);
            IsLoggedIn = IdentityService.IsLoggedIn();
            IsAuthorized = IsLoggedIn && IdentityService.IsAuthorized();
        }

        private void OnLoggedIn(object sender, EventArgs e)
        {
            IsLoggedIn = true;
            IsAuthorized = IsLoggedIn && IdentityService.IsAuthorized();
        }

        private void OnLoggedOut(object sender, EventArgs e)
        {
            IsLoggedIn = false;
            IsAuthorized = false;
            CleanRestrictedPagesFromNavigationHistory();
            GoBackToLastUnrestrictedPage();
        }

        private void CleanRestrictedPagesFromNavigationHistory()
        {
            NavigationService.Frame.BackStack
                .Where(b => Attribute.IsDefined(b.SourcePageType, typeof(Restricted)))
                .ToList()
                .ForEach(page => NavigationService.Frame.BackStack.Remove(page));
        }

        private void GoBackToLastUnrestrictedPage()
        {
            var currentPage = NavigationService.Frame.Content as Page;
            var isCurrentPageRestricted = Attribute.IsDefined(currentPage.GetType(), typeof(Restricted));
            if (isCurrentPageRestricted)
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    MenuNavigationHelper.UpdateView(typeof(MainPage));
                }
            }
        }

        private void ShellMenuItemClick_Views_Main(object sender, RoutedEventArgs e)
        {
            MenuNavigationHelper.UpdateView(typeof(MainPage));
        }

        private void ShellMenuItemClick_Views_WebView(object sender, RoutedEventArgs e)
        {
            MenuNavigationHelper.UpdateView(typeof(WebViewPage));
        }

        private void ShellMenuItemClick_Views_TreeView(object sender, RoutedEventArgs e)
        {
            MenuNavigationHelper.UpdateView(typeof(TreeViewPage));
        }

        private void ShellMenuItemClick_Views_TabbedPivot(object sender, RoutedEventArgs e)
        {
            MenuNavigationHelper.UpdateView(typeof(TabbedPivotPage));
        }

        private void ShellMenuItemClick_Views_Chart(object sender, RoutedEventArgs e)
        {
            MenuNavigationHelper.UpdateView(typeof(ChartPage));
        }

        private void ShellMenuItemClick_File_Settings(object sender, RoutedEventArgs e)
        {
            MenuNavigationHelper.OpenInRightPane(typeof(SettingsPage));
        }

        private void ShellMenuItemClick_File_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerator = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            var result = NavigationService.GoBack();
            args.Handled = result;
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

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SetTitlebar()
        {
            // CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(Titlebar);
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Transparent;
            // titleBar.ButtonForegroundColor = White;
            // titleBar.ButtonHoverBackgroundColor = White;
            // titleBar.ButtonHoverForegroundColor = White;
            // titleBar.ButtonInactiveBackgroundColor = White;
            // titleBar.ButtonInactiveForegroundColor = White;
            // titleBar.ButtonPressedBackgroundColor = White;
            // titleBar.ButtonPressedForegroundColor = White;
            // ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            // titleBar.ButtonBackgroundColor = Colors.Transparent;
            // titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            Titlebar.Height = sender.Height;
        }
    }
}
