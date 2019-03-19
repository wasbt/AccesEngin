using Mobile.Helpers;
using Mobile.Model;
using Mobile.Services;
using System;
using System.Collections.Generic;
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

        private TypeCheckList typeCheckList;

        public TypeCheckListVM()
        {

        }

        public TypeCheckList TypeCheckList
        {
            get { return TypeCheckList; }
            set
            {
                TypeCheckList = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetDemandeAccesCommand
        {
            get
            {
                return new Command<object>(async (key) =>
                {
                    var accessToken = Settings.AccessToken;
                    TypeCheckList = await _apiServices.GetCheckListByIdAsync(key.ToString(),accessToken);
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
