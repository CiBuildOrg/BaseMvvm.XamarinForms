using BaseMvvm.XamarinForms.Interfaces;
using BaseMvvm.XamarinForms.ViewModels;
using System;
using System.Windows.Input;
using BaseMvvm.XamarinForms.Helpers;
using BaseMvvm.XamarinForms.Models;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.Views
{
    /// <summary>
    /// modified ContentView 
    /// </summary>
    public class CustomContentView : ContentView, ICustomLayout
    {
        /// <summary>
        /// </summary>
        /// <param name="navigationBar">
        /// show / hide navigationBar 
        /// </param>
        public CustomContentView(bool navigationBar) : this(navigationBar, null)
        {
        }

        /// <summary>
        /// create a page with showing navigationBar 
        /// </summary>
        public CustomContentView() : this(true, null)
        {
        }

        public CustomContentView(bool navigationBar, object bindingContextData)
        {
            MvvmMessagingCenter.Init(this);
            ViewModel = bindingContextData ?? Activator.CreateInstance(typeof(BaseViewModel));
            this.BindingContext = ViewModel;
            NavigationPage.SetHasNavigationBar(this, navigationBar);
        }

        public ICommand Commands
        {
            get { return GetViewModel<BaseViewModel>().Commands; }
        }

        public FileImageSource Icon
        {
            get { return GetViewModel<BaseViewModel>().Icon; }
            set { GetViewModel<BaseViewModel>().Icon = value; }
        }

        public bool IsBusy
        {
            get { return GetViewModel<BaseViewModel>().IsBusy; }
            set { GetViewModel<BaseViewModel>().IsBusy = value; }
        }

        public NavigationPage NavPage
        {
            get { return (Application.Current).MainPage as NavigationPage; }
        }

        public string Title
        {
            get { return GetViewModel<BaseViewModel>().Title; }
            set { GetViewModel<BaseViewModel>().Title = value; }
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

        public void SetCommand(string commandName, OnCommandDelegate externalMethod)
        {
            GetViewModel<BaseViewModel>().SetCommand(commandName, externalMethod);
        }
    }
}