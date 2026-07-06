namespace ECommerce.Application.Abstractions.Contracts.Services;

public interface IFileService
{
    
    Task<(string fullAddress,string key)> UploadAsync(string objectKey,
        Stream stream,
        
        string contentType,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string objectKey,
        CancellationToken cancellationToken = default);

    Task<Stream> DownloadAsync(
        string objectKey,
        CancellationToken cancellationToken = default);

    Task<string> GetPresignedUrlAsync(
        string objectKey,
        TimeSpan expiresIn);
    
    public string GetFullAddress(
        string objectKey);
}