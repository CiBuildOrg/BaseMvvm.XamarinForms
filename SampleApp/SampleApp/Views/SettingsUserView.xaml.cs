using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.Views;
using SampleApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsUserView : CustomContentView
    {
        public SettingsUserView() : base(false, new ProfileViewModel())
        {
            InitializeComponent();
            SetCommand("OnLoadProfile", OnLoadProfile);
            CallCommand("OnLoadProfile", true);
        }

        public void OnLoadProfile()
        {
            for (int i = 0; i < 100000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    //async
                }
            }
            GetViewModel<ProfileViewModel>().DisplayName = "TEST USER NAME";
        }
    }
}