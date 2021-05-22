using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Services.SerializerService
{
    public interface ISerializerService
    {
        Task<T> DeserializeToType<T>(HttpResponseMessage response);
        Task<List<T>> DeserializeToListOfType<T>(HttpResponseMessage response);
    }
}
