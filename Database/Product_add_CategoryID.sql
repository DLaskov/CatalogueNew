USE CatalogueSystemDB3

ALTER TABLE dbo.Products DROP COLUMN ProductYear
ALTER TABLE dbo.Products ADD ProductYear int NOT NULL