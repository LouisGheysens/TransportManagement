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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_NavigatieOngeval_Click(object sender, RoutedEventArgs e)
        {
            OngevalWindow ongevalWindow = new();
            ongevalWindow.Show();
            this.Close();
        }

        private void btn_VrachtwagenNavigatie_Click(object sender, RoutedEventArgs e)
        {
            VrachtwagenWindow vrachtwagenWindow = new();
            vrachtwagenWindow.Show();
            this.Close();
        }

        private void btn_ChauffeurNavigatie_Click(object sender, RoutedEventArgs e)
        {
            ChauffeurWindow chauffeurWindow = new();
            chauffeurWindow.Show();
            this.Close();
        }
    }
}
