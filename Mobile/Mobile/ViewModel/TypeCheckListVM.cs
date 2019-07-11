using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class TypeCheckListVM : INotifyPropertyChanged
    {
        private readonly ApiServices _apiServices = new ApiServices();

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

        public TypeCheckListVM()
        {

        }
        public TypeCheckListVM(string id)
        {
            this.GetCheckListByIdCommand.Execute(id);
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

        public ICommand GetCheckListByIdCommand
        {
            get
            {
                return new Command<long>(async (key) =>
                {
                    var accessToken = Settings.AccessToken;
                    TypeCheckList = (await Api.GetCheckListByIdAsync(key)).data;
                    Rubriques = TypeCheckList.Rubriques;
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
