namespace ECommerce.Application.Abstractions.Contracts.Services;

public interface IFileService
{
    
    public string UploadFile(Guid fileId,Stream stream);
}