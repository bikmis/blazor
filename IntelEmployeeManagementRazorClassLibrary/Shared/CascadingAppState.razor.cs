using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intel.EmployeeManagement.RazorClassLibrary.Shared
{
    public partial class CascadingAppState
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } // This is required as we are going to wrap Router with this component

        private string message = "";
        public string Message
        {
            get => message;
            set
            {
                message = value;
                StateHasChanged();
            }
        }

    }
}
