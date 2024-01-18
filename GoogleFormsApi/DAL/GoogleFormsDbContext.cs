using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Persistence
{
    public class GoogleFormsDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public GoogleFormsDbContext()
        {
        }

        /// <summary>
        /// Initializing connection options
        /// </summary>
        /// <param name="options">connection options</param>
        public GoogleFormsDbContext(DbContextOptions<GoogleFormsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Form>()
                .HasOne(f => f.Holder)
                .WithMany(u => u.HoldedForms)
                .HasForeignKey(f => f.HolderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Question>()
                .HasOne(q => q.Form)
                .WithMany(f => f.Questions)
                .HasForeignKey(q => q.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Response>()
                .HasOne(r => r.Question)
                .WithMany(q => q.Responses)
                .HasForeignKey(r => r.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserForm>()
                .HasMany(uf => uf.Responses)
                .WithOne(r => r.UserForm)
                .HasForeignKey(r => r.UserFormId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserForm>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.AssignedForms)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserForm>()
                .HasOne(uf => uf.Form)
                .WithMany(f => f.AssignedUsers)
                .HasForeignKey(uf => uf.FormId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }

        public DbSet<Form> Forms { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<UserForm> UserForms { get; set; }
    }
}
