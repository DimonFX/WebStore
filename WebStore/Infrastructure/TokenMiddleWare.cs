using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure
{
    public class TokenMiddleWare
    {
        private const string correctToken = "qwerty123";
        public RequestDelegate Next { get; }

        public TokenMiddleWare(RequestDelegate next)
        {
            Next = next;
        }

        private async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["referenceToken"];

            //если нет токена, то ничего не делаем, передаем запрос дальше по конвейеру
            if (string.IsNullOrEmpty(token))
            {
                await Next.Invoke(context);
                return;
            }
            if (token == correctToken)
            {
                //Обрабатываем токен...
                await Next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("Token is incorrect");
            }

        }

    }
}
