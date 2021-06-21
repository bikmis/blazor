﻿using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.DivideByZeroService
{
    public class DivideByZeroService : IDivideByZeroService
    {
        private readonly IHttpService httpService;

        public DivideByZeroService(IHttpService _httpService)
        {
            httpService = _httpService;
        }

        public async Task<HttpResponseMessage> DivideByZero()
        {
            try {
                var response = await httpService.SendAsync(HttpMethod.Get, "api/dividebyzero", null);
                return response;
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
