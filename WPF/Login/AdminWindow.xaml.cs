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

namespace WPF.Login
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window, IView
    {
        public AdminWindow()
        {
            InitializeComponent();
            SecureCheck();
        }

        private void SecureCheck()
        {
            if (Thread.CurrentPrincipal == null
                || !Thread.CurrentPrincipal.IsInRole("Administrators"))
            {
                throw new Exception("User is anonymous or isn't an admin.");
            }
        }

        public IViewModel ViewModel
        {
            get
            {
                return DataContext as IViewModel;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}
