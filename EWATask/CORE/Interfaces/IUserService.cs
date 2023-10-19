using CORE.DTOs;
using CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddNewUser(AddNewUser form);
        Task<LoginDto> Login(LoginForm login);
    }
}
