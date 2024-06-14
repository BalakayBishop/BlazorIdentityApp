using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Records
    {
        [Key] 
        public int Id { get; set; }

        [Required, NotNull]
        [StringLength(50)]
        public string Title { get; set; }

        [Required, NotNull]
        public string Description { get; set; }

        [Required, NotNull]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<UserRecords> UserRecords { get; set; } = new List<UserRecords>();
    }
}
