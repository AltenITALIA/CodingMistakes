using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodingMistakes.Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();
           
        }


        private string GetPath()
        {
            return System.IO.Path.GetDirectoryName(typeof(MainWindow).Assembly.Location);
        }

        private void btnDirectory_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", $"\"{this.GetPath()}\"");
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            var file = System.IO.Directory.GetFiles(this.GetPath(), "*.zip").FirstOrDefault();
            LeakWindow window = new LeakWindow();
            MemoryTest test = new MemoryTest(window, file);
            test.Perform();
        }

        private void btnCrossThread_Click(object sender, RoutedEventArgs e)
        {
            DeadWindow window = new DeadWindow();
            window.Show();
        }
    }

    public class MemoryTest
    {
        public string File { get; set; }

        private System.IO.Stream stream;
        private readonly LeakWindow window;

        public MemoryTest(LeakWindow window, string file)
        {
            this.File = file;
            this.window = window;
        }

        public void Perform()
        {
            Task.Run(() =>
            {
                if (string.IsNullOrEmpty(this.File))
                {
                    MessageBox.Show("No zip files found, execute setup instructions!", "Not found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                using var source = new System.IO.FileStream(this.File, System.IO.FileMode.Open);
                stream = new System.IO.MemoryStream();
                source.CopyTo(stream);
                this.stream = null;
                window.Dispatcher.Invoke(() => window.Show());
            });
        }
    }
}
