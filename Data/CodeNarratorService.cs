using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;

namespace AutoDoc_Front_End.Data;

public class CodeNarratorService
{
    public async Task<DocumentItem> GetCodeNarrationAsync(DateOnly startDate)
    {
        
        var region = RegionEndpoint.GetBySystemName("us-west-2");
        var config = new AmazonS3Config { RegionEndpoint = region };
        IAmazonS3 s3Client = new AmazonS3Client(config);
        
        var result = await GetS3Values(s3Client, "hat-23-hsb");

        return result;
    }
    
    async Task<DocumentItem> GetS3Values(IAmazonS3 client, string bucketName)
    {
        DocumentItem? items = null;

        var list = await ListS3Values(client, bucketName);
        var current = list.S3Objects.MaxBy(m => m.LastModified);

        using (var stream = await DownloadObjectFromBucketAsync(client, bucketName, current?.Key))
        using (var reader = new StreamReader(stream))
        {
            var json = await reader.ReadToEndAsync();
            try
            {
                items = JsonConvert.DeserializeObject<DocumentItem>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"S3 response failed to deserialize {ex.Message}");
            }
        }

        return items;
    }
    
    async Task<ListObjectsResponse> ListS3Values(IAmazonS3 client, string bucketName)
    {
        var request = new ListObjectsRequest
        {
            BucketName = bucketName
        };
        var response = await client.ListObjectsAsync(request);

        return response;
    }
    
    async Task<Stream> DownloadObjectFromBucketAsync(IAmazonS3 client, string bucketName, string objectName)
    {
        var request = new Amazon.S3.Model.GetObjectRequest
        {
            BucketName = bucketName,
            Key = objectName
        };
        
        var response = await client.GetObjectAsync(request);
    
        return response.ResponseStream;
    }
}
