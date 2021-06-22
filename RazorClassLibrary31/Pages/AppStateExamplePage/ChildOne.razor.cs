using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.AppStateExamplePage
{
    public partial class ChildOne : IDisposable
    {
        [Inject]
        private IAppStateService appStateService { get; set; }

        [CascadingParameter]
        private CascadingAppState cascadingAppState { get; set; }

        [Parameter]
        public string Name { get; set; }

        public string DoSomething() {
            return "Did something";
        }

        [Parameter]
        public EventCallback<string> OnSaved { get; set; }

        public void Dispose()
        {
            appStateService.OnChange -= StateHasChanged;
        }

        protected override Task OnInitializedAsync()
        {
            appStateService.OnChange += StateHasChanged;
            return base.OnInitializedAsync();
        }

        private void setTime() {
            appStateService.Time = DateTime.Now;
        }

    }
}
