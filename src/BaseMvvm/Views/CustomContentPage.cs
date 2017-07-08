using BaseMvvm.XamarinForms.Interfaces;
using BaseMvvm.XamarinForms.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseMvvm.XamarinForms.Helpers;
using BaseMvvm.XamarinForms.Models;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.Views
{
    public class CustomContentPage : ContentPage, ICustomLayout
    {
        public CustomContentPage(bool navigationBar) : this(navigationBar, null)
        {
        }

        public CustomContentPage() : this(true, null)
        {
        }

        public CustomContentPage(bool navigationBar, object bindingContextData)
        {
            ViewModel = bindingContextData ?? Activator.CreateInstance(typeof(BaseViewModel));
            this.BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, navigationBar);
            SetCommand("OnPullToRefresh", OnPullToRefresh);
        }

        public new virtual void OnAppearing()
        {
            MvvmMessagingCenter.Init(this);
            base.OnAppearing();
        }

        public ICommand Commands
        {
            get { return GetViewModel<BaseViewModel>().Commands; }
        }

        public new FileImageSource Icon
        {
            get { return base.Icon; }
            set { base.Icon = GetViewModel<BaseViewModel>().Icon = value; }
        }

        public new bool IsBusy
        {
            get { return base.IsBusy; }
            set { base.IsBusy = GetViewModel<BaseViewModel>().IsBusy = value; }
        }

        public NavigationPage NavPage
        {
            get { return (Application.Current).MainPage as NavigationPage; }
        }

        public new string Title
        {
            get { return base.Title; }
            set { base.Title = GetViewModel<BaseViewModel>().Title = value; }
        }

        public object ViewModel { get; }

        public void CallCommand(string commandName, bool useBusyIndicator = true)
        {
            GetViewModel<BaseViewModel>().CallCommand(commandName, useBusyIndicator);
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
            return (TViewModel)ViewModel;
        }

        public virtual void OnException(object sender, Exception exception)
        {
        }

        public virtual void OnIncomingEvents(ICustomLayout sender, MvvmMessagingCenterEventArgs args)
        {
        }

        public virtual void OnPullToRefresh()
        {
        }

        public void SetCommand(string commandName, OnCommandDelegate externalMethod)
        {
            GetViewModel<BaseViewModel>().SetCommand(commandName, externalMethod);
        }
    }
}