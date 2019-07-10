using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Extensions
{
    public static class NavigationExtensions
    {
        public static async Task PopPopupAsync(this IPopupNavigation nav, PopupPage page, bool animated = true)
        {
            if (PopupNavigation.Instance.PopupStack.Count != 0 ||
                PopupNavigation.Instance.PopupStack.Last().GetType() != page.GetType())
            {
                await nav.RemovePageAsync(page, animated);
            }
        }



        public static async Task PopAllPopupsAsync(this IPopupNavigation nav, Type pageType, bool animated = true)
        {
            if (PopupNavigation.Instance.PopupStack.Count != 0)
            {
                if (PopupNavigation.Instance.PopupStack.Any(x => x.GetType() == pageType))
                {
                    var lastPopup = PopupNavigation.Instance.PopupStack.LastOrDefault(x => x.GetType() == pageType);
                    if (lastPopup != null)
                    {
                        await PopupNavigation.Instance.RemovePageAsync(lastPopup, animated);
                    }
                    else
                    {
                        foreach (var popup in PopupNavigation.Instance.PopupStack.Where(x => x.GetType() == pageType).ToList())
                        {
                            await PopupNavigation.Instance.RemovePageAsync(popup, animated);
                        }
                    }
                }
            }

        }


        public static async Task PushPopupSingleAsync(this IPopupNavigation nav, PopupPage page, bool animated = true)
        {
            if (nav.PopupStack.Count == 0 ||
                nav.PopupStack.LastOrDefault()?.GetType() != page.GetType())
            {
                await nav.PushAsync(page, animated);
            }
        }



        public static async Task PushSingleAsync(this INavigation nav, Page page, bool animated = true)
        {
            if (nav.NavigationStack.Count == 0 ||
                nav.NavigationStack.Last().GetType() != page.GetType())
            {
                await nav.PushAsync(page, animated);
            }
        }

        public static async Task PushModalAsyncSingle(this INavigation nav, Page page, bool animated = true)
        {
            if (nav.ModalStack.Count == 0 ||
                nav.ModalStack.Last().GetType() != page.GetType())
            {
                await nav.PushModalAsync(page, animated);
            }
        }


    }
}





