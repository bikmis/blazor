using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.LifecyclePage
{
    public partial class LifecycleChild
    {
        private int counter { get; set; }

        [Parameter]
        public string Greeting { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Debug.WriteLine("3. Child OnAfterRenderAsync ran during firstRender.");
            }
            else
            {
                counter++;
                Debug.WriteLine($"{counter} Child OnAfterRenderAsync ran again;");
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        protected override Task OnParametersSetAsync()
        {
            Debug.WriteLine("2. Child OnParametersSetAsync");
            return base.OnParametersSetAsync();
        }

        protected override Task OnInitializedAsync()
        {
            Debug.WriteLine("1. Child OnInitializedAsync");
            return base.OnInitializedAsync();
        }
    }
}
