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
    class AddToBucket
    {
        public Client _client;
        public AddToBucket(Client client)
        {
            _client = client;
        }
        public bool AddFile(string[] key, string[] filePath)
        {
            bool _response = false;

            for (int i = 0; i < key.Length; i++)
            {
                try
                {
                    var request = new PutObjectRequest()
                    {
                        BucketName = _client.bucketName, 
                        Key = key[i],
                        FilePath = filePath[i],
                    };
                    var response = _client.s3Client.PutObject(request);

                     if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show("True", "Response");
                        _response = true;
                    }
                    else
                    {
                        MessageBox.Show("False", "Response");
                        _response = false;
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    MessageBox.Show(ex.Message, "Amazon");
                    _response = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "System");
                    _response = false;
                }
            }
            return _response;
        }
    }
}
