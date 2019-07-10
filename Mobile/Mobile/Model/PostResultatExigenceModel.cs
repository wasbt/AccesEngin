using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Model
{
    public class PostResultatExigenceModel
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public string ResultatExigencJson { get; set; }
    }
}
