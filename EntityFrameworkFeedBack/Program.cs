using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkFeedBack
{
    public class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<FeedBackContext>(new FeedBackDbInitializer());

            using (var context = new FeedBackContext())
            {
                foreach (var fb in context.FeedBacks)
                {
                    Console.WriteLine(fb.Name);
                }
            }
        }        
    }
    // Entity Framework code first
    // Entity Class : SQL Server Table Record => Model
    public class FeedBack
    {
        public int FeedBackId { get; set; }
        [Required, StringLength(50, MinimumLength=2)]
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public string PostIP { get; set; }
    }

    // Entity Set Class = table
    public class FeedBackContext : DbContext
    {
        public FeedBackContext()
        {
            // Empty : SQLExpress
        }

        public FeedBackContext(string connectionString) : base(connectionString)
        {

        }

        public DbSet<FeedBack> FeedBacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FeedBack>()
                .Property(fb => fb.FeedBackId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<FeedBack>().Property(f => f.Comment).IsRequired();
        }
    }

    public class FeedBackDbInitializer : DropCreateDatabaseIfModelChanges<FeedBackContext>
    {
        protected override void Seed(FeedBackContext context)
        {
            base.Seed(context);

            var feedBackData = new List<FeedBack>()
            {
                new FeedBack { Name="Kani", Comment="Like1" },
                new FeedBack { Name="Mani", Comment="Like2" },
                new FeedBack { Name="Moani", Comment="Like3" },
                new FeedBack { Name="Khani", Comment="Like4" },
                new FeedBack { Name="Saini", Comment="Like5" }
            };

            foreach (var fb in feedBackData)
            {
                context.FeedBacks.Add(fb);
            }
            context.SaveChanges();
        }
    }
}
