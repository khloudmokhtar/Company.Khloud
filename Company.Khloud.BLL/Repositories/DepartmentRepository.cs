using Company.Khloud.BLL.Interfaces;
using Company.Khloud.DAL.Data.Contexts;
using Company.Khloud.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Khloud.BLL.Repositories
{
  public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CompanyDbContext context) : base(context) //Ask CLR Create Object from CompanyDbContext
        {
            
        }
        //private readonly CompanyDbContext _Context;//Null

        ////Ask CLR Create Object From CompanyDbContext
        //public DepartmentRepository(CompanyDbContext dbContext)
        //{
        //    _Context = dbContext;
        //}
        //public IEnumerable<Department> GetAll()
        //{

        //    return _Context.Departments.ToList();
        //}

        //public Department? Get(int id)
        //{

        //    return _Context.Departments.Find(id);
        //}
        //public int Add(Department model)
        //{

        //    _Context.Departments.Add(model);
        //    return _Context.SaveChanges();

        //}

        //public int Update(Department model)
        //{

        //   _Context.Departments.Update(model);
        //    return _Context.SaveChanges();
        //}

        //public int Delete(Department model)
        //{
        //    _Context.Departments.Remove(model);
        //    return _Context.SaveChanges();
        //}






    }
}
