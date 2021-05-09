using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.JSExamplePage
{
    public partial class JSExample : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public ElementReference QuestionInput;

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) { 

            
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task ShowAlert() {
            await JSRuntime.InvokeVoidAsync("showAlert"); //showAlert is the name of a function in js/interop.js file
        }

        public async Task AskQuestion() {
            Answer = await JSRuntime.InvokeAsync<string>("askQuestion", Question);  //function signature is askQuestion(Question)
        }

        public async Task FocusOnInputQuestion() {
            await JSRuntime.InvokeVoidAsync("focusOnInputQuestion", QuestionInput); // await QuestionInput.FocusAsync(); u can use this without having to use JavaScript
        }


    }
}
