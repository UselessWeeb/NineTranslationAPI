using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class UpdatePatchUpdateDto
    {
        public int? Id { get; set; }
        public string? Version { get; set; }
        public string? Detail { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public int ProjectDetailId { get; set; }
    }
}
