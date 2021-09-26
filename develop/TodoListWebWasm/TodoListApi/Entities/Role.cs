using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.Entities
{
    public class Role : IdentityRole<Guid>
    {
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
