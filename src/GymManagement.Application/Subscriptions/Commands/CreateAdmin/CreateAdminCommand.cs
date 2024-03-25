using ErrorOr;
using GymManagement.Domain.Admins;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateAdmin;

public class CreateAdminCommand : IRequest<ErrorOr<Admin>>
{
    
}