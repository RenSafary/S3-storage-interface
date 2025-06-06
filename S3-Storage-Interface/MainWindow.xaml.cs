﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Documents_Folder();

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
        private void Button_Upload(object sender, RoutedEventArgs e)
        {
            var openDialogFile = new Microsoft.Win32.OpenFileDialog();
            openDialogFile.Filter = "All files (*.*)|*.*";
            openDialogFile.Multiselect = true;

            List<string>filePaths = new List<string>();
            List<string>fileNames = new List<string>();

            string key = SET_KEY.Text + "/";
            if (openDialogFile.ShowDialog() == true)
            {
                filePaths.AddRange(openDialogFile.FileNames);

                if (key != null)
                {
                    fileNames.AddRange(filePaths.Select(path => (key + System.IO.Path.GetFileName(path))).ToList());
                }
            }
            
            List<string> Params = GetParams();

            Client client = new Client(Params[3], Params[1], Params[2], Params[0]);

            Upload addToBucket = new Upload(client);
            bool response = addToBucket.AddFile(fileNames, filePaths);
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

        private void Download_Obj_Click(object sender, RoutedEventArgs e)
        {
            string selected_obj = ListAllObjs.SelectedItem.ToString();
            if (selected_obj != null)
            {
                List<string> Params = GetParams();
                Client client = new Client(Params[3], Params[1], Params[2], Params[0]);
                Download download = new Download(client);
                download.DownloadFile(selected_obj);
            }
            else
            {
                System.Windows.MessageBox.Show("Select object", "Warning");
            }
        }
        public void Documents_Folder()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string newFolderPath = System.IO.Path.Combine(documentsPath, "s3_bucket_files");

            if (!Directory.Exists(newFolderPath))
            {
                Directory.CreateDirectory(newFolderPath);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ListAllObjs.Items.Clear();
            List<string> Params = new List<string>();

            using (StreamReader sr = new StreamReader("params_s3.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Params.Add(sr.ReadLine());
                }
            }
            Client client = new Client(Params[3], Params[1], Params[2], Params[0]);
            GetList getList = new GetList(client);

            List<string> fileNames = getList.GetAllObjs();
            fileNames.Sort();

            foreach (string file in fileNames)
            {
                ListAllObjs.Items.Add(file);
            }
        }
        private void SET_KEY_Text_Changed(object sender, TextChangedEventArgs e)
        {

        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            string info = "Before uploading, you may enter a path in your bucket\nwhere files'l be saved" +
                "\n\nWhen you donwload files, they'l be saved in documents\n" +
                "s3_bucket_files directory";
            System.Windows.MessageBox.Show(info, "Information");
        }
    }
}
