using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EmployeesRegister.DataAccessLayer
{
    public class EmployeesContext:DbContext
    {
        public EmployeesContext():base("DefaultConnection")
        {

        }
        public DbSet<Models.Employee> Employees
        {
            get; set;
        }
        //public DbSet<Models.Company> Companies { get; set; }
    }
}