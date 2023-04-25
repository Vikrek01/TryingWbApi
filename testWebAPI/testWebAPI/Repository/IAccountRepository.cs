using Microsoft.AspNetCore.Mvc;
using testWebAPI.Models;

namespace testWebAPI.Repository
{
    public interface IAccountRepository
    {
        Task<LoginResponseModel> SignUp(Register model);
        Task<LoginResponseModel> Login(Login model);
    }
}
