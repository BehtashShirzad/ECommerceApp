using ECommerce.Application.Abstractions.Contracts.Services;
using ECommerce.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace ECommerce.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IMinioClient _client;
    private readonly MinioOptions _options;

    public FileService(
        IMinioClient client,
        IOptions<MinioOptions> options)
    {
        _client = client;
        _options = options.Value;
    }

    public async Task<(string fullAddress,string key)> UploadAsync(string objectKey,
        Stream stream,
        
        string contentType,
        CancellationToken cancellationToken = default)
    {
         

        var bucketExists = await _client.BucketExistsAsync(
            new BucketExistsArgs()
                .WithBucket(_options.BucketName),
            cancellationToken);

        if (!bucketExists)
        {
            await _client.MakeBucketAsync(
                new MakeBucketArgs()
                    .WithBucket(_options.BucketName),
                cancellationToken);
        }

        await _client.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(objectKey)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(contentType),
            cancellationToken);

        return ($"{_options.Endpoint}/{_options.BucketName}/{objectKey}",$"{_options.BucketName}/{objectKey}");
    }

    public async Task DeleteAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        await _client.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(objectKey),
            cancellationToken);
    }

    public async Task<Stream> DownloadAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        var ms = new MemoryStream();

        await _client.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(objectKey)
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(ms);
                }),
            cancellationToken);

        ms.Position = 0;

        return ms;
    }

    public async Task<string> GetPresignedUrlAsync(
        string objectKey,
        TimeSpan expiresIn)
    {
        return await _client.PresignedGetObjectAsync(
            new PresignedGetObjectArgs()
                .WithBucket(_options.BucketName)
                .WithObject(objectKey)
                .WithExpiry((int)expiresIn.TotalSeconds));
    }
}