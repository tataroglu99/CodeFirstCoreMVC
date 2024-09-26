using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string Name { get; set; }

        [Required, MaxLength(50)]
        public required string Surname { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int UnitId { get; set; }
        public required Unit Unit { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Surname}, {Unit.Name}, {IsActive}";
        }
    }
}
