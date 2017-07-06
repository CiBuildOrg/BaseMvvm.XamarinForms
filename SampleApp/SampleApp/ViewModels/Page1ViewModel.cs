using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.ViewModels;

namespace SampleApp.ViewModels
{
    public class Page1ViewModel : BaseViewModel
    {
        private string _txtcutom = string.Empty;

        public string CustomText
        {
            get { return _txtcutom; }
            set { SetProperty(ref _txtcutom, value); }
        }
    }
}