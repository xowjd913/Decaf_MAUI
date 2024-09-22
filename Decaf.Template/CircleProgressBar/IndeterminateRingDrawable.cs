using System;
namespace Decaf.Template
{
    public class IndeterminateRingDrawable : BindableObject, IDrawable
    {
        public double Start { get; set; }
        public double End { get; set; }
        public bool IsClockwise { get; set; }

        public virtual void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeLineCap = StrokeLineCap;

            var width = dirtyRect.Width - Thickness;
            var height = dirtyRect.Height - Thickness;

            var x = Thickness / 2f;
            var y = x;

            canvas.StrokeColor = BackgroundColor is null ? Color.WithAlpha(0.2f) : BackgroundColor;
            canvas.StrokeSize = Thickness * 0.9f;
            canvas.DrawEllipse(x, y, width, height);

            canvas.StrokeColor = Color;
            canvas.StrokeSize = Thickness;

            var start = 360f - (float)Start * 360f;
            var end = 360f - (float)End * 360f;

            canvas.DrawArc(x, y, width, height, start, end, IsClockwise, false);
        }

        #region BackgroundColor
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(
                nameof(BackgroundColor),
                typeof(Color),
                typeof(IndeterminateRingDrawable));
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
                typeof(IndeterminateRingDrawable),
                Colors.Blue);
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
                typeof(IndeterminateRingDrawable),
                4f);
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
                typeof(IndeterminateRingDrawable),
                LineCap.Round);
        #endregion
    }
}

