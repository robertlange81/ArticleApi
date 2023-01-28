﻿CREATE PROCEDURE [dbo].[spArticle_Insert]
	@ArticleId NCHAR(100),
	@Color NCHAR(6),
	@IsBulky bit
AS
begin
	insert into dbo.[Article](ArticleId, Color,	isBulky)
	values(@ArticleId, @Color, @IsBulky);
end
