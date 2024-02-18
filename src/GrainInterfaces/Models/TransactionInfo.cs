using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces.Models
{
    [Serializable]

    public class TransactionInfo
    {
        public Guid TransactionId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsCompleted { get; set; }
    }

}
