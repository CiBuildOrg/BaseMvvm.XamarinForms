using BaseMvvm.XamarinForms.ViewModels;
using BaseMvvm.XamarinForms.Views;
using SampleApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PullToRefresh : CustomContentPage
    {
        public PullToRefresh()
        {
            InitializeComponent();
        }

        public PullToRefresh(bool navigationBar) : base(navigationBar)
        {
            InitializeComponent();
        }

        public override void OnPullToRefresh()
        {
            var viewModel = GetViewModel<BaseViewModel>();
            for (int i = 0; i < 100000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    //async
                }
            }
            viewModel.Title = "pulled and refreshed the page";
        }
    }
}