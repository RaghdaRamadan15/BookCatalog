using BookLending.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Interface
{
    public interface IAuthService
    {
        Task<List<string>> RegisterAsync(RegisterUser User);
        Task<AuthResult> LoginAuthAsync(LoginUser User);
    }
}
