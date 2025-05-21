using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3_Storage_Interface.Storage
{
    class Upload
    {
        public Client _client;
        public Upload(Client client)
        {
            _client = client;
        }
        public bool AddFile(string[] key, string[] filePath)
        {
            bool _response = false;

            if (key.Length != filePath.Length)
            {
                MessageBox.Show("Количество ключей и путей к файлам должно совпадать.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            for (int i = 0; i < key.Length; i++)
            {
                try
                {
                    // Проверяем, существует ли файл
                    if (!System.IO.File.Exists(filePath[i]))
                    {
                        MessageBox.Show($"Файл не найден: {filePath[i]}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue; // Пропускаем этот файл
                    }

                    var request = new PutObjectRequest()
                    {
                        BucketName = _client.bucketName,
                        Key = key[i],
                        FilePath = filePath[i],
                    };
                    var response = _client.s3Client.PutObject(request);

                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show($"Файл успешно загружен: {key[i]}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        _response = true;
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка загрузки файла: {key[i]}. Статус: {response.HttpStatusCode}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        _response = false;
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    // Выводим более подробную информацию об ошибке
                    MessageBox.Show($"Ошибка Amazon S3: {ex.Message}\nКод ошибки: {ex.ErrorCode}\nСтатус: {ex.StatusCode}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    _response = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка системы: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    _response = false;
                }
            }
            return _response;
        }
    }
}
