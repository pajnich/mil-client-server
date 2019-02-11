using Client.viewmodels;
using System;
using System.Windows;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SnapWindowLeft();
        }

        void MainWindow_ContentRendered(object e, EventArgs eventArgs)
        {
            BindViewModel();
        }

        private void SnapWindowLeft()
        {
            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }

        private void BindViewModel()
        {
            DataContext = new UnitViewModel();
        }
    }
}
