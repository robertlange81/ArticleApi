CREATE PROCEDURE [dbo].[spArticle_GetAll]
AS
begin
	select
	Id,
	ArticleId,
	Color,
	isBulky
	from dbo.[Article]
end
