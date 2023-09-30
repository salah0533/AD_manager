using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AD_manager.Models

{
    [Table("delevary_product")]
    public class Delevary_product
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //foreign key
        public int Order_id { get; set; }

        public int Product_id { get; set; }

        public int sallerID { get; set; }
        
        public double PrixAchat { get; set; }

        public int OrganizationId { get; set; }
        public double prixVont { get; set; }

        public bool isFactured { get; set; }

    }
}
