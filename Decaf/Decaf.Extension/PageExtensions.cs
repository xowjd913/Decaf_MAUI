using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PageExtensions
{
    public static bool IsModal(this Page page)
    {
        try
        {
            var navigation = page.Parent;
            foreach (var item in page.Navigation.ModalStack)
            {
                if (item == navigation)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());

        }
        return false;
    }
}
