using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.View.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageNavigationMaster : ContentPage
    {
        public ListView ListView;

        public MasterDetailPageNavigationMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPageNavigationMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailPageNavigationMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPageNavigationMenuItem> MenuItems { get; set; }
            
            public MasterDetailPageNavigationMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPageNavigationMenuItem>(new[]
                {
                    new MasterDetailPageNavigationMenuItem { Id = 0, Title = "Page 1" },
                    new MasterDetailPageNavigationMenuItem { Id = 1, Title = "Page 2" },
                    new MasterDetailPageNavigationMenuItem { Id = 2, Title = "Page 3" },
                    new MasterDetailPageNavigationMenuItem { Id = 3, Title = "Page 4" },
                    new MasterDetailPageNavigationMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}