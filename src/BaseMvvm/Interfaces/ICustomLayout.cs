using System;
using BaseMvvm.XamarinForms.ViewModels;
using System.Windows.Input;
using BaseMvvm.XamarinForms.Models;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.Interfaces
{
    /// <summary>
    /// expander of CustomContentPage and CustomContentView 
    /// </summary>
    public interface ICustomLayout
    {
        /// <summary>
        /// always use this variable for CommandProperty (do not determine a new ICommand) 
        /// </summary>
        ICommand Commands { get; }

        /// <summary>
        /// Page icon 
        /// </summary>
        FileImageSource Icon { get; set; }

        /// <summary>
        /// for ActivityIndicator :) 
        /// </summary>
        bool IsBusy { get; set; }

        /// <summary>
        /// basically gets Application.Current.MainPage 
        /// </summary>
        NavigationPage NavPage { get; }

        /// <summary>
        /// title of page 
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// ViewModel for BindingContext 
        /// </summary>
        object ViewModel { get; }

        /// <summary>
        /// using one ICommand and Calls it with parameters 
        /// </summary>
        /// <param name="commandName">
        /// unique CommandParameter name 
        /// </param>
        /// <param name="useBusyIndicator">
        /// manages the IsBusyProperty and works async method 
        /// </param>
        void CallCommand(string commandName, bool useBusyIndicator = true);

        /// <summary>
        /// custom caster for ViewModel Property 
        /// </summary>
        /// <typeparam name="TViewModel">
        /// </typeparam>
        /// <returns>
        /// </returns>
        TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel;

        /// <summary>
        /// incoming exception from MvvmMessagingCenter 
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="exception">
        /// </param>
        void OnException(object sender, Exception exception);

        /// <summary>
        /// incoming data (without exception) from MvvmMessagingCenter 
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="args">
        /// </param>
        void OnIncomingEvents(ICustomLayout sender, MvvmMessagingCenterEventArgs args);

        /// <summary>
        /// PullToRefresh action method (it can be works async) 
        /// </summary>
        void OnPullToRefresh();

        /// <summary>
        /// set custom command with external method, be careful for the conflicts 
        /// </summary>
        /// <param name="commandName">
        /// unique CommandProperty name 
        /// </param>
        /// <param name="externalMethod">
        /// action method 
        /// </param>
        void SetCommand(string commandName, OnCommandDelegate externalMethod);
    }
}