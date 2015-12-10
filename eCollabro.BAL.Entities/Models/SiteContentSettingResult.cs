using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL.Entities.Models
{
    public partial class SiteContentSettingResult
    {
        public int FeatureId { get; set; }
        public int SiteId { get; set; }
        public int ContentSettingId { get; set; }
        public string ContentSettingName { get; set; }
        public string ContentSettingDescription { get; set; }
        public bool IsAssigned { get; set; }
    }
}
