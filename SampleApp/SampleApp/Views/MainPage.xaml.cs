using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.Helpers;
using BaseMvvm.XamarinForms.Interfaces;
using BaseMvvm.XamarinForms.Models;
using BaseMvvm.XamarinForms.ViewModels;
using BaseMvvm.XamarinForms.Views;
using SampleApp.Views;
using Xamarin.Forms;

namespace SampleApp
{
    public partial class MainPage : CustomContentPage
    {
        public MainPage() : base(false)//disabled navigationbar
        {
            InitializeComponent();
            //
            SetCommand("CustomCmd", CustomCmdMth);//setter
            //
        }

        private bool bl = false;

        private void CustomCmdMth(object viewModel)
        {
            if (bl)
            {
                LblInfo.BackgroundColor = Color.Red;
                LblInfo.TextColor = Color.White;
            }
            else
            {
                LblInfo.BackgroundColor = Color.White;
                LblInfo.TextColor = Color.Red;
            }
            LblInfo.Text = "text changed: " + bl;
            bl = !bl;
        }

        private async void BtnCmd_Clicked(object sender, EventArgs e)
        {
            await this.ChangeMainPage(new Page1(false));//permanently change the mainPage
        }

        private void BtnCallCmd_Clicked(object sender, EventArgs e)
        {
            CallCommand("CustomCmd", false);//caller
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
            DisplayAlert("exception", args.MessageId, "OK");
        }

        private void BtnMessaningCenter1_Clicked(object sender, EventArgs e)
        {
            MvvmMessagingCenter.SendIncomingEvent(this, "testMessage", new { userName = "mustafa" });
        }

        private async void BtnProfile_Clicked(object sender, EventArgs e)
        {
            await this.ChangeMainPage(new SettingsUser() { Title = "User Profile" });//permanently change the mainPage
        }
    }
}