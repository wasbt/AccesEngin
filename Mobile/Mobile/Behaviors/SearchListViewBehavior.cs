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
    public class SearchListViewBehavior : Behavior<ListView>
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
            if (((ListView)sender).SelectedItem == null)
                return;

            DemandeAcces selectedDemande = (listView.SelectedItem) as DemandeAcces;
            var mdp = Application.Current.MainPage as MasterDetailPage;
            mdp.Detail.Navigation.PushAsync(new SearchResultsView(selectedDemande.Id));
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.ItemSelected -= ListView_ItemSelected;
        }
    }

   
}