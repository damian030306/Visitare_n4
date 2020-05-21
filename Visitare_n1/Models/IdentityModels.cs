using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Visitare_n1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUserModel : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUserModel> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Visitare_n1.Models.AnswerAndQuestion2> AnswerAndQuestion2 { get; set; }

        public System.Data.Entity.DbSet<Visitare_n1.Models.Route> Routes { get; set; }

        public System.Data.Entity.DbSet<Visitare_n1.Models.Points3> Points3 { get; set; }

        public System.Data.Entity.DbSet<Visitare_n1.Models.Test1> Test1 { get; set; }

        public System.Data.Entity.DbSet<Visitare_n1.Models.VisitedPoint> VisitedPoints { get; set; }
    }
}