﻿using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.AppStore_Service
{
    public interface IAppStoreService
    {
        User User { get; set; }
        string AccessToken { get; set; }
        StringContent SerializeToString(object data);
        Task<T> DeserializeToType<T>(HttpResponseMessage response);
        Task<List<T>> DeserializeToListOfType<T>(HttpResponseMessage response);
    }
}
