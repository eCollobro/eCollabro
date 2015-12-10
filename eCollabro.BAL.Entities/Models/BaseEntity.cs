using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.Models
{
    public class BaseEntity
    {
        public  virtual bool IsDeleted { get; set; }
        public  virtual bool IsActive { get; set; }
        public  virtual bool IsAnomynousAccess { get; set; }
        public  virtual string ApprovalStatus { get; set; }

    }
}
