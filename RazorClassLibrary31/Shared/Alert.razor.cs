using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Intel.Personnel.RazorClassLibrary.Shared
{
    public partial class Alert
    {
        [Parameter]
        public bool IsHidden { get; set; }

        [Parameter]
        public EventCallback<bool> CloseButtonEvent { get; set; }

        [Parameter]
        public string AlertColor { get; set; }

        [Parameter]
        public string Message { get; set; }

        private async Task closeMessage()
        {
            await CloseButtonEvent.InvokeAsync(true);
        }
    }
}
