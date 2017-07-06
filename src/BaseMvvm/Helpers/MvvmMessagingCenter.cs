using System;
using BaseMvvm.XamarinForms.Interfaces;
using BaseMvvm.XamarinForms.Models;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.Helpers
{
    public static class MvvmMessagingCenter
    {
        private static readonly Action<ICustomLayout, MvvmMessagingCenterEventArgs> MessagingCenterIncomingEvents = (s, e) =>
        {
            _currentPage.OnIncomingEvents(s, e);
        };

        private static readonly Action<ICustomLayout, Exception> OnAppExceptionOccurred = (s, e) =>
        {
            _currentPage.OnException(s, e);
        };

        private static readonly object Sync = new object();

        private static ICustomLayout _currentPage;

        public static void Init(ICustomLayout subcriber)
        {
            if (_currentPage != null)
                MessagingCenter.Unsubscribe<ICustomLayout, Exception>(_currentPage, MessagingCenterMessage.ExceptionOccurred);

            lock (Sync)
                _currentPage = subcriber;
            MessagingCenter.Subscribe<ICustomLayout, Exception>(subcriber, MessagingCenterMessage.ExceptionOccurred, OnAppExceptionOccurred);
        }

        public static void SendException(ICustomLayout sender, Exception exp)
        {
            MessagingCenter.Send<ICustomLayout, Exception>(sender, MessagingCenterMessage.ExceptionOccurred, exp);
        }

        public static void SendIncomingEvent(ICustomLayout sender, string message, object Event)
        {
            MessagingCenter.Send<ICustomLayout, MvvmMessagingCenterEventArgs>(sender, message, new MvvmMessagingCenterEventArgs(message, Event));
        }

        public static void SubcribeIncomingEvent(ICustomLayout subcriber, string message)
        {
            MessagingCenter.Unsubscribe<ICustomLayout, MvvmMessagingCenterEventArgs>(subcriber, message);

            MessagingCenter.Subscribe<ICustomLayout, MvvmMessagingCenterEventArgs>(subcriber, message, MessagingCenterIncomingEvents);
        }
    }
}