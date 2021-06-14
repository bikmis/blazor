using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Intel.EmployeeManagement.RazorClassLibrary.Helper;
using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.JSExamplePage
{
    public partial class JSExample
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        private string question { get; set; } = string.Empty;

        private string answer { get; set; } = string.Empty;

        private ElementReference questionInput;

        private string refreshToken { get; set; }

        private Employee employee { get; set; } = new Employee();

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
            await jsRuntime.InvokeVoidAsync("showAlert"); //showAlert is the name of a function in scripts/app.js file
        }

        private async Task askQuestion() {
            answer = await jsRuntime.InvokeAsync<string>("askQuestion", question);  //function signature is askQuestion(Question)
        }

        private async Task focusOnInputQuestion() {
            await jsRuntime.InvokeVoidAsync("focusOnInputQuestion", questionInput); // await QuestionInput.FocusAsync(); u can use this without having to use JavaScript
        }

        private async Task setEmployeeToSessionStorage() {
            var newEmployee = new Employee() {ID = 1, FirstName = "Jack", LastName = "Smith", DepartmentID = 25, MiddleName = "", Position = "Software Engineer", DateOfBirth = DateTime.Now };
            var employeeJson = JsonSerializer.Serialize(newEmployee);
            await jsRuntime.InvokeVoidAsync("setToSessionStorage", "employee", employeeJson); 
        }

        private async Task getEmployeeFromSessionStorage()
        {
            var employeeJson = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "employee");
            if (employeeJson != null)
            {
                employee = JsonSerializer.Deserialize<Employee>(employeeJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            else {
                employee = new Employee();
            }
        }

        private async Task getRefreshTokenFromSessionStorage() {
            refreshToken = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "refresh_token");
        }

        private async Task showPrompt() {
            await ExampleJsInterop.Prompt(jsRuntime, "What is your name?");            
        }

        private async Task clear() {
            employee = new Employee();
            await jsRuntime.InvokeVoidAsync("removeFromSessionStorage", "employee");
        }

    }
}
