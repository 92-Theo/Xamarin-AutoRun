using AutoRunApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AutoRunApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}