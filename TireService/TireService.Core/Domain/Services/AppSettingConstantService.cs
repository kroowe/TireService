using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class AppSettingConstantService
{
    private readonly PostgresContext _postgresContext;
    private readonly DbSet<AppSettingConstant> _dbSet;
    private readonly CancellationToken _token;

    public AppSettingConstantService(PostgresContext postgresContext, CancellationTokenSource cancellationTokenSource)
    {
        _postgresContext = postgresContext;
        _dbSet = postgresContext.Set<AppSettingConstant>();
        _token = cancellationTokenSource.Token;
    }

    public async Task<AppSettingConstant> CreateConstant(string key, string value)
    {
        if (_dbSet.Any(x => x.Key == key))
        {
            throw new Exception("Key already created");
        }
        
        var appSettingConstant = new AppSettingConstant
        {
            Key = key,
            Value = value
        };
        
        _dbSet.Add(appSettingConstant);
        await _postgresContext.SaveChangesAsync(_token);
        return appSettingConstant;
    }

    public async Task<AppSettingConstant> UpdateConstant(string key, string value)
    {
        var appSettingConstant = await _dbSet.FirstOrDefaultAsync(x => x.Key == key, _token);
        
        if (appSettingConstant == null)
        {
            throw new Exception("Key not found");
        }

        appSettingConstant.Value = value;
        _dbSet.Update(appSettingConstant);
        await _postgresContext.SaveChangesAsync(_token);
        return appSettingConstant;
    }

    public async Task RemoveConstant(string key)
    {
        if (AppSettingConstantKeys.ReservedKeys.Contains(key))
        {
            throw new Exception("Key reserved by app");
        }
        
        var appSettingConstant = await _dbSet.FirstOrDefaultAsync(x => x.Key == key, _token);
        
        if (appSettingConstant == null)
        {
            throw new Exception("Key not found");
        }

        _dbSet.Remove(appSettingConstant);
        await _postgresContext.SaveChangesAsync(_token);
    }

    public async Task<AppSettingConstant> GetByKey(string key)
    {
        var appSettingConstant = await _dbSet.FirstOrDefaultAsync(x => x.Key == key, _token);

        return appSettingConstant;
    }
}