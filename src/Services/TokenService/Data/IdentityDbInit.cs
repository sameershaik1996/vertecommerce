﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenService.Models;

namespace TokenService.Data
{
    public class IdentityDbInit
    {
        //This example just creates an Administrator role and one Admin users
        public static async void Initialize(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            //create database schema if none exists
            // _context.Database.EnsureCreated();
            context.Database.Migrate();
            //If there is already an Administrator role, abort
            //  if (context.Roles.Any(r => r.Name == "Administrator")) return;

            //Create the Administartor Role
            // await roleManager.CreateAsync(new IdentityRole("Administrator"));
            if (context.Users.Any(r => r.UserName == "sam@gmail.com")) return;
            //Create the default Admin account and apply the Administrator role
            string user = "sam@gmail.com";
            string password = "Sam@123";
            await userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);
            //   await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user), "Administrator");
        }

    }
}
