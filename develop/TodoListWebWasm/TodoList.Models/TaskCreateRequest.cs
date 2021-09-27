using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TaskCreateRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(20, ErrorMessage = "Độ dài chuỗi không quá 20 kí tự.")]
        [Required(ErrorMessage = "Vui lòng nhập tên. ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn độ ưu tiên.")]
        public Priority? Priority { get; set; }
    }
}
