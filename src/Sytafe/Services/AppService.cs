using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Sytafe.Extensions;
using Sytafe.Library.Extensions;
using Sytafe.Library.Models;

namespace Sytafe.Services
{
    public class AppService
    {
        public string Token { get; set; } = string.Empty;

        private readonly string _serverAddress;

        public AppService(string serverAddress)
        {
            _serverAddress = serverAddress;
        }

        public UsedInfo CreateUsedToday(string userId)
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {Token}");
            var used = new UsedInfo
            {
                UserId = userId,
            };
            var content = new StringContent(used.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var rs = http.PostAsync($"{_serverAddress}/used/today", content).GetAwaiter().GetResult();
            var responseContent = rs.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!rs.IsSuccessStatusCode)
            {
                throw Error(rs, responseContent);
            }
            used = responseContent.JsonDeserialize<UsedInfo>();
            return used;
        }

        private Exception Error(HttpResponseMessage rs, string responseContent)
        {
            if (string.IsNullOrEmpty(responseContent))
            {
                return new Exception(rs.ReasonPhrase);
            }
            return new Exception(responseContent.JsonDeserialize<ExceptionInfo>().Message);
        }

        public UsedInfo GetUsedTodayUsing(string userId)
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {Token}");
            var rs = http.GetAsync($"{_serverAddress}/used/today/using/user/{userId}").GetAwaiter().GetResult();
            var responseContent = rs.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!rs.IsSuccessStatusCode)
            {
                throw Error(rs, responseContent);
            }
            if (rs.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null;
            }
            var used = responseContent.JsonDeserialize<UsedInfo>();
            return used;
        }

        public UserInfo SignIn(string username, string password)
        {
            using var http = new HttpClient();
            var user = new UserInfo
            {
                Username = username,
                Password = password,
            };
            var content = new StringContent(user.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var rs = http.PostAsync($"{_serverAddress}/user/sign-in", content).GetAwaiter().GetResult();
            var responseContent = rs.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!rs.IsSuccessStatusCode)
            {
                throw Error(rs, responseContent);
            }
            user = responseContent.JsonDeserialize<UserInfo>();
            return user;
        }

        public UsedInfo UpdateUsedToday(string id)
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {Token}");
            var content = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json);
            var rs = http.PutAsync($"{_serverAddress}/used/today/{id}", content).GetAwaiter().GetResult();
            var responseContent = rs.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!rs.IsSuccessStatusCode)
            {
                throw Error(rs, responseContent);
            }
            var used = responseContent.JsonDeserialize<UsedInfo>();
            return used;
        }
    }
}
