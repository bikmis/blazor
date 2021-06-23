namespace Intel.EmployeeManagement.RazorClassLibrary.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
