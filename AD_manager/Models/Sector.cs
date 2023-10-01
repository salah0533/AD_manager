using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_manager.Models
{
    [Table("Sector")]
    public class Sector
    {
        [PrimaryKey,AutoIncrement]
        public int SectorId { get; set; }

        [Unique]
        public string SectorName { get; set; }
        public Sector() { 
        }
    }
}
