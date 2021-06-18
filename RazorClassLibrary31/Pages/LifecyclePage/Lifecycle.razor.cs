﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Pages.LifecyclePage
{
    public partial class Lifecycle
    {

        private int counter { get; set; }

        private string name { get; set; }

        private string greeting { get; set; }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Debug.WriteLine("3. Parent OnAfterRenderAsync ran during firstRender.");
            }
            else {
                counter++;
                Debug.WriteLine($"{counter} Parent OnAfterRenderAsync ran again;");
            }
            
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override Task OnParametersSetAsync()
        {
            Debug.WriteLine("2. Parent OnParametersSetAsync");
            return base.OnParametersSetAsync();
        }

        protected override Task OnInitializedAsync()
        {
            Debug.WriteLine("1. Parent OnInitializedAsync");
            return base.OnInitializedAsync();
        }

    }
}
