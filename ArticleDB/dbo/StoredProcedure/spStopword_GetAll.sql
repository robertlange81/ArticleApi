CREATE PROCEDURE [dbo].[spStopword_GetAll]
AS
begin
	select [Term] from dbo.[ArticleStopword]
end
