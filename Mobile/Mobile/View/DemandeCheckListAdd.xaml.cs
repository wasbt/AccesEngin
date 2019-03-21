using Mobile.Model;
using Mobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DemandeCheckListAdd : ContentPage
	{
        //TypeCheckListVM ViewModel;
        private CheckListRubriqueGroupVM ViewModel
        {
            get { return (CheckListRubriqueGroupVM)BindingContext; }
            set { BindingContext = value; }
        }

        private List<CheckListRubrique> ListCheckListRubrique = new List<CheckListRubrique>();


        public DemandeCheckListAdd ()
		{
			InitializeComponent ();
        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (ViewModel.Items.Count == 0)
                {
                    //BindingContext = new CheckListRubriqueGroupVM("1");
                    //ViewModel.LoadCheckListRubriqueCommand.Execute(null);
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
            }
        }

        public DemandeCheckListAdd(CheckListRubriqueGroupVM viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
        }

        public DemandeCheckListAdd(string id)
        {
            InitializeComponent();
            ViewModel = new CheckListRubriqueGroupVM(id);
        }
    }
}