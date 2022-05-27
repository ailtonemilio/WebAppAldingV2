using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebAppAldingV2.Model
{
    public partial class CRUDModel : DbContext
    {
        public CRUDModel()
            : base("name=CRUDModel")
        {
        }

        public virtual DbSet<UserAlding> UserAlding { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAlding>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserAlding>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<UserAlding>()
                .Property(e => e.UserGender)
                .IsUnicode(false);

            modelBuilder.Entity<UserAlding>()
                .Property(e => e.UserProvince)
                .IsUnicode(false);
        }
    }
}
