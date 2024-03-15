using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleFormsApi.IntegrationTesting.ControllersTests
{
    public class SeedTestData
    {
        public async Task SeedTestUser(UserManager<AppUser> userManager) 
        {
            var testUser = await userManager.FindByEmailAsync(TestConstants.Email);
            if (testUser != null)
            {
                return;
            }

            var result = await userManager.CreateAsync(new AppUser() { Email = TestConstants.Email, UserName = TestConstants.Username }, TestConstants.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to seed data");
            }
        }
    }
}
