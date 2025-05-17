using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace S3_Storage_Interface.Storage
{
    internal class Client
    {
        private readonly IAmazonS3 s3Client;
        public Client(string accessKey, string secretKey, string endpoint)
        {
            var config = new AmazonS3Config
            {
                ServiceURL = endpoint,
                ForcePathStyle = true
            };
            s3Client = new AmazonS3Client(accessKey, secretKey, config);
        }
        public List<S3Object> ListObjects(string bucketName)
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = "Photo-Storage",
                    MaxKeys = 1000
                };

                var response = s3Client.ListObjectsV2(request);
                return response.S3Objects;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"S3 Error: {ex.Message}");
                return new List<S3Object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<S3Object>();
            }
        }

        // Пример использования
        public void PrintObjects(string bucketName)
        {
            var objects = ListObjects(bucketName);
            Console.WriteLine($"Objects in bucket '{bucketName}':");
            foreach (var obj in objects)
            {
                Console.WriteLine($"- {obj.Key} (Size: {obj.Size} bytes, Last Modified: {obj.LastModified})");
            }
        }
    }
}
