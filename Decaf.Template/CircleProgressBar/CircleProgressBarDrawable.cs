using System;
namespace Decaf.Template
{
    public class CircleProgressBarDrawable : IndeterminateRingDrawable, IDrawable
    {
        public CircleProgressBarDrawable()
        {
            Start = 0.25;
        }

        public override void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeLineCap = StrokeLineCap;

            var width = dirtyRect.Width - Thickness;
            var height = dirtyRect.Height - Thickness;

            var x = Thickness / 2f;
            var y = x;

            End = End < 0d ? 0d : (End > 1d ? 1d : End);

            if (End < 1)
            {
                canvas.StrokeColor = BackgroundColor is null ? Color.WithAlpha(0.2f) : BackgroundColor;
                canvas.StrokeSize = Thickness * 0.9f;
                canvas.DrawEllipse(x, y, width, height);

                canvas.StrokeColor = Color;
                canvas.StrokeSize = Thickness;
                canvas.DrawArc(x, y, width, height, (float)Start * 360f, (float)GetAngle(), true, false);
            }
            else
            {
                canvas.StrokeColor = Color;
                canvas.StrokeSize = Thickness;
                canvas.DrawEllipse(x, y, width, height);
            }
        }

        private float GetAngle()
        {
            var angle = (float)(Start - End) * 360f;

            return angle < 0f ? 360f + angle : angle;
        }
    }
}

