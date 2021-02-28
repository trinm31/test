using System;
using System.Linq;
using AtnWeb.Models;
using AtnWeb.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AtnWeb.Data
{
    public class DbIntializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public DbIntializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {
                
            }
            
            if (_db.Roles.Any(r => r.Name == SD.Role_Manager)) return;
            if (_db.Roles.Any(r => r.Name == SD.Role_ShopStaff)) return;
            
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Manager)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_ShopStaff)).GetAwaiter().GetResult();
            
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Manager@gmail.com",
                Email = "Manager@gmail.com",
                EmailConfirmed = true,
                Name = "Manager"
            },"Manager123@").GetAwaiter().GetResult() ;
            
            ApplicationUser userManager = _db.ApplicationUsers.Where(u => u.Email == "Manager@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(userManager, SD.Role_Manager).GetAwaiter().GetResult();
            
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "ShoppStaff@gmail.com",
                Email = "ShoppStaff@gmail.com",
                EmailConfirmed = true,
                Name = "ShoppStaff"
            },"ShoppStaff123@").GetAwaiter().GetResult() ;
            
            ApplicationUser userShopStaff = _db.ApplicationUsers.Where(u => u.Email == "ShoppStaff@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(userShopStaff, SD.Role_ShopStaff).GetAwaiter().GetResult();
        }
    }
}