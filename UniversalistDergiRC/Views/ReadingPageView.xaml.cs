using System;
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
            BindingContext = new ReadingPageViewModel(navigationController);
            MessagingCenter.Subscribe<ReadingPageViewModel>(this, Constants.RESET_IMAGE_POSITION_MESSAGEKEY, resetImagePosition);
            MessagingCenter.Subscribe<ReadingPageViewModel>(this, Constants.ANIMATE_IMAGE_MESSAGEKEY, animateImageAsync);
        }

        private async void animateImageAsync(ReadingPageViewModel obj)
        {
            if (imgActivePage != null)
            {
                try
                {
                    double currentScale = imgActivePage.Scale;
                    await imgActivePage.ScaleTo(currentScale * 1.1, 250);
                    await imgActivePage.ScaleTo(currentScale, 250);
                }
                catch
                {
                    // Exception is not important in animation
                }
            }
        }

        private void resetImagePosition(ReadingPageViewModel obj)
        {
            if (imgActivePage == null || imgActivePage.Behaviors.Count == 0 || (imgActivePage.Behaviors[0] as ZoomImageBehavior) == null)
                return;
            (imgActivePage.Behaviors[0] as ZoomImageBehavior).ResetToDefaultPosition();
        }
    }
}
