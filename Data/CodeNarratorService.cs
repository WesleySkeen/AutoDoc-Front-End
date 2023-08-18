using Amazon;
using Amazon.S3;
using Newtonsoft.Json;

namespace AutoDoc_Front_End.Data;

public class CodeNarratorService
{
    public async Task<List<DocumentItem>> GetCodeNarrationAsync(DateOnly startDate)
    {
        
        var region = RegionEndpoint.GetBySystemName("us-west-2");
        var config = new AmazonS3Config { RegionEndpoint = region };
        IAmazonS3 s3Client = new AmazonS3Client(config);
        
        var result = await GetS3Values(s3Client, "hat-23-hsb","hat-32-hsb-file.json");

        return result;
    }

    async Task<List<DocumentItem>> GetS3Values(IAmazonS3 client, string bucketName, string objectName)
    {
        List<DocumentItem>? items = null;

        using (var stream = await DownloadObjectFromBucketAsync(client, bucketName, objectName))
        using (var reader = new StreamReader(stream))
        {
            var json = await reader.ReadToEndAsync();
            try
            {
                items = JsonConvert.DeserializeObject<List<DocumentItem>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"S3 response failed to deserialize {ex.Message}");
            }
        }

        return items;
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
