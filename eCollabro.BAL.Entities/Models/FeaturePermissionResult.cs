using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.Models
{
    public  class FeaturePermissionResult
    {
        public int FeatureId { get; set; }
        public int ContentPermissionId { get; set; }
        public string ContentPermissionName { get; set; }
        public string ContentPermissionDescription { get; set; }
        public bool IsAssigned { get; set; }
        
    }
}
