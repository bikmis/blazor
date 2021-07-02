using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.Tests.Blazor_Client_Tests.Unit_Tests.Services
{
    public class MockPhotoService : IPhotoService
    {
        private IEnumerable<Photo> photos = new List<Photo>() { 
            new Photo { ID = 1, AlbumId = 1, ThumbnailUrl="thumbnail Url 1", Title= "fake title 1", URL = "url 1"},
            new Photo { ID = 2, AlbumId = 1, ThumbnailUrl="thumbnail Url 2", Title= "fake title 2", URL = "url 2"},
            new Photo { ID = 3, AlbumId = 1, ThumbnailUrl="thumbnail Url 3", Title= "fake title 3", URL = "url 3"}
        };

        public Task<IEnumerable<Photo>> GetPhotos()
        {
            return Task.FromResult(photos);   
        }
    }
}
