using Company.Khloud.BLL.Interfaces;
using Company.Khloud.DAL.Data.Contexts;
using Company.Khloud.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Khloud.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        //private object GetPrimaryKeyValue(T entity)
        //{
        //    var entityType = _context.Model.FindEntityType(typeof(T));
        //    var primaryKey = entityType.FindPrimaryKey();
        //    var primaryProperty = primaryKey.Properties.First();

        //    var keyValue = typeof(T).GetProperty(primaryProperty.Name).GetValue(entity);
        //    return keyValue;
        //}

        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof (Employee))
            {
                return (IEnumerable <T>)await _context.Employees.Include(E => E.Department).ToListAsync();
            }
           return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E=>E.Id == id) as T;
            }
            return _context.Set<T>().Find(id);
        }
        public async Task AddAsync(T model)
        {
          await _context.Set<T>().AddAsync(model);
           
        }



        //public int Update(T entity)
        //{
        //    var existing = _context.Set<T>().Find(new object[] { GetPrimaryKeyValue(entity) });

        //    if (existing == null)
        //        return 0;

        //    _context.Entry(existing).CurrentValues.SetValues(entity);
        //    return _context.SaveChanges();
        //}

        public void Update(T model)
        {

            _context.Set<T>().Update(model);
           
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            
        }


     





    }
}
