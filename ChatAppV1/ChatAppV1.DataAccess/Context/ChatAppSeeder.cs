using Microsoft.AspNetCore.Identity;

namespace ChatAppV1.DataAccess.Context
{
    public static class ChatAppSeeder
    {
        public static void SeedData(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByIdAsync("TU-ID").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.Id = "TU-ID";
                user.UserName = "testuser";
                user.Email = "testuser@test.com";
                user.PhoneNumber = "123456789";
                IdentityResult result = userManager.CreateAsync(user, "testPa55").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Chatter").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Chatter").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Id = "RL-CHT";
                role.Name = "Chatter";
                role.NormalizedName = "CHT";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
