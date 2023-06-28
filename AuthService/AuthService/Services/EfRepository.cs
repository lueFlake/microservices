using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afonin.AuthService.DbContexts;
using Afonin.AuthService.Domain.Base;
using Afonin.AuthService.Domain.Interfaces;

namespace AdAstra.HRPlatform.API.Services
{
    public class EfRepository<T>: IEfRepository<T> where T: BaseEntity
    {
        private readonly MainDbContext _context;

        public EfRepository(MainDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetById(long id)
        {
            var result = _context.Set<T>().FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                //TODO: need to add logger
                return null;
            }

            return result;
        }

        public long Add(T entity)
        {
            var result = _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return result.Entity.Id;
        }

        public void Update(T entity)
        {
            var result = _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}