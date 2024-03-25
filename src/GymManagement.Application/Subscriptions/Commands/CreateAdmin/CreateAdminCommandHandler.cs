using GymManagement.Domain.Admins;
using MediatR;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Application.Subscriptions.Commands.CreateAdmin;

public class CreateAdminCommandHandler(IAdminsRepository adminsRepository,
    IUnitOfWork unitOfWork) : 
    IRequestHandler<CreateAdminCommand, ErrorOr<Admin>>
{
    private IAdminsRepository AdminsRepository { get; } = adminsRepository;
    private IUnitOfWork UnitOfWork { get; } = unitOfWork;

    public async Task<ErrorOr<Admin>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var result = await AdminsRepository.CreateAdminAsync();
        await UnitOfWork.CommitChangesAsync();
        return result;
    }
}