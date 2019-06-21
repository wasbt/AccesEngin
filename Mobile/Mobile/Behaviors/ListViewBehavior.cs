using Mobile.Model;
using Mobile.View;
using Mobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Behaviors
{
    public class ListViewBehavior : Behavior<ListView>
    {
        ListView listView;
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            listView = bindable;
            listView.ItemSelected += ListView_ItemSelected;
        }

        void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DemandeAcces selectedDemande = ((ListView)sender).SelectedItem as DemandeAcces;

            if (selectedDemande == null)
            {
                ((ListView)sender).SelectedItem = null;
                return;
            }

            //Application.Current.MainPage.Navigation.PushAsync(new DemandeAccesDetailsView(selectedDemande));
            var mdp = Application.Current.MainPage as MasterDetailPage;

            mdp.Detail.Navigation.PushAsync(new DemandeAccesDetailsView(selectedDemande.Id));
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.ItemSelected -= ListView_ItemSelected;
        }
    }

    public class ListViewBehaviorCheckBox : Behavior<ListView>
    {
        ListView listView;
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            listView = bindable;
            listView.ItemSelected += ListView_ItemSelected;
        }

        void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var item = (e.SelectedItem) as CheckListExigence;
            var item = ((ListView)sender).SelectedItem as CheckListExigenceVM;
            if (item == null)
            {
                ((ListView)sender).SelectedItem = null;
                return;
            }

            item.IsConforme = !item.IsConforme;
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.ItemSelected -= ListView_ItemSelected;
        }
    }
}
