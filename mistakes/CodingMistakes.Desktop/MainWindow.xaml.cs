using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace CodingMistakes.Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Dictionary<Guid, string> example = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCrossThread_Click(object sender, RoutedEventArgs e)
        {
            DeadWindow window = new DeadWindow();
            window.Show();
        }

        private async void btnBadAsync_Click(object sender, RoutedEventArgs e)
        {
            this.AsyncCorrect();
            this.AsyncCorrect();
        }

        private async void AsyncVoid()
        {
            this.example.Add(Guid.NewGuid(), "test");
        }

        private Task AsyncCorrect()
        {
            return Task.Run(() => this.example.Add(Guid.NewGuid(), "test"));
        }
    }
}