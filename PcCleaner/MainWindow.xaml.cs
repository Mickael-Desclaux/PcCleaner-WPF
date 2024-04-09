using System.Diagnostics;
using System.IO;
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
        public DirectoryInfo winTemp;
        public DirectoryInfo appTemp;

        public MainWindow()
        {
            InitializeComponent();
            winTemp = new DirectoryInfo(@"C:\Windows\Temp");
            appTemp = new DirectoryInfo(System.IO.Path.GetTempPath());
        }

        //Calcul de la taille d'un dossier
        public long DirSize(DirectoryInfo dir)
        {
            long size = 0;
            try
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    size += file.Length;
                }
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in dirs)
                {
                    size += DirSize(subdir);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Ignorer les fichiers et dossiers protégés
            }
            return size;
        }

        //Vider un dossier
        public void ClearTempData(DirectoryInfo di)
        {
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                }
                catch (Exception ex) {
                    continue;
                }
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                    Console.WriteLine(dir.FullName);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void Button_Clean_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Nettoyage en cours ...");
            btnClean.Content = "\nNettoyage en cours";

            Clipboard.Clear();

            try
            {
                ClearTempData(winTemp);
            } catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message); ;
            }

            try
            {
                ClearTempData(appTemp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message); ;
            }

            btnClean.Content = "\nNettoyage effectué !";
            titre.Content = "Nettoyage effectué !";
            espace.Content = "0 Mb";
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

        private void Button_Analyse_Click(object sender, RoutedEventArgs e)
        {
            AnalyseFolders();
        }

        //Analyse de la taille des dossiers et fichiers à supprimer
        public void AnalyseFolders()
        {
            Console.WriteLine("Début de l'analyse...");
            long totalSize = 0;

            try 
            {
                totalSize += DirSize(winTemp) / 1000000;
                totalSize += DirSize(appTemp) / 1000000;
            } catch (Exception ex)
            {
                Console.WriteLine("Impossible d'analyser les dossiers : " + ex.Message);
            }


            espace.Content = totalSize + " Mb";
            titre.Content = "Analyze effectué !";
            date.Content = DateTime.Today;
        }
    }
}