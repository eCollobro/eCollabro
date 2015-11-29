// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.DataMapper;
using System.Linq;
using System.Collections.Generic;
using eCollabro.Service.DataContracts.Core;
using eCollabro.Service.Core;
using eCollabro.BAL.Entities.Models;
using eCollabro.Service.DataMembers.Core;

#endregion 

namespace eCollabro.Service
{
    /// <summary>
    /// Translate
    /// </summary>
    public static class Translate
    {
        #region Role Feature
         
        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="features"></param>
        /// <param name="modules"></param>
        public static void Convert(List<ModuleFeature_Result> features, List<ModuleDC> modules)
        {
            if (modules != null)
            {
                foreach (ModuleFeature_Result feature in features)
                {
                    ModuleDC module = modules.Where(ee => ee.ModuleId.Equals(feature.ModuleId)).FirstOrDefault();
                    if (module == null)
                    {
                        module = new ModuleDC
                        {
                            ModuleId = feature.ModuleId,
                            ModuleName = feature.ModuleName,
                            ModuleDescription = feature.ModuleDescription,
                            ModuleCode = feature.ModuleCode

                        };
                        modules.Add(module);
                    }
                    module.Features.Add(new ModuleFeatureDC
                    {
                        FeatureId = feature.FeatureId,
                        FeatureCode = feature.FeatureCode,
                        FeatureName = feature.FeatureName,
                        IsSelected = feature.IsAssigned.Value
                    });
                }
            }
        }
        #endregion

        /// <summary>
        /// ModuleDC
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static ModuleDC Convert(Module module)
        {
            var map = Mapper.Resolve<Module, ModuleDC>(MapResolveOptions.UsePrivateSetter);
            ModuleDC moduleDC = Mapper.Map<Module, ModuleDC>(module);
            module.Features.ToList().ForEach(feature =>
                {
                    if (moduleDC.Features == null)
                    {
                        moduleDC.Features = new List<ModuleFeatureDC>();
                    }
                    moduleDC.Features.Add(Convert(feature));
                }
                );
            return moduleDC;
        }


        public static ModuleFeatureDC Convert(Feature feature)
        {
            var map = Mapper.Resolve<Feature, ModuleFeatureDC>(MapResolveOptions.UsePrivateSetter);
            ModuleFeatureDC moduleFeatureDC = Mapper.Map<Feature, ModuleFeatureDC>(feature);
            return moduleFeatureDC;
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="features"></param>
        /// <param name="modules"></param>
        public static void Convert(List<FeatureResult> features, List<ModuleDC> modules)
        {
            if (modules != null)
            {
                foreach (FeatureResult feature in features)
                {
                    ModuleDC module = modules.Where(ee => ee.ModuleId.Equals(feature.ModuleId)).FirstOrDefault();
                    if (module == null)
                    {
                        module = new ModuleDC
                        {
                            ModuleId = feature.ModuleId,
                            ModuleName = feature.ModuleName,
                            ModuleDescription = feature.ModuleDescription,
                            ModuleCode = feature.ModuleCode

                        };
                        modules.Add(module);
                    }
                    ModuleFeatureDC moduleFeatureDC = new ModuleFeatureDC
                    {
                        FeatureId = feature.FeatureId,
                        FeatureCode = feature.FeatureCode,
                        FeatureName = feature.FeatureName,
                        IsSelected = feature.IsAssigned.Value
                    };
                    module.Features.Add(moduleFeatureDC);
                    moduleFeatureDC.RoleFeaturePermissions = new List<FeaturePermissionDC>();
                    if (feature.RoleFeaturePermissions != null)
                    {
                        foreach (FeaturePermissionResult featurePermissionResult in feature.RoleFeaturePermissions)
                        {
                            moduleFeatureDC.RoleFeaturePermissions.Add(Mapper.Map<FeaturePermissionResult, FeaturePermissionDC>(featurePermissionResult));
                        }
                    }
                    moduleFeatureDC.SiteContentSettings=new List<SiteContentSettingDC>();
                    if (feature.SiteContentSettings != null)
                    {
                        foreach (SiteContentSettingResult siteContentSetting in feature.SiteContentSettings)
                        {
                            moduleFeatureDC.SiteContentSettings.Add(Mapper.Map<SiteContentSettingResult, SiteContentSettingDC>(siteContentSetting));
                        }
                    }
                }
            }
        }



    }
}
