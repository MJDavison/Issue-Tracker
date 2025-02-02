﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using IssueTracker.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IssueTracker.MVC.ViewModels;



namespace IssueTracker.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<Personnel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Project { get; set; }                


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<ProjectUser>()
                 .HasKey(pu => new { pu.PersonnelId, pu.ProjectId});

            builder.Entity<TicketUser>()
                .HasKey(tu => new { tu.PersonnelId, tu.TicketId });

            
                
           

            //Identity            

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Roles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<Personnel>(entity =>
            {
                entity.ToTable("Users");
            });          
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            
            
        }


        public DbSet<IssueTracker.MVC.ViewModels.TicketViewModel> TicketViewModel { get; set; }
        
    }
}
