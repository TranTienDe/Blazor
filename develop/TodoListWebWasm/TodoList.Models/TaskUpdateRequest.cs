using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TaskUpdateRequest
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public Priority Priority { get; set; }
    }
}
