using System.Collections.Generic;
using System.Threading.Tasks;
using Afonin.AuthService.Domain.Base;

namespace Afonin.AuthService.Domain.Interfaces
{
    public interface IEfRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        long Add(T entity);

        void Update(T entity);
        // TODO: expand if needed
    }
}