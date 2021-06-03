﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.Personnel.RazorClassLibrary.Services.Serializer_Service
{
    public interface ISerializerService
    {
        StringContent SerializeToString(object data);
        Task<T> DeserializeToType<T>(HttpResponseMessage response);
        Task<List<T>> DeserializeToListOfType<T>(HttpResponseMessage response);        
    }
}
