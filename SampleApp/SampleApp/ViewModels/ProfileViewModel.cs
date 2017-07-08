using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.ViewModels;
using Xamarin.Forms;

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

        private Color _aicolor;

        public Color AiColor
        {
            get { return _aicolor; }
            set { SetProperty(ref _aicolor, value); }
        }
    }
}