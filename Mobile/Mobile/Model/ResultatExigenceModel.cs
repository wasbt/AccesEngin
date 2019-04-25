using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Model
{
    public class ResultatExigenceModel 
    {
      

        public DemandeAccesDto DemandeAccesDto { get; set; }
        public List<Group> ResultatValueGrouping { get; set; }
    }

    public class ResultatValue : INotifyPropertyChanged
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Conform { get; set; }
        public string Observation { get; set; }
        public string Datetime { get; set; }
        public string Color { get; set; }
        public bool IsInfoG { get; set; }
        public bool IsExigence { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


    }

    public class Group : BindableObject
    {
        public string Key { get; set; }
        public string ColorRubrique { get; set; }
        public List<ResultatValue> ResultatValue { get; set; }

    }
}
