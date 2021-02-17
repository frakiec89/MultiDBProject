using Microsoft.EntityFrameworkCore;

namespace MultiDBProject.DB
{
    public abstract  class ApDBContext : DbContext
    {
        public ApDBContext()
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
    public class MSSQLDBContext : ApDBContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.10.148;Database=myDataBase;User Id=stud;Password=stud");
        }
    }
    public class PostGreSQLDBContext : ApDBContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=stud;Password=stud;Host=192.168.10.160;Port=5432;Database=myDataBase");
        }
    }
    public class User
    {
        public  int UserId { get; set; }
        public  string Name { get; set; }
        public int GenderId { get; set; }
        public virtual Gender Genders { get; set; }
    }
    public class Gender
    {
        public int GenderId { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}/*
Add-Migration   - Context MSSQLDBContext -OutputDir Migrations\SqlServerMigrations
Add-Migration   - Context PostGreSQLDBContext - OutputDir Migrations\PostGreSqlServerMigrations
update-database - Context MSSQLDBContext 
update-database - Context PostGreSQLDBContext 
*/