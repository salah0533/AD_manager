using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_manager.Models
{
    public class Order
    {
        [PrimaryKey,Unique]
        public string OrderId { get; set; }
        public int year { get; set; }
        public int organizationID { get; set; }

        public int receiverID { get; set; }

        public int sectorID { get; set; }
        
        public DateTime date { get; set; }

        public Order()
        {

        }

    }
}
