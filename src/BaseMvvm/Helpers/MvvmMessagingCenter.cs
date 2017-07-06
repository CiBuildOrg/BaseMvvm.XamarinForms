using System;
using BaseMvvm.XamarinForms.Interfaces;
using BaseMvvm.XamarinForms.Models;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.Helpers
{
    /// <summary>
    /// custom MessagingCenter for ICustomLayout (CustomContentPage and CustomContentView) 
    /// </summary>
    public static class MvvmMessagingCenter
    {
        /// <summary>
        /// dynamically calls OnIncomingEvents method in ICustomLayout for handling CustomEvent 
        /// </summary>
        private static readonly Action<ICustomLayout, MvvmMessagingCenterEventArgs> MessagingCenterIncomingEvents = (s, e) =>
        {
            _currentPage.OnIncomingEvents(s, e);
        };

        /// <summary>
        /// dynamically calls OnIncomingEvents method in ICustomLayout for handling Exception 
        /// </summary>
        private static readonly Action<ICustomLayout, Exception> OnAppExceptionOccurred = (s, e) =>
        {
            _currentPage.OnException(s, e);
        };

        private static readonly object Sync = new object();

        /// <summary>
        /// latest instance of ICustomLayout, so we can redirect all event to it 
        /// </summary>
        private static ICustomLayout _currentPage;

        /// <summary>
        /// MvvmMessagingCenter initializer 
        /// </summary>
        /// <param name="subcriber">
        /// subcriber ICustomLayout 
        /// </param>
        public static void Init(ICustomLayout subcriber)
        {
            if (_currentPage != null)
                MessagingCenter.Unsubscribe<ICustomLayout, Exception>(_currentPage, MessagingCenterMessage.ExceptionOccurred);

            lock (Sync)
                _currentPage = subcriber;
            MessagingCenter.Subscribe<ICustomLayout, Exception>(subcriber, MessagingCenterMessage.ExceptionOccurred, OnAppExceptionOccurred);
        }

        /// <summary>
        /// </summary>
        /// <param name="sender">
        /// sender ICustomLayout 
        /// </param>
        /// <param name="exp">
        /// exception which occurs on run-time 
        /// </param>
        public static void SendException(ICustomLayout sender, Exception exp)
        {
            MessagingCenter.Send<ICustomLayout, Exception>(sender, MessagingCenterMessage.ExceptionOccurred, exp);
        }

        /// <summary>
        /// </summary>
        /// <param name="sender">
        /// sender ICustomLayout 
        /// </param>
        /// <param name="message">
        /// type of MessagingCenterMessage class 
        /// </param>
        /// <param name="Event">
        /// custom object (we can cast it after handling) 
        /// </param>
        public static void SendIncomingEvent(ICustomLayout sender, string message, object Event)
        {
            MessagingCenter.Send<ICustomLayout, MvvmMessagingCenterEventArgs>(sender, message, new MvvmMessagingCenterEventArgs(message, Event));
        }

        /// <summary>
        /// custom event subcriber 
        /// </summary>
        /// <param name="subcriber">
        /// subcriber ICustomLayout 
        /// </param>
        /// <param name="message">
        /// type of MessagingCenterMessage class 
        /// </param>
        public static void SubcribeIncomingEvent(ICustomLayout subcriber, string message)
        {
            MessagingCenter.Unsubscribe<ICustomLayout, MvvmMessagingCenterEventArgs>(subcriber, message);

            MessagingCenter.Subscribe<ICustomLayout, MvvmMessagingCenterEventArgs>(subcriber, message, MessagingCenterIncomingEvents);
        }
    }
}