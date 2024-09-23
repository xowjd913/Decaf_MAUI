using System;
using System.Diagnostics;
using Decaf.OnBoarding.Models;

namespace Decaf.OnBoarding.Selector
{
    public class OnBoardingPageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StandardPageTemplate { get; set; }
        public DataTemplate FinalPageTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var onBoardingPageItem = item as OnBoardingSurveyPage;
            if (onBoardingPageItem.IsFinalPage)
                return FinalPageTemplate;
            
            return StandardPageTemplate;
        }
    }
}

