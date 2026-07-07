namespace ECommerce.Infrastructure.Options;

public class MinioOptions
{
    public string Endpoint { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public string Public_Url { get; set; } = null!;

    public bool UseSSL
    {
        get;
        set
        {
            field = value;
            if (value == true)

                Schema = "https";
            else
                Schema = "http";
        }
        
    }
    public string Schema { get; set; }
}