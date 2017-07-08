using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.Helpers;
using BaseMvvm.XamarinForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetMainPage : CustomContentPage
    {
        public SetMainPage()
        {
            InitializeComponent();
        }

        private async void BtnSetPage_Clicked(object sender, EventArgs e)
        {
            await this.ChangeMainPage(new MainPage());
        }
    }
}