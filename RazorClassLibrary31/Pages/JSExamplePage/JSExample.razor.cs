using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RazorClassLibrary31.Models;
using System;
using System.Threading.Tasks;
using System.Text.Json;


namespace RazorClassLibrary31.Pages.JSExamplePage
{
    public partial class JSExample : ComponentBase
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; }
        
        private string question { get; set; } = string.Empty;
        
        private string answer { get; set; } = string.Empty;
        
        private ElementReference questionInput;
        
        string firstname { get; set; }

        Employee employee = new Employee() { FirstName = "Hello", LastName = "Mishra", Age= 20, MiddleName= "", Position= "CTO", DateOfBirth = DateTime.Now};

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
            var jsonObj = JsonSerializer.Serialize(employee);
            await jsRuntime.InvokeVoidAsync("setToSessionStorage", "employee", jsonObj); 
        }

        private async Task getEmployeeFromSessionStorage() {
            var jsonObj = await jsRuntime.InvokeAsync<string>("getFromSessionStorage", "employee");
            var employeeObj = JsonSerializer.Deserialize<Employee>(jsonObj);
        }

    }

}
