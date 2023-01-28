CREATE PROCEDURE [dbo].[spArticle_GetByArticleID]
	@ArticleId NCHAR(100)
AS
begin
	select
	Id,
	ArticleId,
	Color,
	isBulky
	from dbo.[Article]
	where ArticleId = @ArticleId
end
