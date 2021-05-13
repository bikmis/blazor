using Microsoft.AspNetCore.Components;

namespace RazorClassLibrary31.Shared
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

        private void closeMessage()
        {
            CloseButtonEvent.InvokeAsync(true);
        }
    }
}
