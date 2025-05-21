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

                List<string> Params = GetParams();

                Client client = new Client(Params[3], Params[1], Params[2], Params[0]);
                AddToBucket addToBucket = new AddToBucket(client);

                bool response = addToBucket.AddFile(fileNames, filePaths);
            }
        }
        public List<string> GetParams()
        {
            string endpoint;
            string accessKey;
            string secretKey;
            string bucketName;

            List<string> Params = new List<string>();
            using (StreamReader sr = new StreamReader("params_s3.txt"))
            {
                endpoint = sr.ReadLine();
                Params.Add(endpoint);

                accessKey = sr.ReadLine();
                Params.Add(accessKey);

                secretKey = sr.ReadLine();
                Params.Add(secretKey);

                bucketName = sr.ReadLine();
                Params.Add(bucketName);
            }
            return Params;
        }

        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            ListAllObjs.Items.Clear();
            List<string> Params = new List<string>();
            
            using(StreamReader sr = new StreamReader("params_s3.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Params.Add(sr.ReadLine());
                }
            }
            Client client = new Client(Params[3], Params[1], Params[2], Params[0]);
            GetList getList = new GetList(client);

            List<string> fileNames = getList.GetAllObjs();

            foreach (string file in fileNames)
            {
                ListAllObjs.Items.Add(file);
            }
        }

        private void Delete_Obj_Click(object sender, RoutedEventArgs e)
        {
            string key = ListAllObjs.SelectedItem.ToString();

            List<string> Params = GetParams();
            Client client = new Client(Params[3], Params[1], Params[2], Params[0]);
            Delete delete = new Delete(client);
            delete.DeleteObjs(key);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
