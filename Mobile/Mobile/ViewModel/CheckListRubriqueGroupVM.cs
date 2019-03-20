﻿using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class CheckListRubriqueGroupVM : BaseViewModel
    {


       
        private CheckListRubriqueVM _oldCheckListRubrique;
        private readonly ApiServices _apiServices = new ApiServices();
        public CheckListRubriqueGroupVM()
        {
           


        }

        public ICommand GetCheckListByIdCommand
        {
            get
            {
                return new Command<object>(async (key) =>
                {
                    var accessToken = Settings.AccessToken;
                    TypeCheckList = await _apiServices.GetCheckListByIdAsync(key.ToString(), accessToken);
                    Rubriques = TypeCheckList.Rubriques;
                });
            }
        }

        private ObservableCollection<CheckListRubriqueVM> items;
        public ObservableCollection<CheckListRubriqueVM> Items
        {
            get => items;

            set => SetProperty(ref items, value);
        }

        public Command LoadCheckListRubriqueCommand { get; set; }
        public Command<CheckListRubriqueVM> RefreshItemsCommand { get; set; }

        public CheckListRubriqueGroupVM(string Id)
        {
            this.GetCheckListByIdCommand.Execute(Id);
            items = new ObservableCollection<CheckListRubriqueVM>();
            Items = new ObservableCollection<CheckListRubriqueVM>();
            LoadCheckListRubriqueCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());
            RefreshItemsCommand = new Command<CheckListRubriqueVM>((item) => ExecuteRefreshItemsCommand(item));
        }
        public bool isExpanded = false;

        private void ExecuteRefreshItemsCommand(CheckListRubriqueVM item)
        {
            if (_oldCheckListRubrique == item)
            {
                // click twice on the same item will hide it
                item.Expanded = !item.Expanded;
            }
            else
            {
                if (_oldCheckListRubrique != null)
                {
                    // hide previous selected item
                    _oldCheckListRubrique.Expanded = false;
                }
                // show selected item
                item.Expanded = true;
            }

            _oldCheckListRubrique = item;
        }

        async System.Threading.Tasks.Task ExecuteLoadItemsCommandAsync()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
               Items.Clear();

              
                if (rubriques != null && rubriques.Count > 0)
                {
                    foreach (var checkListRubrique in rubriques)
                        Items.Add(new CheckListRubriqueVM(checkListRubrique));
                }
                else { IsEmpty = true; }

            }
            catch (Exception ex)
            {
                IsBusy = false;
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private TypeCheckList typeCheckList;

        public TypeCheckList TypeCheckList
        {
            get { return typeCheckList; }
            set
            {
                typeCheckList = value;
                OnPropertyChanged();
            }
        }

        private List<CheckListRubrique> rubriques;

        public List<CheckListRubrique> Rubriques
        {
            get { return rubriques; }
            set
            {
                rubriques = value;
                OnPropertyChanged();
            }
        }
    }
}
