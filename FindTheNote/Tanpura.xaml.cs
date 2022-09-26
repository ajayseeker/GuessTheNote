using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindTheNote
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tanpura : ContentPage
    {
        private TanpuraViewModel viewModel { get; set; }
        public Tanpura()
        {
            InitializeComponent();
            viewModel = new TanpuraViewModel();
            this.BindingContext = viewModel;
        }
    }
}