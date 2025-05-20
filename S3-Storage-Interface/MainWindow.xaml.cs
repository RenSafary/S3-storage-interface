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
using System.Windows.Media.Animation;

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
            GetParams();
        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
        private void Button_Upload(object sender, RoutedEventArgs e)
        {
            var openDialogFile = new Microsoft.Win32.OpenFileDialog();

            openDialogFile.Filter = "All files (*.*)|*.*";
            openDialogFile.Multiselect = true;

            if (openDialogFile.ShowDialog() == true)
            {
                string[] filePaths = openDialogFile.FileNames;
                string[] fileNames = filePaths.Select(path => System.IO.Path.GetFileName(path)).ToArray();
            }
        }
        public void GetParams()
        {
            using (StreamReader sr = new StreamReader("params_s3.txt"))
            {
                string endpoint = sr.ReadLine();
                string accessKey = sr.ReadLine();
                string secretKey = sr.ReadLine();
                string bucketName = sr.ReadLine();

                Client s3Client = new Client(bucketName, accessKey, secretKey, endpoint);

                GetList getList = new GetList(s3Client);
                List<string> list = getList.GetAllObjs();
            }
        }
    }
}
