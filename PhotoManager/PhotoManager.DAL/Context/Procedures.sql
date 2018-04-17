CREATE PROCEDURE dbo.Search
                @keyword varchar(255)
            AS
            BEGIN
                SELECT p.id FROM photos p JOIN cameraSettings c ON c.Id = p.CameraSettingsId WHERE
@keyword IS NULL OR	p.Name LIKE '%' +@keyWord+ '%'
END