using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_manager.Models

{
    [Table("Saller")]
    public class Saller
    {
        [PrimaryKey,AutoIncrement]
        public int sallerId {  get; set; }
        
        [Unique]
        public string sallerName { get; set; }
        public string wilaya { get; set; }
        public string baladia { get; set; }
        public string adress { get;set; }

    }
}
