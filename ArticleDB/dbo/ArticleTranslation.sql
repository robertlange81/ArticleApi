CREATE TABLE [dbo].[ArticleTranslation]
(
	[Id] INT NOT NULL Identity,
    [ArticleId] NCHAR(100) NOT NULL,
    [CountryCode] NCHAR(2) NOT NULL,
    [Title] NCHAR(100) NOT NULL, 
    [Description] NCHAR(500) NOT NULL,
    CONSTRAINT [FK_ArticleTranslation_Article_ArticleId] FOREIGN KEY ([ArticleId])
    REFERENCES Article([ArticleId]) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT [PK_ArticleTranslation_ArticleId_CountryCode] PRIMARY KEY CLUSTERED
    (
        [ArticleId],
	    [CountryCode]
    )
)
