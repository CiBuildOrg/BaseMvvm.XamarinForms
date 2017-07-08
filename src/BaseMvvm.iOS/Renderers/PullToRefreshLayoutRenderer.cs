using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BaseMvvm.iOS.Renderers;
using BaseMvvm.XamarinForms.Layouts;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PullToRefreshLayout), typeof(PullToRefreshLayoutRenderer))]

namespace BaseMvvm.iOS.Renderers
{
    /// <summary>
    /// Pull to refresh layout. thanks to jamesmontemagno 
    /// </summary>
    //https://github.com/jamesmontemagno/Xamarin.Forms-PullToRefreshLayout
    [Preserve(AllMembers = true)]
    public class PullToRefreshLayoutRenderer : ViewRenderer<PullToRefreshLayout, UIView>
    {
        /// <summary>
        /// Used for registration with dependency service 
        /// </summary>
        public async static void Init()
        {
            var temp = DateTime.Now;
        }

        private UIRefreshControl refreshControl;

        /// <summary>
        /// Raises the element changed event. 
        /// </summary>
        /// <param name="e">
        /// E. 
        /// </param>
        protected override void OnElementChanged(ElementChangedEventArgs<PullToRefreshLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            refreshControl = new UIRefreshControl();

            refreshControl.ValueChanged += OnRefresh;

            try
            {
                TryInsertRefresh(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("View is not supported in PullToRefreshLayout: " + ex);
            }

            UpdateColors();
            UpdateIsRefreshing();
            UpdateIsSwipeToRefreshEnabled();
        }

        private bool TryInsertRefresh(UIView view, int index = 0)
        {
            if (view is UITableView)
            {
                view.InsertSubview(refreshControl, index);
                return true;
            }

            if (view is UICollectionView)
            {
                view.InsertSubview(refreshControl, index);
                return true;
            }

            var uiWebView = view as UIWebView;
            if (uiWebView != null)
            {
                uiWebView.ScrollView.InsertSubview(refreshControl, index);
                return true;
            }

            var uIScrollView = view as UIScrollView;
            if (uIScrollView != null)
            {
                view.InsertSubview(refreshControl, index);
                uIScrollView.AlwaysBounceVertical = true;
                return true;
            }

            if (view.Subviews == null)
                return false;

            for (int i = 0; i < view.Subviews.Length; i++)
            {
                var control = view.Subviews[i];
                if (TryInsertRefresh(control, i))
                    return true;
            }

            return false;
        }

        private BindableProperty rendererProperty;

        /// <summary>
        /// Gets the bindable property. 
        /// </summary>
        /// <returns>
        /// The bindable property. 
        /// </returns>
        private BindableProperty RendererProperty
        {
            get
            {
                if (rendererProperty != null)
                    return rendererProperty;

                var type = Type.GetType("Xamarin.Forms.Platform.iOS.Platform, Xamarin.Forms.Platform.iOS");
                var prop = type.GetField("RendererProperty");
                var val = prop.GetValue(null);
                rendererProperty = val as BindableProperty;

                return rendererProperty;
            }
        }

        private void UpdateColors()
        {
            if (RefreshView == null)
                return;
            if (RefreshView.RefreshColor != Color.Default)
                refreshControl.TintColor = RefreshView.RefreshColor.ToUIColor();
            if (RefreshView.RefreshBackgroundColor != Color.Default)
                refreshControl.BackgroundColor = RefreshView.RefreshBackgroundColor.ToUIColor();
        }

        private void UpdateIsRefreshing()
        {
            IsRefreshing = RefreshView.IsRefreshing;
        }

        private void UpdateIsSwipeToRefreshEnabled()
        {
            refreshControl.Enabled = RefreshView.IsPullToRefreshEnabled;
        }

        /// <summary>
        /// Helpers to cast our element easily Will throw an exception if the Element is not correct 
        /// </summary>
        /// <value>
        /// The refresh view. 
        /// </value>
        public PullToRefreshLayout RefreshView
        {
            get { return Element; }
        }

        private bool isRefreshing;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is refreshing. 
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is refreshing; otherwise, <c> false </c>. 
        /// </value>
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                if (isRefreshing)
                    refreshControl.BeginRefreshing();
                else
                    refreshControl.EndRefreshing();
            }
        }

        /// <summary>
        /// The refresh view has been refreshed 
        /// </summary>
        private void OnRefresh(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                //someone pulled down to refresh or it is done
                if (RefreshView == null)
                    return;

                var command = RefreshView.RefreshCommand;
                if (command == null)
                    return;

                command.Execute("@OnPullToRefresh");
                UpdateIsRefreshing();
            });
        }

        /// <summary>
        /// Raises the element property changed event. 
        /// </summary>
        /// <param name="sender">
        /// Sender. 
        /// </param>
        /// <param name="e">
        /// E. 
        /// </param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == PullToRefreshLayout.IsPullToRefreshEnabledProperty.PropertyName)
                UpdateIsSwipeToRefreshEnabled();
            else if (e.PropertyName == PullToRefreshLayout.IsRefreshingProperty.PropertyName)
                UpdateIsRefreshing();
            else if (e.PropertyName == PullToRefreshLayout.RefreshColorProperty.PropertyName)
                UpdateColors();
            else if (e.PropertyName == PullToRefreshLayout.RefreshBackgroundColorProperty.PropertyName)
                UpdateColors();
        }

        /// <summary>
        /// Dispose the specified disposing. 
        /// </summary>
        /// <param name="disposing">
        /// If set to <c> true </c> disposing. 
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (refreshControl != null)
            {
                refreshControl.ValueChanged -= OnRefresh;
            }
        }
    }
}