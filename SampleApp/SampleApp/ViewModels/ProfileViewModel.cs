using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.ViewModels;

namespace SampleApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string username = string.Empty;
        public const string UsernamePropertyName = "DisplayName";

        public string DisplayName
        {
            get { return username; }
            set { SetProperty(ref username, value, UsernamePropertyName); }
        }
    }
}