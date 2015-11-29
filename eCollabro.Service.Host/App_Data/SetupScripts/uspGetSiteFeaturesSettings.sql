-- exec uspGetSiteFeatures 0

-- Create date: <4-Feb-2014>
-- Description:	<Get All Features for SiteType>
-- =============================================
-- exec [uspGetSiteFeaturesSettings] 1
CREATE  procedure [dbo].[uspGetSiteFeaturesSettings]
 @SiteId INT
As
Begin
DECLARE @True bit, @False bit 
SELECT @True = 1, @False = 0

SELECT    
    SiteFeature.FeatureId,
	SiteFeature.SiteId, 
	FeatureContentSetting.ContentSettingId, 
	ContentSetting.ContentSettingName, 
	ContentSetting.ContentSettingDescription, 
	CASE WHEN isNull(SiteContentSetting.ContentSettingId,0) =0 	THEN @False ELSE @True END AS IsAssigned 
FROM            
    SiteFeature 
	INNER JOIN Feature ON SiteFeature.FeatureId = Feature.FeatureId
	INNER JOIN FeatureContentSetting ON Feature.FeatureId = FeatureContentSetting.FeatureId 
	INNER JOIN ContentSetting ON FeatureContentSetting.ContentSettingId = ContentSetting.ContentSettingId 
	LEFT OUTER JOIN SiteContentSetting ON Feature.FeatureId = SiteContentSetting.FeatureId AND ContentSetting.ContentSettingId = SiteContentSetting.ContentSettingId
WHERE 
    SiteFeature.SiteId=@SiteId 	 
End




