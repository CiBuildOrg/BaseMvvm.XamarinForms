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
    public partial class SettingsUser : CustomContentPage
    {
        public SettingsUser()
        {
            InitializeComponent();

            StackLayout layout = new StackLayout();
            SettingsUserView usr1 = new SettingsUserView(false, new ProfileViewModel()
            {
                AiColor = Color.Red
            });
            layout.Children.Add(usr1);
            SettingsUserView usr2 = new SettingsUserView(false, new ProfileViewModel()
            {
                AiColor = Color.Blue
            });
            layout.Children.Add(usr2);
            SettingsUserView usr3 = new SettingsUserView(false, new ProfileViewModel()
            {
                AiColor = Color.Cyan
            });
            layout.Children.Add(usr3);
            Content = layout;
        }
    }
}