using System.Linq;
using eCollabro.DataMapper;
using eCollabro.Service.DataContracts;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Service.Core;
using eCollabro.Client.Models.Core;


namespace eCollabro.Client
{
    internal static class SecurityClientTranslate
    {

        public static ModuleModel Convert(ModuleDC module)
        {
            var map = Mapper.Resolve<ModuleDC, ModuleModel>(MapResolveOptions.UsePrivateSetter);
            ModuleModel featureModel = Mapper.Map<ModuleDC, ModuleModel>(module);
            if (module.Features != null)
            {
                module.Features.ToList().ForEach(feature => featureModel.ModuleFeatures.Add(Convert(feature)));
            }
            return featureModel;
        }

        public static ModuleDC Convert(ModuleModel module)
        {
            var map = Mapper.Resolve<ModuleModel, ModuleDC>(MapResolveOptions.UsePrivateSetter);
            ModuleDC moduleDC = Mapper.Map<ModuleModel, ModuleDC>(module);
            //module.Features.ToList().ForEach(feature => featureModel.ModuleFeatures.Add(Convert(feature)));
            return moduleDC;
        }
        
        public static ModuleFeatureModel Convert(ModuleFeatureDC feature)
        {
            var map = Mapper.Resolve<ModuleFeatureDC, ModuleFeatureModel>(MapResolveOptions.UsePrivateSetter);
            ModuleFeatureModel moduleFeatureModel = Mapper.Map<ModuleFeatureDC, ModuleFeatureModel>(feature);
            return moduleFeatureModel;
        }


        public static UserModel Convert(UserDetailDC userDeatilsDC)
        {
            var map = Mapper.Resolve<UserDetailDC, UserModel>(MapResolveOptions.UsePrivateSetter);
            UserModel userDetailsModel = Mapper.Map<UserDetailDC, UserModel>(userDeatilsDC);
            return userDetailsModel;
        }

        public static UserDetailDC Convert(UserModel userDeatilsModel)
        {
            var map = Mapper.Resolve<UserModel, UserDetailDC>(MapResolveOptions.UsePrivateSetter);
            UserDetailDC userDetailDC = Mapper.Map<UserModel, UserDetailDC>(userDeatilsModel);
            return userDetailDC;
        }

      
        public static UserContextModel Convert(UserContextDC userContextDC)
        {
            UserContextModel userDetailsModel = new UserContextModel()
            {
                UserName = userContextDC.UserName,
                SiteId = userContextDC.SiteId,
                Language = userContextDC.Language
            };
            return userDetailsModel;
        }

        public static UserContextDC Convert(UserContextModel userContextModel)
        {
            UserContextDC userContextDC = new UserContextDC()
            {
                UserName = userContextModel.UserName,
                SiteId = userContextModel.SiteId,
                Language = userContextModel.Language
            };
            return userContextDC;
        }

  
        public static NavigationTypeModel Convert(NavigationTypeDC lkpNavigationTypeDC)
        {
            NavigationTypeModel navigationTypeModel = new NavigationTypeModel()
            {
                NavigationTypeId = lkpNavigationTypeDC.NavigationTypeId,
                NavigationTypeCode = lkpNavigationTypeDC.NavigationTypeCode,
                NavigationType = lkpNavigationTypeDC.NavigationType,
                IsActive = lkpNavigationTypeDC.IsActive,
                CreatedById = lkpNavigationTypeDC.CreatedById,
                CreatedOn = lkpNavigationTypeDC.CreatedOn,
                ModifiedById = lkpNavigationTypeDC.ModifiedById,
                ModifiedOn = lkpNavigationTypeDC.ModifiedOn,
            };
            return navigationTypeModel;
        }

  
    }
}
