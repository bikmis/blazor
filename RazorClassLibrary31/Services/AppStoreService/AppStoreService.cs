using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.AppStore_Service
{
    public class AppStoreService : IAppStoreService
    {
        private User _user = new User();
        private string _accessToken;
        public User User { get => _user; set => _user = value; }
        public string AccessToken { get => _accessToken; set => _accessToken = value; }
    }
}
