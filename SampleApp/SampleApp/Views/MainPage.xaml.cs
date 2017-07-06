using System;
using BaseMvvm.XamarinForms.Helpers;
using BaseMvvm.XamarinForms.Interfaces;
using BaseMvvm.XamarinForms.Models;
using BaseMvvm.XamarinForms.Views;
using SampleApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CustomContentPage
    {
        public MainPage() : base(false, new MainPageViewModel())//disabled navigationbar and sets viewModel
        {
            InitializeComponent();
            //
            SetCommand("CustomCmd", CustomCmdMth);//setter

            CallCommand("CustomCmd", false);//you can access xaml properties while call from in ctor
            //
            SetCommand("BtnCallCommand", BtnCallFromCmd);//setter
        }

        private bool bl = false;

        private void CustomCmdMth()
        {
            LblInfo.BackgroundColor = Color.White;
            LblInfo.TextColor = Color.Red;

            LblInfo.Text = "you can access xaml properties while call from in ctor: " + bl;
        }

        private async void BtnCmd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1(true));
        }

        private void BtnCallFromCmd()
        {
            GetViewModel<MainPageViewModel>().LblText = "text is changed dynamically: " + GetViewModel<MainPageViewModel>().State;
            GetViewModel<MainPageViewModel>().State = !GetViewModel<MainPageViewModel>().State;
        }

        private void BtnThrowException_Clicked(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("custom exception!!!");
            }
            catch (Exception exception)
            {
                MvvmMessagingCenter.SendException(this, exception);//subcriber is auto change to currentPage
            }
        }

        public override void OnException(object sender, Exception exception)
        {
            DisplayAlert("exception", exception.Message, "OK");
        }

        public override void OnIncomingEvents(ICustomLayout sender, MvvmMessagingCenterEventArgs args)
        {
            object obj = args.Cast<object>();//custom caster
            DisplayAlert("exception", args.MessageId + " " + obj.ToString(), "OK");
        }

        private void BtnMessaningCenter1_Clicked(object sender, EventArgs e)
        {
            MvvmMessagingCenter.SubcribeIncomingEvent(this, "testMessage");
            MvvmMessagingCenter.SendIncomingEvent(this, "testMessage", new { userName = "mustafa" });
        }

        private async void BtnProfile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsUser() { Title = "User Profile" });
        }
    }
}