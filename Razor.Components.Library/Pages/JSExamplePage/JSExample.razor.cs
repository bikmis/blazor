using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razor.Components.Library.Pages.JSExamplePage
{
    public partial class JSExample
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;

        public async Task ShowAlert() {
            await JSRuntime.InvokeVoidAsync("showAlert"); //showAlert is the name of a function in js/interop.js file
        }

        public async Task AskQuestion() {
            Answer = await JSRuntime.InvokeAsync<string>("askQuestion", Question);  //function signature is askQuestion(Question)
        }

    }
}
