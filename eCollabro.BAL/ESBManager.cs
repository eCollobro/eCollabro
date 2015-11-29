using eCollabro.BAL.Entities.ADPModel;
using eCollabro.BAL.Entities.CustomModels;
using Intersoft.ESB.DAL;
using Intersoft.ESB.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCollabro.BAL
{
    public class ESBManager : BaseManager
    {
        ComponentsRepository _componentsRepository = null;

        public ESBManager()
        {
            _componentsRepository = new ComponentsRepository();
        }
        public List<AppEntity> GetAllApps(int serviceId)
        {
           List<AppEntity>  apps=_componentsRepository.GetESBApps(serviceId);
           return apps;
        }
    }
}
