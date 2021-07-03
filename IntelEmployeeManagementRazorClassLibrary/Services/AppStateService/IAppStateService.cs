using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service
{
    public interface IAppStateService
    {
        event Action OnChange;

        User User { get; set; }

        string AccessToken { get; set; }

        HttpContent Serialize(object data);

        Task<T> Deserialize<T>(HttpResponseMessage response);

        Task<List<T>> DeserializeToList<T>(HttpResponseMessage response);

        DateTime? Time { get; set; }

        AlertPopUp AlertPopUp { get; set; }
    }
}
