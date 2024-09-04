using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decaf.Utilities
{
    public static class PageUtilities
    {
        public static Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage;
            if (currentPage is FlyoutPage flyoutPage)
            {
                var flyoutPageDetail = flyoutPage.Detail;
                var navigation = flyoutPageDetail.Navigation;

                var navigationModalStack = navigation.ModalStack;
                if (navigationModalStack.Count() > 0)
                    return navigationModalStack.Last();

                var navigationStack = navigation.NavigationStack;
                return navigationStack.Last();
            }

            return currentPage;
        }

        public static bool IsCurrentPageModal()
            => GetCurrentPage().IsModal();
    }
}
