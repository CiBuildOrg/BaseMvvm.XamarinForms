using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : CustomContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        public Page1(bool navigationBar) : base(navigationBar)
        {
            InitializeComponent();
        }

        public override void OnPullToRefresh(object bindingContextData)
        {
            for (int i = 0; i < 100000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    //async
                }
            }
            LblInfo.Text = "pulled and refreshed the page";
        }
    }
}