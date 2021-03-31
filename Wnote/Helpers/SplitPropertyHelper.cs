using System.Diagnostics;
using Wnote.Views;

namespace Wnote.Helpers
{
    public class SplitPropertyHelper
    {
        private static MainPage _mainPage;
        private static LeftPane _leftPane;
        private static RightPane _rightPane;
        public static void Initialize(MainPage main,LeftPane leftPane,RightPane rightPane)
        {
            _mainPage = main;
            _leftPane = leftPane;
            _rightPane = rightPane;
        }

        public static void openFileInRightPane(ExplorerItem item)
        {
            _rightPane.OpenFile(item);
        }


    }
}
