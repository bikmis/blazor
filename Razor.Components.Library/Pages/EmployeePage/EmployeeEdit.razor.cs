using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razor.Components.Library.Pages.EmployeePage
{
    public partial class EmployeeEdit
    {
        public string Display;
        public string Show;
        public string AriaHidden;
        public string PaddingRight;

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public void DisplayEditForm() {
            Display = "block";
            Show = "show";
            AriaHidden = "false";
            PaddingRight = "17px";
            StateHasChanged();
        }

        public void CloseEditForm() {
            Display = "none";
            Show = "";
            AriaHidden = "true";
            PaddingRight = "0px";
        }

    }
}
