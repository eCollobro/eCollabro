-- exec uspGetRoleFeatures 0

-- Create date: <4-Feb-2014>
-- Description:	<Get All Features for role>
-- =============================================
-- exec uspGetRoleFeatures 3
CREATE procedure [dbo].[uspGetRoleFeatures]
 @RoleId INT
As
Begin

DECLARE @True bit, @False bit 
SELECT @True = 1, @False = 0

declare @SiteId INT 
SET @SiteId = ( SELECT SiteId from [Role] Where RoleId=@RoleId)

SELECT        
      Module.ModuleId, 
	  Module.ModuleCode, 
	  Module.ModuleName,
	  Module.ModuleDescription, 
	  Feature.FeatureId, 
	  Feature.FeatureCode, 
	  Feature.FeatureName, 
	  Feature.IsNavigationLink, 
	  Feature.Link, 
	  --Feature.IsSiteAssignable, 
	  SiteFeature.CreatedOn,
	  SiteFeature.CreatedById,
	  CASE WHEN 
		isNull(RoleFeature.FeatureId,0) =0 
	  THEN 
		 @False 
	  ELSE  
		  @True END 
	  AS IsAssigned 
FROM  
       Module 
	   INNER JOIN Feature ON Module.ModuleId = Feature.ModuleId 
	   INNER JOIN SiteFeature ON Feature.FeatureId = SiteFeature.FeatureId AND SiteFeature.SiteId=@SiteId
	   LEFT OUTER JOIN RoleFeature ON RoleFeature.FeatureId = Feature.FeatureId AND RoleFeature.RoleId =@RoleId
End


