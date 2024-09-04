using Decaf.Base.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decaf.Main.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(
                INavigationService navigationService
            ) 
            : base(navigationService)
        {
            TitlePage = "MainPage";
        }
    }
}
