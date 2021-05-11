using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) { 
           
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        private async Task ShowAlert() {
            await jsRuntime.InvokeVoidAsync("showAlert"); //showAlert is the name of a function in js/interop.js file
        }

        private async Task AskQuestion() {
            answer = await jsRuntime.InvokeAsync<string>("askQuestion", question);  //function signature is askQuestion(Question)
        }

        private async Task FocusOnInputQuestion() {
            await jsRuntime.InvokeVoidAsync("focusOnInputQuestion", questionInput); // await QuestionInput.FocusAsync(); u can use this without having to use JavaScript
        }


    }
}
