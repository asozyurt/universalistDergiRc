/*
 Ahmet Selçuk Özyurt
 13.03.2017
 www.asozyurt.com
 Zoom In-Zoom Out Image with Xamarin Forms
 
 Don't bother me with license. Use it, build it and develop it as you want.

 */

using System;
using UniversalistDergiRC.Repositories;
using Xamarin.Forms;

namespace UniversalistDergiRC.Core
{
    public class ZoomImageBehavior : Behavior<View>
    {
        #region Fields
        private View _associatedObject;
        private double _currentScale = 1, _startScale = 1, _xOffset, _yOffset;

        private PanGestureRecognizer _panGestureRecognizer;
        private ContentView _parent;
        private PinchGestureRecognizer _pinchGestureRecognizer;
        private TapGestureRecognizer _tapGestureRecognizer;

        long panStartTime, panEndTime;
        double panStartX, panStartY;
        
        #endregion

        public double Clamp(double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }

        internal void ResetToDefaultPosition()
        {
            _parent.Content.TranslationX = 0;
            _parent.Content.TranslationY = 0;

            _parent.Content.Scale = 1;
            _currentScale = 1;
            _xOffset = 0;
            _yOffset = 0;
        }

        protected override void OnAttachedTo(View associatedObject)
        {
            InitializeRecognizers();
            _associatedObject = associatedObject;
            InitializeEvents();

            base.OnAttachedTo(associatedObject);
        }

        protected override void OnDetachingFrom(View associatedObject)
        {
            CleanupEvents();

            _parent = null;
            _pinchGestureRecognizer = null;
            _panGestureRecognizer = null;
            _tapGestureRecognizer = null;
            _associatedObject = null;

            base.OnDetachingFrom(associatedObject);
        }

        private void AssociatedObjectBindingContextChanged(object sender, EventArgs e)
        {
            if (_associatedObject == null && (sender as Image) != null)
                _associatedObject = sender as Image;

            _parent = _associatedObject.Parent as ContentView;

            _parent?.GestureRecognizers.Remove(_panGestureRecognizer);
            _parent?.GestureRecognizers.Add(_panGestureRecognizer);
            _parent?.GestureRecognizers.Remove(_pinchGestureRecognizer);
            _parent?.GestureRecognizers.Add(_pinchGestureRecognizer);
            _parent?.GestureRecognizers.Remove(_tapGestureRecognizer);
            _parent?.GestureRecognizers.Add(_tapGestureRecognizer);
        }

        /// <summary>
        /// Cleanup the events.
        /// </summary>
        private void CleanupEvents()
        {
            _pinchGestureRecognizer.PinchUpdated -= OnPinchUpdated;
            _panGestureRecognizer.PanUpdated -= OnPanUpdated;
            _tapGestureRecognizer.Tapped -= OnTapUpdated;
            _associatedObject.BindingContextChanged -= AssociatedObjectBindingContextChanged;
        }

        /// <summary>
        /// Initialise the events.
        /// </summary>
        private void InitializeEvents()
        {
            CleanupEvents();

            _pinchGestureRecognizer.PinchUpdated += OnPinchUpdated;
            _panGestureRecognizer.PanUpdated += OnPanUpdated;
            _tapGestureRecognizer.Tapped += OnTapUpdated;
            _associatedObject.BindingContextChanged += AssociatedObjectBindingContextChanged;
            AssociatedObjectBindingContextChanged(_associatedObject, null);
        }

        private void InitializeRecognizers()
        {
            _pinchGestureRecognizer = new PinchGestureRecognizer();
            _panGestureRecognizer = new PanGestureRecognizer();
            _tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (_parent == null || !IsTranslateEnabled)
            {
                return;
            }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    panStartX = _xOffset;
                    panStartY = _yOffset;
                    panStartTime = DateTime.Now.Ticks;
                    break;
                case GestureStatus.Running:
                    _parent.Content.TranslationX = _xOffset + e.TotalX;
                    _parent.Content.TranslationY = _yOffset + e.TotalY;
                    break;
                case GestureStatus.Completed:
                    _xOffset = _parent.Content.TranslationX;
                    _yOffset = _parent.Content.TranslationY;

                    if (_currentScale == 1)
                    {
                        panEndTime = DateTime.Now.Ticks;
                        if (panEndTime - panStartTime < 1500000)
                        {
                            double yDifference = panStartY - _yOffset;
                            double xDifference = panStartX - _xOffset;

                            if (yDifference > -100 && yDifference < 100
                                && (xDifference < -35 || xDifference > 35))
                            {
                                // If x is bigger than 0 sliding direction is right, otherwise left
                                MessagingCenter.Send(this, xDifference > 0 ? Constants.RIGHT_SLIDE : Constants.LEFT_SLIDE);
                            }
                            panStartX = 0;
                            panStartY = 0;
                        }

                    }
                    break;
            }
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (_parent == null || !IsScaleEnabled)
            {
                return;
            }

            switch (e.Status)
            {
                case GestureStatus.Started:
                    _startScale = _parent.Content.Scale;
                    _parent.Content.AnchorX = 0;
                    _parent.Content.AnchorY = 0;
                    break;
                case GestureStatus.Running:
                    _currentScale += (e.Scale - 1) * _startScale;
                    _currentScale = Math.Max(1, _currentScale);

                    var renderedX = _parent.Content.X + _xOffset;
                    var deltaX = renderedX / _parent.Width;
                    var deltaWidth = _parent.Width / (_parent.Content.Width * _startScale);
                    var originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;
                    var renderedY = _parent.Content.Y + _yOffset;
                    var deltaY = renderedY / _parent.Height;
                    var deltaHeight = _parent.Height / (_parent.Content.Height * _startScale);
                    var originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                    var targetX = _xOffset - (originX * _parent.Content.Width) * (_currentScale - _startScale);
                    var targetY = _yOffset - (originY * _parent.Content.Height) * (_currentScale - _startScale);

                    _parent.Content.TranslationX = Clamp(targetX, -_parent.Content.Width * (_currentScale - 1), 0);
                    _parent.Content.TranslationY = Clamp(targetY, -_parent.Content.Height * (_currentScale - 1), 0);

                    _parent.Content.Scale = _currentScale;
                    break;

                case GestureStatus.Completed:
                    _xOffset = _parent.Content.TranslationX;
                    _yOffset = _parent.Content.TranslationY;
                    break;
            }
        }

        private void OnTapUpdated(object sender, EventArgs e)
        {
            if (_parent == null || !IsScaleEnabled)
            {
                return;
            }
            // If scale is 1 zoom in for 1.3 scale 
            if (_parent.Content.Scale == 1)
            {
                OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Started, 0, new Point(0, 0)));
                OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Running, 1.3, new Point(0, 0)));
                OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Completed, 0, new Point(0, 0)));
            }
            // If scale is not 1 set scale to 1 which is its first scale
            else
            {
                ResetToDefaultPosition();
            }
        }
        #region IsScaleEnabled property
        public static readonly BindableProperty IsScaleEnabledProperty =
            BindableProperty.Create("IsScaleEnabled", typeof(bool), typeof(ZoomImageBehavior), default(bool));
        //BindableProperty.Create<MultiTouchBehavior, bool>(w => w.IsScaleEnabled, default(bool));

        public bool IsScaleEnabled
        {
            get { return (bool)GetValue(IsScaleEnabledProperty); }
            set { SetValue(IsScaleEnabledProperty, value); }
        }
        #endregion

        #region IsTranslateEnabled property
        public static readonly BindableProperty IsTranslateEnabledProperty =

        // BindableProperty.Create<MultiTouchBehavior, bool>(w => w.IsTranslateEnabled, default(bool));
        BindableProperty.Create("IsTranslateEnabled", typeof(bool), typeof(ZoomImageBehavior), default(bool));
        public bool IsTranslateEnabled
        {
            get { return (bool)GetValue(IsTranslateEnabledProperty); }
            set { SetValue(IsTranslateEnabledProperty, value); }
        }

        #endregion

    }
}