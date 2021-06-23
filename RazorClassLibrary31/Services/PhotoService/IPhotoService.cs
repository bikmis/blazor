using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> GetPhotos();
    }
}
