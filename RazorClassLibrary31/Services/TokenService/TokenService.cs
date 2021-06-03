namespace Intel.EmployeeManagement.RazorClassLibrary.Services.Token_Service
{
    public class TokenService : ITokenService
    {
        private string _accessToken { get; set; }

        public string AccessToken { get => _accessToken; set => _accessToken = value; }
    }
}
