using Server.viewmodels;
using System.Windows;

namespace Server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SnapWindowRight();
            BindViewModel();
        }
        
        private void SnapWindowRight()
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }
        
        private void BindViewModel()
        {
            DataContext = new UnitViewModel();
        }
    }
}
