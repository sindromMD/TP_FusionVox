using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_FusionVox.Models.Data;

namespace TP_FusionVox.Services
{
    public class ServiceBaseAsync<T> : IServiceBaseAsync<T> where T : class
    {
        protected readonly TP_FusionVoxDbContext _dbContext;

        public ServiceBaseAsync(TP_FusionVoxDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreerAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;

        }

        public async Task EditerAsync(T entity)
        {

            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Update<T>(entity);
            }
               
            else _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> ObtenirParIdAsync(int? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> ObtenirParNomAsync(string nom)
        {
            return await _dbContext.Set<T>().FindAsync(nom);
        }

        public async Task<IReadOnlyList<T>> ObtenirToutAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task SupprimerAsync(int id)
        {
            var entity = await this.ObtenirParIdAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

        }
    }

}
