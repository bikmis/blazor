using Microsoft.AspNetCore.Components;
using RazorClassLibrary31.Services.Guid_Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary31.Pages.GuidExamplePage
{
    public partial class GuidExample
    {
        [Inject]
        private IGuidServiceAddSingleton guidServiceAddSingleton { get; set; }

        [Inject]
        private IGuidServiceAddScoped guidServiceAddScoped { get; set; }

        [Inject]
        private IGuidServiceAddTransient guidServiceAddTransient { get; set; }

        private Guid guidAddSingleton { get; set; }

        private int counterAddSingleton { get; set; }

        private Guid guidAddScoped { get; set; }

        private int counterAddScoped { get; set; }

        private Guid guidAddTransient { get; set; }

        private int counterAddTransient { get; set; }

        protected override Task OnInitializedAsync()
        {
            addSingleton();
            addScoped();
            addTransient();
            return base.OnInitializedAsync();
        }

        private void addSingleton()
        {
            guidAddSingleton = guidServiceAddSingleton.CreateGuid();
            counterAddSingleton = guidServiceAddSingleton.Increment();
            StateHasChanged();
        }

        private void addScoped()
        {
            guidAddScoped = guidServiceAddScoped.CreateGuid();
            counterAddScoped = guidServiceAddScoped.Increment();
            StateHasChanged();
        }

        private void addTransient()
        {
            guidAddTransient = guidServiceAddTransient.CreateGuid();
            counterAddTransient = guidServiceAddTransient.Increment();
            StateHasChanged();
        }

    }
}
