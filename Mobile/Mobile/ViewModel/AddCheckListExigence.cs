using Mobile.Model;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.ViewModel
{
    public class AddCheckListExigence
    {

        ApiServices _apiServices = new ApiServices();
        public List<bool> IsConforme { get; set; }
        public List<DateTime> Date { get; set; }
        public List<string> Observation { get; set; }

        public ICommand AddCommand
        {
            get
            {
                return new Command<CheckListExigence>((CheckListExigence checkList) =>
                {
                    var data = checkList;
                    //await _apiServices.PostIdeaAsync(idea, Settings.AccessToken);
                });
            }
        }
    }
    class Resultat
    {
        public List<bool> IsConforme { get; set; }
        public List<DateTime> Date { get; set; }
        public List<string> Observation { get; set; }
    }
}