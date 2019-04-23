﻿using Mobile.Helpers;
using Mobile.Services;
using Shared.Models;
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
        long Id = 0;
     
        public ResultatExigenceModel resultatExigence;

        public ResultatExigenceModel ResultatExigence
        {
            get => resultatExigence;

            set => SetProperty(ref resultatExigence, value);
        }

        public SearchResultsVM(long id)
        {
             Id = id;
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            SearchResultsCommand?.Execute(Id);
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
                        ResultatExigence = await _apiServices.GetResultatExigenceByDemandeAccesId(Id, accessToken);
                    }
                }));
            }
        }
    }
}
