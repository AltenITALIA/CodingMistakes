using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CodingMistakes.Memory
{
    /// <summary>
    /// Interaction logic for DeadWindow.xaml
    /// </summary>
    public partial class DeadWindow : Window
    {
        public DeadWindow()
        {
            InitializeComponent();
        }

        private void btnCrossThread_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => this.RemoveSkull());
        }

        private async void btnGood_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => this.Dispatcher.Invoke(() => this.RemoveSkull()));
            this.Background = new SolidColorBrush(Colors.LimeGreen);
        }

        public void RemoveSkull()
        {
            try
            {
                this.imgDeath.Visibility = Visibility.Collapsed;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"{ex.GetType().Name}\r\n{ex.Message}", "CrossThreadException!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
