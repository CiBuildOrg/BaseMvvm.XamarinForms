using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExceptionPage : CustomContentPage
    {
        public ExceptionPage()
        {
            InitializeComponent();
        }

        public override void OnException(object sender, Exception exception)
        {
            DisplayAlert("exception", exception.Message, "OK");
        }
    }
}