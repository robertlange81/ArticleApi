CREATE PROCEDURE [dbo].[spTranslation_Insert]
	@ArticleId NVARCHAR(100),
	@CountryCode NCHAR(2),
	@Title NVARCHAR(100),
	@Description NVARCHAR(500)
AS
begin
	insert into dbo.[ArticleTranslation](ArticleId, CountryCode, Title, [Description])
	values(@ArticleId, @CountryCode, @Title, @Description);
end
