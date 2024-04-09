using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PcCleaner
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

        private void Button_Clean_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_History_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO: Créer page historique", "Historique", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Votre logiciel est à jour.", "Mise à jour", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Website_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://github.com/Mickael-Desclaux")
                {
                    UseShellExecute = true
                });
            } catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

        }
    }
}