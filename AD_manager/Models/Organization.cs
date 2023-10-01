using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_manager.Models
{
    [Table("Organization")]
    public class Organization
    {
        [PrimaryKey, AutoIncrement]
        public int OrganizationId { get; set; }

        [MaxLength(255),Unique]
        public string OrganizationName { get; set; }
        [MaxLength(255)]
        public string wilaya { get; set; }
        [MaxLength(255)]
        public string baladia { get; set; }
        [MaxLength(255)]
        public string adress { get; set; }

        public Organization()
        {

        }
    }
}
