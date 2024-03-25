using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Admins.Persistence;

public class AdminsRepository(GymManagementDbContext dbContext) : IAdminsRepository
{
    private readonly GymManagementDbContext _dbContext = dbContext;

    public Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId);
    }

    public async Task<Admin>  CreateAdminAsync()
    {
        var result= await _dbContext.Admins.AddAsync(new Admin(Guid.NewGuid()));
        return result.Entity;
    }
    public Task UpdateAsync(Admin admin)
    {
        _dbContext.Admins.Update(admin);

        return Task.CompletedTask;
    }
}