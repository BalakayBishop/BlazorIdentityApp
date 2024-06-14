using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UserRecords
    {
        [Key]
        public int RecordId { get; set; }

        [Key]
        public int UserId { get; set; }

        [ForeignKey("RecordId")]
        public Records Record { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }
    }
}
