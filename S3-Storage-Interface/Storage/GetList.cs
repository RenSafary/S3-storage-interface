using System;
using System.Collections.Generic;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace S3_Storage_Interface.Storage
{
    internal class GetList
    {
        public Client _client;
        public GetList(Client client)
        {
            _client = client;
        }
        public List<string> GetAllObjs()
        {
            var fileNames = new List<string>();
            var request = new ListObjectsV2Request()
            {
                BucketName = _client.bucketName
            };
            var response = _client.s3Client.ListObjectsV2Async(request).Result;

            using (StreamWriter sw = new StreamWriter("objs.txt"))
            {
                foreach (var file in response.S3Objects)
                {
                    fileNames.Add(file.Key);
                    sw.WriteLine(file.Key);
                }
            }
            return fileNames;
        }
    }
}
