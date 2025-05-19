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
        public string bucketName;
        public string access_key;
        public string secret_key;
        public string endpoint;
        public string keyName;
        public string filePath;

        public AmazonS3Client s3Client;
        
        public Client(string bucketName, string access_key, string secret_key, string endpoint, string keyName, string filePath)
        {
            this.bucketName = bucketName;
            this.access_key = access_key; 
            this.secret_key = secret_key; 
            this.endpoint = endpoint;
            this.keyName = keyName;
            this.filePath = filePath;

            var config = new AmazonS3Config
            {
                ServiceURL = endpoint,
                ForcePathStyle = true,
            };

            s3Client = new AmazonS3Client(access_key, secret_key, config);

        }
    }
}
