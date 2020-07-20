namespace AspNet_MVC_Crud.Contexts
{
    using AspNet_MVC_Crud.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ShopContext : DbContext
    {
        public ShopContext(): base("name=ShopContext") { }

        public DbSet<Product> Products { get; set; }
    }

}