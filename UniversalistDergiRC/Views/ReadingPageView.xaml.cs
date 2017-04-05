using System;
using System.Threading;
using System.Threading.Tasks;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.Repositories;
using UniversalistDergiRC.ViewModels;
using Xamarin.Forms;

namespace UniversalistDergiRC.Views
{
    public partial class ReadingPageView : ContentPage
    {
        public ReadingPageView(NavigationController navigationController)
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            BindingContext = new ReadingPageViewModel(navigationController);
            MessagingCenter.Subscribe<ReadingPageViewModel>(this, Constants.RESET_IMAGE_POSITION_MESSAGEKEY, resetImagePositionAsync);
            MessagingCenter.Subscribe<ReadingPageViewModel>(this, Constants.ANIMATE_IMAGE_MESSAGEKEY, animateImageAsync);
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            if (Height > Width)
                navigationRow.Height = new GridLength(1, GridUnitType.Star);
            else
                navigationRow.Height = new GridLength(2, GridUnitType.Star);
        }

        private async void animateImageAsync(ReadingPageViewModel obj)
        {
            if (imgActivePage == null) return;

            ViewExtensions.CancelAnimations(imgActivePage);
            double currentScale = imgActivePage.Scale;
            try
            {
                await imgActivePage.ScaleTo(currentScale * 1.1, 250);
                await imgActivePage.ScaleTo(currentScale, 250);
            }
            catch
            {// Exception is not important in animation 
            }
            finally
            {
                imgActivePage.Scale = currentScale;
            }
        }


        private async void resetImagePositionAsync(ReadingPageViewModel obj)
        {

            if (imgActivePage == null || imgActivePage.Behaviors.Count == 0 || (imgActivePage.Behaviors[0] as ZoomImageBehavior) == null)
                return;
            (imgActivePage.Behaviors[0] as ZoomImageBehavior).ResetToDefaultPosition();

            ViewExtensions.CancelAnimations(imgActivePage);
            imgActivePage.Opacity = 0;
            try
            {
                await imgActivePage.FadeTo(1, 400);
            }
            catch
            {
                // Exception is not important in animation
                imgActivePage.Opacity = 100;
            }
        }

    }
}
