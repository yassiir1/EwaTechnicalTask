using CORE.DTOs;
using CORE.Entites;
using CORE.Interfaces;
using CORE.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyConnections.Implementaions
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> user, RoleManager<IdentityRole> roleManager)
        {
             this.signInManager = signInManager;
            this._user = user;
            this._roleManager = roleManager;
        }
        public async Task<bool>  AddNewUser(AddNewUser form)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = form.Email,
                    Email = form.Email,
                    NormalizedEmail = _user.NormalizeEmail(form.Email), // Normalize the email
                    Name = form.Name,
                    PhoneNumber = form.Phone,
                    isActive = true
                };
                var result = await _user.CreateAsync(user, form.Password);
                var InsertedData = _user.FindByIdAsync(user.Id);
                InsertedData.Result.NormalizedEmail = _user.NormalizeEmail(form.Email);
                await _user.UpdateAsync(InsertedData.Result);
                bool roleExists = await _roleManager.RoleExistsAsync(form.role.ToString());
                if (!roleExists)
                {
                    var role = new IdentityRole(form.role.ToString());
                    await _roleManager.CreateAsync(role);
                }
                await _user.AddToRoleAsync(user, form.role.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<LoginDto> Login(LoginForm login)
        {
            var data = new LoginDto();
            var user = await _user.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return data;
            }
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, true, true);
            if (result.Succeeded)
            {
                if (user.isActive)
                {
                    List<Claim> claims = new List<Claim>
                        {
                           new Claim("UID", user.Id.ToString())
                        };
                    var identity = new ClaimsIdentity(claims, ".AspNetCore.Identity.Application");
                    var principal = new ClaimsPrincipal(identity);
                    data.UserName = user.UserName;
                    data.Name = user.Name;
                    data.Email = user.Email;
                    data.PhoneNumber = user.PhoneNumber;
                    return data;

                }
            }
            else
            {
                return data;
            }
            return data;
        }
    }
}
