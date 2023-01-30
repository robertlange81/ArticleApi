CREATE TABLE [dbo].[Article]
(
	[Id] BIGINT NOT NULL Identity, 
    [ArticleId] NVARCHAR(100) NOT NULL, 
    [Color] NCHAR(6) NULL,
    [isBulky] BIT NOT NULL,
    [CreatedAt] DATETIME NOT NULL DEFAULT getDate(), 
    [UpdatedAt] DATETIME NULL, 
    CONSTRAINT [PK_Article_ArticleId] PRIMARY KEY CLUSTERED 
    (
	    [ArticleId] ASC
    )
)
