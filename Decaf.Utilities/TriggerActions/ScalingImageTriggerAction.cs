using System;
namespace Decaf.Utility.TriggerActions
{
    public class ScalingImageTriggerAction : TriggerAction<VisualElement>
    {
        public double StartScale { get; set; } = 0;
        public double EndScale { get; set; } = 1.2;
        public uint Duration { get; set; } = 1000;
        public Easing AnimationEasing { get; set; } = Easing.CubicOut;

        protected override async void Invoke(VisualElement sender)
        {
            // 애니메이션 실행
            sender.Scale = StartScale;
            await sender.ScaleTo(EndScale, Duration, AnimationEasing);
            if (EndScale != 1)
                await sender.ScaleTo(1, Duration, AnimationEasing);
        }
    }
}

