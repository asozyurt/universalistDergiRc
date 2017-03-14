using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UniversalistDergiRC.ViewModels
{
    public class CustomCarousel :CarouselPage
    {
        private bool _internalPageChange;



        public CustomCarousel()
        {
            CurrentPageChanged += OnCurrentPageChanged;
        }

        protected override bool OnBackButtonPressed()
        {
            if (SelectedIndex > 0)
            {
                SelectedIndex--;
                return true;
            }

            return base.OnBackButtonPressed();
        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create("SelectedIndex", typeof(int), typeof(CustomCarousel), -1, BindingMode.TwoWay,
                (BindableProperty.ValidateValueDelegate)null, OnIndexPropertyChanged,
                (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null,
                (BindableProperty.CreateDefaultValueDelegate)null);

        private static void OnIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var CustomCarousel = bindable as CustomCarousel;
            if (CustomCarousel != null && !CustomCarousel._internalPageChange)
            {
                var index = (int)newValue;
                CustomCarousel.SetCurrentPageByIndex(index);
            }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        private void OnCurrentPageChanged(object sender, EventArgs eventArgs)
        {
            if (!_internalPageChange)
                return;

            var CustomCarousel = sender as CustomCarousel;
            if (CustomCarousel != null)
            {
                var currentPage = CustomCarousel.CurrentPage;
                SetSelectedIndexByPage(currentPage);
            }
        }

        protected virtual void SetSelectedIndexByPage(ContentPage contentPage)
        {
            _internalPageChange = true;
            var index = Children.IndexOf(contentPage);
            SelectedIndex = index;
            _internalPageChange = false;
        }

        protected virtual void SetCurrentPageByIndex(int index)
        {
            _internalPageChange = true;
            SelectedIndex = index;
            if (index > Children.Count - 1 || index == -1)
            {
                _internalPageChange = false;
                return;
            }

            var currentPage = Children[index];
            if (currentPage != null)
            {
                CurrentPage = currentPage;
            }

            _internalPageChange = false;
        }

        protected override ContentPage CreateDefault(object item)
        {
            var contentPage = new ContentPage();
            if (item != null)
                contentPage.Title = item.ToString();
            return contentPage;
        }
    }
}
