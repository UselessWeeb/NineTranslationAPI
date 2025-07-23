using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils {
    public class EmailSettings
    {
        public required string SmtpServer { get; set; }
        public int Port { get; set; }
        public required string SENDER_EMAIL { get; set; }
        public required string SENDER_NAME { get; set; }
        public required string SENDER_PASSWORD { get; set; }
    }

}
