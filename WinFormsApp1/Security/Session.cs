using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Infrastructure;
using WinFormsApp1.Models.Auth;

namespace WinFormsApp1.Security
{
    public class Session
    {
        public static string? Token { get; private set; }
        public static string? UserName { get; private set; }
        public static string? Email { get; private set; }

        // Guardo también todo el objeto por si lo necesito más adelante
        public static LoginResponse? CurrentUser { get; private set; }

        public static void SetSession(LoginResponse user)
        {
            CurrentUser = user;
            Token = user.Token;
            UserName = user.UserName;
            Email = user.Email;

            // Setea el JWT como Authorization = Bearer TOKEN
            ApiClient.SetBearerToken(user.Token);
        }

        public static void Clear()
        {
            CurrentUser = null;
            Token = null;
            UserName = null;
            Email = null;

            ApiClient.SetBearerToken(null);
        }

        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(Token);
    }
}
