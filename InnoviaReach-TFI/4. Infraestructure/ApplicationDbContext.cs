using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transversal.Extensions;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users,Privileges,string,IdentityUserClaim<string>, UsersPrivileges,IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        //public ApplicationDbContext()
        //{

        //}
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=InnoviaDB2;User ID=testuser;Password=1234;Current Language=Spanish;MultipleActiveResultSets=True;Integrated Security=True;TrustServerCertificate=True");
        //    }
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly)
                .SetPropertyDefaultSqlValue("CreateDate", "getdate()")
                .SetPropertyDefaultValue<bool>("Active", true)
                .SetPropertyQueryFilter("Active", true);
        }

        public override int SaveChanges()
        {
            SetUpdateDateOnModifiedEntries();
            CheckDeleteRoleBase();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetUpdateDateOnModifiedEntries();
            CheckDeleteRoleBase();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetUpdateDateOnModifiedEntries()
        {
            var modifiedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Metadata.FindProperty("UpdateDate") != null &&
                            e.State == EntityState.Modified);

            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.Property("UpdateDate").CurrentValue = DateTime.Now;
            }
        }

        private void CheckDeleteRoleBase()
        {
            try
            {
                var modifiedEntries = ChangeTracker
                                    .Entries()
                                    .Where(e => e.Metadata.FindProperty("NormalizedName") != null &&
                                                e.Entity is Privileges &&
                                                (e.State == EntityState.Deleted ||
                                                e.State == EntityState.Modified)
                                                );

                foreach (var modifiedEntry in modifiedEntries)
                {
                    var role = modifiedEntry.Property("NormalizedName")?.CurrentValue?.ToString() ?? null;
                    if (role == "ADMINISTRADOR" || role == "USER")
                    {
                        throw new Exception($"No se puede eliminar/editar el rol {role} porque es un rol base del sistema.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
