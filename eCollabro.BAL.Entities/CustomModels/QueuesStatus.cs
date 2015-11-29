using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.CustomModels
{
    public class QueuesStatus
    {
        [Key]
        public int RowId { get; set; }
        public int NewQueues {get;set; }

        public int FailedQueues { get; set; }

    }
}
