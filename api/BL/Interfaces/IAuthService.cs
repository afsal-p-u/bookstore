using BL.DTO;
using BookStore.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAuthService
    {
        Task<string> SignUp(CreateUserDTO user);
        Task<string> SignIn(LoginDTO user);
    }
}
