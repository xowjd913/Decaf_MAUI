using System;
using System.Runtime.CompilerServices;

namespace Decaf.Template
{
	public class CircleProgressBar : GraphicsView
	{
        private readonly IndeterminateRingDrawable indeterminateDrawable = new();
        private CancellationTokenSource? indeterminateCts;
        private readonly CircleProgressBarDrawable progressDrawable = new();

        private double _prevProgress;

        public CircleProgressBar()
        {
            indeterminateDrawable.SetBinding(IndeterminateRingDrawable.ColorProperty, new Binding(ColorProperty.PropertyName, source: this));
            indeterminateDrawable.SetBinding(IndeterminateRingDrawable.BackgroundColorProperty, new Binding(BackgroundColorProperty.PropertyName, source: this));
            indeterminateDrawable.SetBinding(IndeterminateRingDrawable.ThicknessProperty, new Binding(ThicknessProperty.PropertyName, source: this));
            indeterminateDrawable.SetBinding(IndeterminateRingDrawable.StrokeLineCapProperty, new Binding(StrokeLineCapProperty.PropertyName, source: this));

            progressDrawable.SetBinding(IndeterminateRingDrawable.ColorProperty, new Binding(ColorProperty.PropertyName, source: this));
            progressDrawable.SetBinding(IndeterminateRingDrawable.BackgroundColorProperty, new Binding(BackgroundColorProperty.PropertyName, source: this));
            progressDrawable.SetBinding(IndeterminateRingDrawable.ThicknessProperty, new Binding(ThicknessProperty.PropertyName, source: this));
            progressDrawable.SetBinding(IndeterminateRingDrawable.StrokeLineCapProperty, new Binding(StrokeLineCapProperty.PropertyName, source: this));

            WidthRequest = 50d;
            HeightRequest = 50d;

            this.Drawable = IsIndeterminate ? indeterminateDrawable : progressDrawable;
        }

        protected override void OnPropertyChanging([CallerMemberName] string? propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == ProgressProperty.PropertyName)
            {
                _prevProgress = Progress;
            }
        }

        protected override void OnPropertyChanged(string? propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsIndeterminateProperty.PropertyName && IsIndeterminate)
            {
                StopProgress();

                this.Drawable = indeterminateDrawable;

                StartIndeterminate();
            }

            if (IsIndeterminate) return;

            if (propertyName == IsIndeterminateProperty.PropertyName && !IsIndeterminate)
            {
                StopIndeterminate();
            }

            this.Drawable = progressDrawable;

            if (propertyName == ProgressProperty.PropertyName && Smooth && Progress < 1)
            {
                StopProgress();

                StartProgress();
            }
            else
            {
                progressDrawable.End = Progress;
                Invalidate();
            }
        }

        private void StopProgress()
        {
            this.AbortAnimation("progress");
        }

        private void StartProgress()
        {
            this.Animate("progress", (p) =>
            {
                progressDrawable.End = p;
                Invalidate();
            }, start: _prevProgress, end: Progress, length: 200, easing: Easing.SinOut);
        }

        private void StopIndeterminate()
        {
            this.AbortAnimation("rotation");
            this.AbortAnimation("indeterminateForward");
            this.AbortAnimation("indeterminateBackward");

            this.Rotation = 0;

            try
            {
                indeterminateCts?.Cancel();
            }
            catch { }

            indeterminateCts = null;
        }

        private void StartIndeterminate()
        {
            indeterminateCts = new();

            this.Animate("rotation", de =>
            {
                this.Rotation = de;
            }, 0, 359, length: 2000, repeat: () => true);

            StartIndeterminate(0, 0, indeterminateCts.Token);
        }

        private void StartIndeterminate(double start, double end, CancellationToken token = default)
        {
            start = end;
            end = start - 0.15;
            if (end < 0) end += 1d;

            if (token.IsCancellationRequested) return;

            this.Animate("indeterminateForward", (de) =>
            {
                indeterminateDrawable.Start = start;
                indeterminateDrawable.End = de - 1d + 0.05;
                indeterminateDrawable.IsClockwise = true;
                Invalidate();
            }, start, end < start ? end + 1d : end, length: 750, easing: Easing.SinInOut, finished: async (v, c) =>
            {
                if (token.IsCancellationRequested || c) return;

                await Task.Delay(250);

                this.Animate("indeterminateBackward", (de) =>
                {
                    indeterminateDrawable.Start = start - 0.1;
                    indeterminateDrawable.End = de - 1d;
                    indeterminateDrawable.IsClockwise = false;
                    Invalidate();
                }, start, end < start ? end + 1d : end, length: 750, easing: Easing.SinInOut, finished: async (v, c) =>
                {
                    if (c) return;

                    await Task.Delay(250);

                    StartIndeterminate(start, end, token);
                });
            });
        }

        #region BackgroundColor
        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly new BindableProperty BackgroundColorProperty =
            BindableProperty.Create(
                nameof(BackgroundColor),
                typeof(Color),
                typeof(CircleProgressBar));
        #endregion

        #region Color
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(
                nameof(Color),
                typeof(Color),
                typeof(CircleProgressBar),
                Colors.Blue);
        #endregion

        #region Progress
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(
                nameof(Progress),
                typeof(double),
                typeof(CircleProgressBar));
        #endregion

        #region Thickness
        public float Thickness
        {
            get { return (float)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public static readonly BindableProperty ThicknessProperty =
            BindableProperty.Create(
                nameof(Thickness),
                typeof(float),
                typeof(CircleProgressBar),
                4f);
        #endregion

        #region Smooth
        public bool Smooth
        {
            get { return (bool)GetValue(SmoothProperty); }
            set { SetValue(SmoothProperty, value); }
        }

        public static readonly BindableProperty SmoothProperty =
            BindableProperty.Create(
                nameof(Smooth),
                typeof(bool),
                typeof(CircleProgressBar),
                false);
        #endregion

        #region IsIndeterminate
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        public static readonly BindableProperty IsIndeterminateProperty =
            BindableProperty.Create(
                nameof(IsIndeterminate),
                typeof(bool),
                typeof(CircleProgressBar),
                false);
        #endregion

        #region StrokeLineCap
        public LineCap StrokeLineCap
        {
            get { return (LineCap)GetValue(StrokeLineCapProperty); }
            set { SetValue(StrokeLineCapProperty, value); }
        }

        public static readonly BindableProperty StrokeLineCapProperty =
            BindableProperty.Create(
                nameof(StrokeLineCap),
                typeof(LineCap),
                typeof(CircleProgressBar),
                LineCap.Round);
        #endregion
    }
}

