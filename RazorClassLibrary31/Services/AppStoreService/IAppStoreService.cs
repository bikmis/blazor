using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.AppStore_Service
{
    public interface IAppStoreService
    {
        User User { get; set; }
        string AccessToken { get; set; }
    }
}
