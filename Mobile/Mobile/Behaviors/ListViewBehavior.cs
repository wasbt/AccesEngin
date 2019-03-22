﻿using Mobile.Model;
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
            DemandeAcces selectedDemande = (listView.SelectedItem) as DemandeAcces;
            //Application.Current.MainPage.Navigation.PushAsync(new DemandeAccesDetailsView(selectedDemande));
            var mdp = Application.Current.MainPage as MasterDetailPage;
             mdp.Detail.Navigation.PushAsync(new DemandeAccesDetailsView(selectedDemande));
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
                return;

            item.IsConforme =! item.IsConforme;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.ItemSelected -= ListView_ItemSelected;
        }
    }
}
