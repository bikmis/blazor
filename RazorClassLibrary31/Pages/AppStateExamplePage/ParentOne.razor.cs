using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.AppStateExamplePage
{
    public partial class ParentOne : IDisposable
    {
        [Inject]
        private IAppStateService appStateService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        private string parentToChild { get; set; } = "John";

        private string name { get; set; }

        [Parameter]
        public string Id { get; set; }

        public void Dispose()
        {
            appStateService.OnChange -= StateHasChanged;
        }

        protected override Task OnInitializedAsync()
        {
            appStateService.OnChange += StateHasChanged;
            var keyValues = Utility.ParseUri(navigationManager.Uri);
            keyValues.TryGetValue("name", out string value);
            name = value;
            return base.OnInitializedAsync();
        }

        private void setTime() {
            appStateService.Time = DateTime.Now;
        }

    }
}
