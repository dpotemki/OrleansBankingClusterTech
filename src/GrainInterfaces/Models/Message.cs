using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces.Models
{
    [Serializable]
    public class Message
    {
        public int FromUserId { get; set; }
        public DateTime Timestamp { get; set; }
        public required string Text { get; set; }
    }
}
