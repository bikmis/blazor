using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RazorClassLibrary31.Models;
using RazorClassLibrary31.Services.Authentication_Service;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.JSExamplePage
{
    public partial class JSExample : ComponentBase
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; }
        
        private string question { get; set; } = string.Empty;
        
        private string answer { get; set; } = string.Empty;
        
        private ElementReference questionInput;
        
        private string refreshToken { get; set; }

        Employee employee = new Employee() { FirstName = "Hello", LastName = "Mishra", Age= 20, MiddleName= "", Position= "CTO", DateOfBirth = DateTime.Now};

        [Inject]
        private AuthenticationStateProvider authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((AuthenticationService)authenticationService).GuardRoute();

        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) { 
           
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        private async Task showAlert() {
            await jsRuntime.InvokeVoidAsync("showAlert"); //showAlert is the name of a function in js/interop.js file
        }

        private async Task askQuestion() {
            answer = await jsRuntime.InvokeAsync<string>("askQuestion", question);  //function signature is askQuestion(Question)
        }

        private async Task focusOnInputQuestion() {
            await jsRuntime.InvokeVoidAsync("focusOnInputQuestion", questionInput); // await QuestionInput.FocusAsync(); u can use this without having to use JavaScript
        }

        private async Task setEmployeeToSessionStorage() {
            var employeeJson = JsonSerializer.Serialize(employee);
            await jsRuntime.InvokeVoidAsync("setToSessionStorage", "employee", employeeJson); 
        }

        private async Task getEmployeeFromSessionStorage() {
            var employeeJson = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "employee");
            var emp = JsonSerializer.Deserialize<Employee>(employeeJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        private async Task getRefreshTokenFromSessionStorage() {
            refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");
        }

    }
}
