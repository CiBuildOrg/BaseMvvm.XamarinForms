using System;
using BaseMvvm.XamarinForms.ViewModels;
using System.Windows.Input;
using BaseMvvm.XamarinForms.Models;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.Interfaces
{
    public interface ICustomLayout
    {
        NavigationPage NavPage { get; }
        ICommand Commands { get; }
        FileImageSource Icon { get; set; }
        bool IsBusy { get; set; }
        string Title { get; set; }
        object ViewModel { get; }

        /// <summary>
        /// </summary>
        /// <param name="commandName">
        /// unique cmd name 
        /// </param>
        /// <param name="useBusyIndicator">
        /// use with activityindicator 
        /// </param>
        void CallCommand(string commandName, bool useBusyIndicator = true);

        /// <summary>
        /// incoming exception from messaging center 
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="exception">
        /// </param>
        void OnException(object sender, Exception exception);

        /// <summary>
        /// incoming data (except exception) from messaging center 
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="args">
        /// </param>
        void OnIncomingEvents(ICustomLayout sender, MvvmMessagingCenterEventArgs args);

        TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel;

        void OnPullToRefresh(object bindingContextData);

        /// <summary>
        /// set custom command with external method, be careful for the multiple addition 
        /// </summary>
        /// <param name="commandName">
        /// unique cmd name 
        /// </param>
        /// <param name="externalMethod">
        /// triggered method 
        /// </param>
        void SetCommand(string commandName, OnCommandDelegate externalMethod);
    }
}