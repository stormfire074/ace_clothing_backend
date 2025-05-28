using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserRegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<string> Roles { get; set; }
    }


    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class LoginResponse
    {
        public LoginResponse() { }
        public LoginResponse(string? token, Exception? error=null)
        {
            Token = token;
            if (error != null)
            {
                Error = new Error(error);

            }
        }

        public string? Token {  get; set; }
        public Error? Error { get; set; }
    }
}
