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
using System.IO;

namespace S3_Storage_Interface
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Endpoint_URL.Text == "" ||
                Access_Key_Id.Text == "" ||
                Secret_Access_Key.Text == "" ||
                Bucket_Name.Text == "")
            {
                MessageBox.Show("null", "null");
            }
            else
            {
                using (StreamWriter sw = new StreamWriter("params_s3.txt"))
                {
                    sw.WriteLine(Endpoint_URL.Text);
                    sw.WriteLine(Access_Key_Id.Text);
                    sw.WriteLine(Secret_Access_Key.Text);
                    sw.WriteLine(Bucket_Name.Text);
                }
                this.Close();
            }
        }
    }
}
