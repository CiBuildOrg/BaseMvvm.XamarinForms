using System;
using System.Threading.Tasks;
using BaseMvvm.XamarinForms.Interfaces;
using BaseMvvm.XamarinForms.Models;
using Xamarin.Forms;

namespace BaseMvvm.XamarinForms.Helpers
{
    public static class LayoutExtensio
    {
        #region main page change

        //https://forums.xamarin.com/discussion/26870/changing-root-from-a-navigation-page
        public static async Task ChangeMainPage(this ICustomLayout app, Page page)
        {
            var root = app.NavPage.Navigation.NavigationStack[0];
            app.NavPage.Navigation.InsertPageBefore(page, root);
            await PopToRootAsync(app.NavPage);
        }

        private static async Task PopToRootAsync(NavigationPage np)
        {
            while (np.Navigation.ModalStack.Count > 0)
            {
                await np.Navigation.PopModalAsync(false);
            }
            while (np.CurrentPage != np.Navigation.NavigationStack[0])
            {
                await np.PopAsync(false);
            }
        }

        #endregion main page change

        /// <summary>
        /// caster of EventProperty 
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="args">
        /// </param>
        /// <returns>
        /// </returns>
        public static T Cast<T>(this MvvmMessagingCenterEventArgs args)
        {
            return (T)args.Event;
        }
    }
}