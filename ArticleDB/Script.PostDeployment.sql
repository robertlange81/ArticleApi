/*
Vorlage für ein Skript nach der Bereitstellung							
--------------------------------------------------------------------------------------
 Diese Datei enthält SQL-Anweisungen, die an das Buildskript angefügt werden.		
 Schließen Sie mit der SQLCMD-Syntax eine Datei in das Skript nach der Bereitstellung ein.			
 Beispiel:   :r .\myfile.sql								
 Verwenden Sie die SQLCMD-Syntax, um auf eine Variable im Skript nach der Bereitstellung zu verweisen.		
 Beispiel:   :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
if not exists (select 1 from dbo.Article)
begin
    insert into dbo.Article (
        [ArticleId], 
        [Color],
        [isBulky]
    ) values
    (
        '123',
        'FF0000',
        0
    ),
    (
        '345',
        'FFFF00',
        0
    ),
    (
        '567x',
        '0000FF',
        1
    );
end

if not exists (select 1 from dbo.ArticleTranslation)
begin
    insert into dbo.ArticleTranslation (
        [ArticleId], 
        [CountryCode],
        [Title],
        [Description]
    ) values
    (
        '123',
        'DE',
        'Blumentopf',
        'Beschreibung Blumentopf'
    ),
    (
        '123',
        'IT',
        'italienisch Blumentopf',
        'Beschreibung italienisch Blumentopf'
    ),
    (
        '123',
        'FR',
        'franz. Blumentopf',
        'Beschreibung franz. Blumentopf'
    ),
    (    
        '123',
        'CH',
        'schweizer Blumentopf',
        'Beschreibung schweizer Blumentopf'
    ),
    (    
        '345',
        'DE',
        'deutsche Taschenlampe',
        'Beschreibung deutsche Blumentopf'
    ),
    (    
        '567x',
        'DE',
        'blauer Sessel deluxe',
        'Beschreibung für deutschen Sessel'
    ),
    (    
        '567x',
        'FR',
        'fauteuil bleu',
        'Description du fauteuil français'
    );
end

if not exists (select 1 from dbo.ArticleStopword)
begin
    insert into dbo.ArticleStopword (
        [Term]
    ) values
    (
        'Stop'
    ),
    (
        'Sex'
    ),
    (
        'Drugs'
    ),
    (
        'Rock & Roll'
    ),
    (
        ','
    ),
    (
        'Delete'
    ),
    (
        'Drop'
    );
end