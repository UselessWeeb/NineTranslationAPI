using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class VnPaySettings
    {
        public required string TmnCode { get; set; }
        public required string VnPayUrl { get; set; }
        public required string VnPayReturnUrl { get; set; }
        public required string VnPayHashSecret { get; set; }
        public required string VnPayAPIUrl { get; set; }
    }
}
