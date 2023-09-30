using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_manager.Models

{
    [Table("Reciver")]
   public class Reciver
    {
        [PrimaryKey,AutoIncrement]
        public int ReciverId { get; set; }
        [Unique]
        public string ReciverName { get; set; }
    }
}
