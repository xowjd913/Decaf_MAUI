using Decaf.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decaf.Base.MVVM
{
    public class ViewModelBase : BindableBase, IDestructible, IApplicationLifecycleAware, IPageLifecycleAware, INavigationAware, IInitialize, INavigationPageOptions
    {
        public string TitlePage { get; set; }

        public INavigationService NavigationService { get; }
        
        public virtual bool ClearNavigationStackOnNavigation => true;

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnResume()
        {

        }

        public virtual void OnSleep()
        {

        }

        public virtual void OnAppearing()
        {

        }

        public virtual void OnDisappearing()
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public void Destroy()
        {
        }

        public async Task<INavigationResult> GoBackAsync()
        {
            var isModal = PageUtilities.IsCurrentPageModal();

            var navResult = await NavigateGoBack(isModal);
            if (!navResult.Success)
                navResult = await NavigateGoBack(!isModal);

            return navResult;
        }

        async Task<INavigationResult> NavigateGoBack(bool isModal = false)
        {
            var navParams = new NavigationParameters()
            {
                {KnownNavigationParameters.UseModalNavigation, isModal }
            };

            return await NavigationService.GoBackAsync(navParams);
        }
    }
}
