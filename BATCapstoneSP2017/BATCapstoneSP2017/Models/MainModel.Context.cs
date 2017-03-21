
namespace BATCapstoneSP2017
{




    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Web.UI.WebControls;

    public partial class MainModelContext : DbContext
    {
        public MainModelContext()
            : base("name=MainModelContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
            //base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<MenuItem> MenuItems { get; set; }
    }

}