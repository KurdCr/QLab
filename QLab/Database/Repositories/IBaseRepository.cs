using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QLab.Database.Repositories
{
    public interface IBaseRepository<TModel, Tkey>
        where TModel : class
        where Tkey : struct
    {
        Task<TModel> GetByIdAsync(Tkey id);
        Task<List<TModel>> GetAllAsync();
        Task<TModel> CreateAsync(TModel tModel);
        Task UpdateAsync(TModel tModel);
        Task DeleteAsync(TModel tModel);

    }

    public class BaseRepository<TModel, Tkey> : IBaseRepository<TModel, Tkey>
        where TModel : class
        where Tkey : struct
    {
        protected readonly ApplicationDbContext DbContext;
        public DbSet<TModel> Table { get; }

        public BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Table = dbContext.Set<TModel>();
        }

        public async Task<TModel> GetByIdAsync(Tkey id)
        {
            try
            {
                return await Table.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TModel>> GetAllAsync()
        {
            try
            {
                return await Table.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TModel> CreateAsync(TModel tModel)
        {
            try
            {
                Table.Add(tModel);
                await DbContext.SaveChangesAsync();
                return tModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync(TModel tModel)
        {
            try
            {
                DbContext.Entry(tModel).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(TModel tModel)
        {
            try
            {
                Table.Remove(tModel);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
