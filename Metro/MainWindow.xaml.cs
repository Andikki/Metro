using System.Windows;
using System.Windows.Input;
using Metro.Model;

namespace Metro
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;
        }

        private void StationElement_MouseDown(object sender, MouseButtonEventArgs e)
        {

            viewModel.SelectStation(((FrameworkElement)sender).DataContext as Station);
        }
    }
}
