using Mobile.Helpers;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class SearchResultsVM : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        public SearchResultsVM()
        {

        }

        private ICommand _SearchResultsCommand;
        public ICommand SearchResultsCommand
        {
            get
            {
                return _SearchResultsCommand ?? (_SearchResultsCommand = new Command<long>(async (Id) =>
                {
                    if (Id > 0)
                    {
                        var accessToken = Settings.AccessToken;
                        var t = await _apiServices.GetResultatExigenceByDemandeAccesId(Id, accessToken);
                    }
                }));
            }
        }
    }
}
