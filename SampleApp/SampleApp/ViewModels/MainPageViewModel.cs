using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class MainPageViewModel : BaseMvvm.XamarinForms.ViewModels.BaseViewModel
    {
        private string _lbltext = string.Empty;

        public string LblText
        {
            get { return _lbltext; }
            set { SetProperty(ref _lbltext, value); }
        }

        private bool _state;

        public bool State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private Color _color;

        public Color LblColor
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
    }
}