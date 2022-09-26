using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindTheNote
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            this.BindingContext = viewModel;
        }
    }
}
