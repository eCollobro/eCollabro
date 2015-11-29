-- EXEC [dbo].[uspGetUserNavigations] 2,1
CREATE proc [dbo].[uspGetUserNavigations] (
@UserId as INT,
@SiteId as int = 0
)
As
BEGIN 
   
  SELECT       
 
 Navigation.NavigationId,
 Navigation.NavigationCode,
 Navigation.NavigationText,
 Navigation.AdditionalHtml,
  Navigation.SiteId ,
 Navigation.NavigationParentId,
 Navigation_1.NavigationCode AS NavigationParentCode,  Navigation_1.NavigationText AS NavigationParentText,  Navigation.NavigationTypeId,  lkpNavigationType.NavigationTypeCode,
 lkpNavigationType.NavigationType,
 Navigation.FeatureId,
 Feature.FeatureCode,
 Feature.FeatureName,
 case WHEN isnull(Navigation.Link,'')='' then 
	Feature.Link
 else
   Navigation.Link
END AS Link ,
 Navigation.ContentPageId,
 Navigation.DisplayOrder,
 Navigation.IsAnomynousAccess
 INTO #Temp 
FROM            
Navigation
INNER JOIN lkpNavigationType ON Navigation.NavigationTypeId = lkpNavigationType.NavigationTypeId
LEFT OUTER JOIN ContentPage ON Navigation.ContentPageId = ContentPage.ContentPageId LEFT OUTER JOIN Feature ON Navigation.FeatureId = Feature.FeatureId LEFT OUTER JOIN Navigation AS Navigation_1 ON Navigation.NavigationParentId = Navigation_1.NavigationId WHERE Navigation.IsDeleted =0 AND Navigation.IsActive=1 AND Navigation.SiteId=@SiteId 
	IF @UserId<>0  -- User information exists
		BEGIN 
				DECLARE @roleId INT
				DECLARE @siteAdmin INT
				DECLARE @childLevel INT
				SET @childLevel=1

				SET @siteAdmin=(Select TOP 1 isNull(UserId,0) from SiteCollectionAdmin where UserId=@UserId)

				IF @siteAdmin>0 
				  SELECT * FROM #TEMP ORDER BY DisplayOrder
                ELSE
				  BEGIN 
					SET @roleId =(Select TOP 1 RoleId from UserRole where UserId=@UserId AND SiteId =@SiteId)

					IF(@roleId=1) -- Role is Super Admin - Get All Primary Site Features/-- Role is Site Admin - Get All Site Features
						BEGIN
					      SELECT * FROM #TEMP ORDER BY DisplayOrder
						END
					ELSE -- Get all role feature
						BEGIN 

							SELECT * INTO #Temp1 FROM #Temp 
							WHERE FeatureId in 
							(
								SELECT FeatureId FROM RoleFeature WHERE RoleId in 
								(
								SELECT RoleId FROM UserRole WHERE UserId=@UserId
								) 
							)
							or FeatureId is null 
							SELECT * FROM 
							( 
							SELECT *, (SELECT COUNT(1)  FROM #TEMP1 AS InnerTemp WHERE InnerTemp.NavigationParentId=OuterTemp.NavigationId) AS ChildCount
							FROM #Temp1 AS OuterTemp 
							) AS T 
							WHERE NavigationTypeId<>1 OR (NavigationTypeId=1 AND ChildCount>0) 
							ORDER BY DisplayOrder  
						END
					END
          END
      ELSE
	    BEGIN -- Anomynous User
		   SELECT * FROM #TEMP WHERE  IsAnomynousAccess=1 ORDER BY DisplayOrder
		END
END



