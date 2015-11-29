CREATE PROCEDURE [dbo].[uspGetNavigations] 

(
	@SiteId	INT
)
AS  
BEGIN  
SELECT        Navigation.NavigationId, Navigation.NavigationCode, Navigation.NavigationText,Navigation.AdditionalHtml, Navigation.SiteId, Navigation.NavigationParentId, Navigation.Link,
			  Navigation.NavigationTypeId,Navigation.DisplayOrder, Navigation.IsAnomynousAccess, Navigation.IsActive, Navigation.CreatedById, Navigation.CreatedOn,  
			  Navigation.ModifiedOn,Navigation.ModifiedById,Navigation.ContentPageId, Navigation.FeatureId
			  ,lkpNavigationType.NavigationTypeCode, lkpNavigationType.NavigationType
			  ,[Site].SiteName
			  ,Feature.FeatureCode, Feature.FeatureName
			  ,Navigation_1.NavigationCode AS NavigationParentCode, Navigation_1.NavigationText AS NavigationParentText
			  ,UserMembership.UserName ModifiedByName, UserMembership_1.UserName  CreatedByName

FROM            Navigation INNER JOIN

                         lkpNavigationType ON Navigation.NavigationTypeId = lkpNavigationType.NavigationTypeId INNER JOIN

                         [Site] ON Navigation.SiteId = [Site].SiteId LEFT OUTER JOIN

                         [ContentPage] ON Navigation.ContentPageId = [ContentPage].ContentPageId LEFT OUTER JOIN

                         Feature ON Navigation.FeatureId = Feature.FeatureId LEFT OUTER JOIN
						 
						 Navigation AS Navigation_1 ON Navigation.NavigationParentId = Navigation_1.NavigationId LEFT JOIN 

						 UserMembership ON Navigation.ModifiedById = UserMembership.UserId LEFT JOIN 

						 UserMembership AS UserMembership_1 ON Navigation.CreatedById = UserMembership_1.UserId where Navigation.IsDeleted=0

						 AND [Site].SiteId=@SiteId

END

