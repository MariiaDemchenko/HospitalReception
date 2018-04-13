IF OBJECT_ID ( 'dbo.Search', 'P' ) IS NOT NULL   
    DROP PROCEDURE dbo.Search;  
GO  
CREATE PROCEDURE dbo.Search 
 @keyword varchar(255)
AS   
DECLARE 
@keyWordString nvarchar(255) = '%'+@keyWord+'%'
SELECT * FROM photos p JOIN cameraSettings c ON c.Id = p.CameraSettingsId WHERE
@keyword IS NULL OR	p.Name LIKE @keyWordString
GO  