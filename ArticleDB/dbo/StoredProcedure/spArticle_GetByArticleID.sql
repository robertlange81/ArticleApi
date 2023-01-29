CREATE PROCEDURE [dbo].[spArticle_GetByArticleID]
	@ArticleId NCHAR(100)
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
	from dbo.[Article] art LEFT JOIN .[dbo].[ArticleTranslation] trans ON (art.ArticleId = trans.ArticleId)
	where art.ArticleId = @ArticleId
end