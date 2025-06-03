using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace S3_Storage_Interface.Storage
{
    class Upload
    {
        public Client _client;
        public Upload(Client client)
        {
            _client = client;
        }
        public bool AddFile(List<string> keys, List<string> filePaths)
        {
            bool _response = false;

            if (keys.Count != filePaths.Count)
            {
                MessageBox.Show("The number of keys and file paths must match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            for (int i = 0; i < keys.Count; i++)
            {
                try
                { 
                    if (!System.IO.File.Exists(filePaths[i]))
                    {
                        MessageBox.Show($"File is not found: {filePaths[i]}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue; 
                    }

                    using (var fileStream = new FileStream(filePaths[i], FileMode.Open))
                    {
                        var request = new PutObjectRequest
                        {
                            BucketName = _client.bucketName,
                            Key = keys[i],
                            InputStream = fileStream,
                            AutoCloseStream = true,
                            UseChunkEncoding = false
                        };

                        _client.s3Client.PutObject(request);
                        MessageBox.Show("File was uploaded!", "Success");
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    MessageBox.Show($"Error Amazon S3: {ex.Message}\nError code: {ex.ErrorCode}\nStatus: {ex.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _response = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"System error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _response = false;
                }
            }
            return _response;
        }
    }
}
