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
using S3_Storage_Interface.Storage;
using System.IO;

namespace S3_Storage_Interface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void GetParams()
        {
            using (StreamReader sr = new StreamReader("params.txt"))
            {
                string bucketName = sr.ReadLine();
                string endpoint = sr.ReadLine();
                string accessKey = sr.ReadLine();
                string secretKey = sr.ReadLine();
                //string keyName = sr.ReadLine(); in another place
                //string filePath = sr.ReadLine(); in another place

                Client s3Client = new Client(bucketName, accessKey, secretKey, endpoint, keyName, filePath);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}
