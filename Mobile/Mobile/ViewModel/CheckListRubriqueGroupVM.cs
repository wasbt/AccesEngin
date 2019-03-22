using Mobile.Helpers;
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

        private readonly ApiServices _apiServices = new ApiServices();

        private CheckListRubriqueVM _oldCheckListRubrique;
        public long DemandeAccesEnginId { get; set; }


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
                    await ExecuteLoadItemsCommandAsync();
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

        public CheckListRubriqueGroupVM(long Id)
        {
            DemandeAccesEnginId = Id;
            this.GetCheckListByIdCommand.Execute(Id);
            items = new ObservableCollection<CheckListRubriqueVM>();
            Items = new ObservableCollection<CheckListRubriqueVM>();
            LoadCheckListRubriqueCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());
            RefreshItemsCommand = new Command<CheckListRubriqueVM>((item) => ExecuteRefreshItemsCommand(item));

        }
        public bool isExpanded = true;

        private void ExecuteRefreshItemsCommand(CheckListRubriqueVM item)
        {
            //if (_oldCheckListRubrique == item)
            //{
            //    // click twice on the same item will hide it
            //    item.Expanded = !item.Expanded;
            //}
            //else
            //{
            //if (_oldCheckListRubrique != null)
            //{
            //    // hide previous selected item
            //    _oldCheckListRubrique.Expanded = false;

            //}
            // show selected item
            if (item.Expanded)
            {
                item.Expanded = false;
            }
            else
            {

                item.Expanded = true;
            }
            //}

            //_oldCheckListRubrique = item;
        }

        async Task ExecuteLoadItemsCommandAsync()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                Items.Clear();


                if (rubriques != null && rubriques.Count > 0)
                {
                    foreach (var checkListRubrique in TypeCheckList.Rubriques)
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
            get => typeCheckList;

            set => SetProperty(ref typeCheckList, value);

        }

        private List<CheckListRubrique> rubriques;

        public List<CheckListRubrique> Rubriques
        {
            get => rubriques;

            set => SetProperty(ref rubriques, value);
        }
        private ResultatCheckList ResultatCheckList;

        public ICommand AddCommand
        {
            get
            {
                return new Command<ObservableCollection<CheckListRubriqueVM>>((Rubrique) =>
                {
                    ResultatCheckList = new ResultatCheckList();
                    ResultatCheckList.ResultatsList = new List<Resultats>();
                    var data = Rubrique;
                    ResultatCheckList.DemandeAccesEnginId = DemandeAccesEnginId;
                    ResultatCheckList.CreatedBy = Settings.Username;
                    ResultatCheckList.CreatedOn = DateTime.Now;

                    foreach (var rubrique in Rubrique)
                    {
                        foreach (var exigence in rubrique)
                        {
                            var ex = new Resultats()
                            {
                                CheckListExigenceId = exigence.Id,
                                IsConform = exigence.IsConforme,
                                Date = exigence.Date,
                                Observation = exigence.Observation,
                            };
                            ResultatCheckList.ResultatsList.Add(ex);
                        }

                    }

                    //await _apiServices.PostIdeaAsync(idea, Settings.AccessToken);
                });
            }
        }

    }

}
