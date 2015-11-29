-- exec uspGetSiteFeatures 0

-- Create date: <4-Feb-2014>
-- Description:	<Get All Features for SiteType>
-- =============================================
-- exec uspGetSiteFeatures 3
CREATE procedure [dbo].[uspGetSiteFeatures]
 @SiteId INT
As
Begin
DECLARE @True bit, @False bit 
SELECT @True = 1, @False = 0

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
	  SiteFeature.CreatedOn,
	  SiteFeature.CreatedById,
	  CASE WHEN 
		isNull(SiteFeature.FeatureId,0) =0 
	  THEN 
		 @False 
	  ELSE  
		  @True END 
	  AS IsAssigned 
FROM  
       Module 
	   INNER JOIN Feature ON Module.ModuleId = Feature.ModuleId 
	   LEFT JOIN SiteFeature ON Feature.FeatureId = SiteFeature.FeatureId AND SiteFeature.SiteId=@SiteId
	 
End




