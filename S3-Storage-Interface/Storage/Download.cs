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

            // Получаем путь к папке "Документы"
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Создаем полный путь к файлу, который будет загружен
            string filePath = Path.Combine(documentsPath, "s3_bucket_files"); // Используйте Key как имя файла
            filePath = Path.Combine(filePath, Key);

            // Извлекаем директорию из ключа
            string directoryPath = Path.GetDirectoryName(filePath);

            // Создаем папку, если она не существует
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
                MessageBox.Show($"Файл успешно загружен в: {filePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (AmazonS3Exception e)
            {
                MessageBox.Show($"Ошибка на сервере. Сообщение: '{e.Message}' при загрузке объекта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show($"Доступ запрещен. Сообщение: '{e.Message}'", "Ошибка доступа", MessageBoxButton.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Неизвестная ошибка. Сообщение: '{e.Message}'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
