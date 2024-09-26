using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccess.Models
{
    public class Unit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Code { get; set; }

        [Required, MaxLength(500)]
        public string Name { get; set; }       

        [Required]
        public bool IsActive { get; set; }

        public ICollection<User> Users { get; }

        public Unit()
        {
        }

        public Unit(int code, string name, bool isActive)
        {            
            Code = code;
            Name = name;
            IsActive = isActive;
        }

        public override string ToString()
        {
            return $"{Id}, {Code}, {Name}, {IsActive}";
        }
    }
}
