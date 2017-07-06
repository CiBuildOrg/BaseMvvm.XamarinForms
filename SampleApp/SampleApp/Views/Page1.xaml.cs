using BaseMvvm.XamarinForms.ViewModels;
using BaseMvvm.XamarinForms.Views;
using SampleApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : CustomContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        public Page1(bool navigationBar) : base(navigationBar, new Page1ViewModel())
        {
            InitializeComponent();
        }

        public override void OnPullToRefresh()
        {
            GetViewModel<Page1ViewModel>().CustomText = "processing...";
            for (int i = 0; i < 100000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    //async
                }
            }
            GetViewModel<Page1ViewModel>().CustomText = "pulled and refreshed the page";
        }
    }
}