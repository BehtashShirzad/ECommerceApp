using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Abstractions.Contracts.Transaction;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Shared;

namespace ECommerce.Application.Features.User.Register;

public record RegisterCustomerCommand(string FirstName,string LastName,string Username,string PhoneNumber,string Password,string? Email=null):ITransactionalCommand<RegisterCustomerCommandResponse>;
public record RegisterCustomerCommandResponse(Guid CustomerId);

public class RegisterCustomerCommandHandler(IUserManagerService userManagerService,ICustomerRepository customerRepository):ITransactionalCommandHandler<RegisterCustomerCommand,RegisterCustomerCommandResponse>
{
    public async Task<RegisterCustomerCommandResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var user = await userManagerService.CreateUser(request.Username,
            request.Password,
            request.PhoneNumber,
            AppRoles.User,
            request.Email);
        var customer = Domain.Aggregates.Customer.Customer.Create(request.FirstName,request.LastName,request.PhoneNumber,user.Id);
        await customerRepository.AddAsync(customer,cancellationToken);
        return new RegisterCustomerCommandResponse(customer.Id.Value);
    }
}