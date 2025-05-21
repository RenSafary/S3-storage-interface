using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3_Storage_Interface.Storage
{
    class Delete
    {
        Client _client;
        public Delete(Client client)
        {
            _client = client;
        }
        public void DeleteObjs(string key)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _client.bucketName,
                Key = key
            };

            try
            {
                var response = _client.s3Client.DeleteObject(request);
            }
            catch(AmazonS3Exception e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
