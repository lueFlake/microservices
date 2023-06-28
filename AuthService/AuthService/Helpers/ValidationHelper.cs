using Afonin.AuthService.Domain.Base;
using Afonin.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Afonin.AuthService.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateUnique<T, T1>(T obj, IEfRepository<T> repo, Func<T, T1> func) where T : BaseEntity
        {
            if (obj == null)
            {
                return false;
            }
            return !repo.GetAll().Any(e =>
                func(obj).Equals(func(e)));
        }
    }
}
