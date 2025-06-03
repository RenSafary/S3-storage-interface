using System;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using System.Windows;

namespace S3_Storage_Interface.Storage
{
    class Download
    {
        Client _client;

        public Download(Client client)
        {
            _client = client;
        }

        public void DownloadFile(string Key)
        {
            var request = new GetObjectRequest
            {
                BucketName = _client.bucketName,
                Key = Key
            };

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(documentsPath, "s3_bucket_files");
            filePath = Path.Combine(filePath, Key);

            string directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            try
            {
                using (var response = _client.s3Client.GetObject(request))
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    response.ResponseStream.CopyTo(fileStream);
                }
                MessageBox.Show($"File is downloaded in: {filePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (AmazonS3Exception e)
            {
                MessageBox.Show($"Internal server error. Message: '{e.Message}' during uploading", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show($"Access is denied. Message: '{e.Message}'", "Access error", MessageBoxButton.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unknown error. Message: '{e.Message}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
