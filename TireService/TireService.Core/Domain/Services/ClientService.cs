using Microsoft.EntityFrameworkCore;
using TireService.Dtos.Infos.Client;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class ClientService : ItemBasicService<Client>
{
    public ClientService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource) : base(dbContext, cancellationTokenSource)
    {
    }

    public async Task<IReadOnlyCollection<Client>> GetAll(GetAllClientByFilterInfo info)
    {
        var query = _dbSet.AsQueryable();

        if (string.IsNullOrEmpty(info.Name) == false)
        {
            query = query.Where(x => x.Name.Contains(info.Name));
        }

        if (string.IsNullOrEmpty(info.Surname) == false)
        {
            query = query.Where(x => x.Surname.Contains(info.Surname));
        }

        if (string.IsNullOrEmpty(info.Patronymic) == false)
        {
            query = query.Where(x => x.Patronymic.Contains(info.Patronymic));
        }

        if (string.IsNullOrEmpty(info.PhoneNumber) == false)
        {
            query = query.Where(x => x.PhoneNumber == info.PhoneNumber);
        }

        if (string.IsNullOrEmpty(info.CarNumber) == false)
        {
            query = query.Where(x => x.CarNumber.Contains(info.CarNumber));
        }

        if (info.ClientIds != null && info.ClientIds.Any())
        {
            query = query.Where(x => x.Name == info.Name);
        }
        
        return await query.ToArrayAsync(_token);
    }
}