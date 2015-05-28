USE CatalogueSystemDB

ALTER TABLE dbo.Images ADD UpdatedAt datetime2 NOT NULL
ALTER TABLE dbo.Images ADD MimeType nvarchar(100) NOT NULL