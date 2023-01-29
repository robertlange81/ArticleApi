CREATE PROCEDURE [dbo].[spArticle_GetByTitleAndLastModified]
	@Title NVARCHAR(100),
	@lastModifiedFrom DATETIME,
	@lastModifiedTo DATETIME
AS
begin
  select
      art.[Id]
      ,art.[ArticleId]
      ,[Color]
      ,[isBulky]
      ,[CreatedAt]
      ,[UpdatedAt]
      ,trans.[CountryCode]
      ,trans.[ArticleId]
      ,trans.[Title]
	  ,trans.[Description]
	from dbo.[Article] art LEFT JOIN [dbo].[ArticleTranslation] trans ON (art.ArticleId = trans.ArticleId)
	where art.[ArticleId] IN (select ArticleId from [dbo].[ArticleTranslation] where Title like '%'+@Title+'%')
    and ISNULL([UpdatedAt], [CreatedAt]) BetWeen ISNULL(@lastModifiedFrom, '1900-01-01T00:00:00.000Z') AND ISNULL(@lastModifiedTo, '9999-12-31T00:00:00.000Z')
    end