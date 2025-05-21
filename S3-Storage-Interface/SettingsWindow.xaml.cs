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
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Params();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        public void Params()
        {
            using (StreamReader sr = new StreamReader("params_s3.txt"))
            {
                Endpoint_URL.Text = sr.ReadLine();
                Access_Key_Id.Text = sr.ReadLine();
                Secret_Access_Key.Text = sr.ReadLine();
                Bucket_Name.Text = sr.ReadLine();
            }
        }
    }
}
