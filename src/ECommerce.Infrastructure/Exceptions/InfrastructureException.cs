namespace ECommerce.Infrastructure.Exceptions;

public class InfrastructureException(string message,Exception? innerException=null) : Exception(message, innerException);