-- Datos exclusivamente ficticios. Ejecutar tras 01-schema.sql en una base demo vacia.
USE [Reapify_Portfolio_Demo];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME() <> N'Reapify_Portfolio_Demo'
    THROW 50001, 'Selecciona la base Reapify_Portfolio_Demo.', 1;
IF EXISTS (SELECT 1 FROM dbo.Entities) OR EXISTS (SELECT 1 FROM dbo.Products)
 OR EXISTS (SELECT 1 FROM dbo.Countries) OR EXISTS (SELECT 1 FROM dbo.Industries)
 OR EXISTS (SELECT 1 FROM dbo.States) OR EXISTS (SELECT 1 FROM dbo.CampaignCreators)
 OR EXISTS (SELECT 1 FROM dbo.Transactions)
    THROW 50002, 'La base debe estar vacia. No se modificaron datos existentes.', 1;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @country int, @state int, @industry int, @client int, @campaign int;
    INSERT dbo.Countries (Name, Currency, Region, Subregion)
    VALUES (N'Bolivia', N'BOB', N'Americas', N'South America');
    SET @country = CONVERT(int, SCOPE_IDENTITY());
    INSERT dbo.States (CountryId, Name, Type, TimeZone)
    VALUES (@country, N'La Paz', N'Departamento', N'America/La_Paz');
    SET @state = CONVERT(int, SCOPE_IDENTITY());
    INSERT dbo.Industries (Name) VALUES (N'Gastronomia (demo)');
    SET @industry = CONVERT(int, SCOPE_IDENTITY());
    INSERT dbo.Entities (CreationDate, EntityType, Name, StateId)
    VALUES ('20260901', 1, N'Cafe Nube - DEMO', @state);
    SET @client = CONVERT(int, SCOPE_IDENTITY());
    INSERT dbo.Clients (EntityId, IndustryId, ReferenceLink, ContactName, ContactEmail,
        ContactPhone, NIT, TaxAddress)
    VALUES (@client, @industry, N'https://example.invalid/cafe-nube', N'Contacto ficticio',
        N'cafe.nube@example.invalid', N'DEMO-NO-LLAMAR', N'DEMO-NIT', N'Direccion ficticia');
    INSERT dbo.Products (CreationDate, ProductType) VALUES ('20260901', 1);
    SET @campaign = CONVERT(int, SCOPE_IDENTITY());
    -- Tarifa: Bs 10 por bloque completo de 1000 vistas. Comision: 20%.
    -- Total calculado: Bs 50 cliente / Bs 40 creadores; pagados: Bs 24 al creador.
    INSERT dbo.Campaigns (ProductId, ClientId, Scale, Currency, ComissionRate,
        ClientBudget, CreatorBudget, Platform, Goal, StartDate, EndDate,
        ClientPaymentRate, CreatorPayoutRate, Details, GoogleFormsLink,
        ClientPayment, CreatorPayout, WebhookURL, SalesGrowth, Stage, CampaignStatus)
    VALUES (@campaign, @client, 1, 1, 0.20, 500, 400, 3,
        N'Dar a conocer una cafeteria ficticia', '20260901', '20260930', 10, 8,
        N'DEMO: pagos por bloques de mil; incluye revision, rechazo y pago manual.',
        N'https://example.invalid/formulario', 50, 40,
        N'https://example.invalid/webhook-disabled', 0, 4, 2);
    INSERT dbo.Transactions (CreationDate, Amount, Type, Direction, EntityId, ProductId, Description)
    VALUES ('20260901', 500, 1, 1, @client, @campaign, N'DEMO: deposito del cliente');

    DECLARE @cases TABLE (Id int PRIMARY KEY, Name nvarchar(100), Views int,
        Status int, ClientAmount decimal(18,2), CreatorAmount decimal(18,2));
    INSERT @cases VALUES
        (1, N'Alba Demo', 999, 2, 0, 0),
        (2, N'Bruno Demo', 1000, 2, 10, 8),
        (3, N'Celia Demo', 1500, 2, 10, 8),
        (4, N'Dario Demo', 3400, 4, 30, 24),
        (5, N'Elena Demo', 2800, 1, 0, 0),
        (6, N'Fabio Demo', 7200, 3, 0, 0);
    DECLARE @i int = 1, @creator int, @enrollment int, @metric int,
        @name nvarchar(100), @views int, @status int,
        @clientAmount decimal(18,2), @creatorAmount decimal(18,2);
    WHILE @i <= 6
    BEGIN
        SELECT @name = Name, @views = Views, @status = Status,
            @clientAmount = ClientAmount, @creatorAmount = CreatorAmount
        FROM @cases WHERE Id = @i;
        INSERT dbo.Entities (CreationDate, EntityType, Name, StateId)
        VALUES ('20260902', 2, @name, @state);
        SET @creator = CONVERT(int, SCOPE_IDENTITY());
        INSERT dbo.Creators (EntityId, ContentType, FirstName, LastName, CI,
            RegistrationPhone, RegistrationEmail, FollowerRange, CreatorStatus)
        VALUES (@creator, 8, LEFT(@name, CHARINDEX(' ', @name)-1), N'Demo',
            CONCAT(N'DEMO-', @i), N'DEMO-NO-LLAMAR',
            CONCAT(N'creador', @i, N'@example.invalid'), 2, 2);
        INSERT dbo.CampaignCreators (CreationDate, CampaignId, CreatorId)
        VALUES ('20260903', @campaign, @creator);
        SET @enrollment = CONVERT(int, SCOPE_IDENTITY());
        INSERT dbo.Products (CreationDate, ProductType) VALUES ('20260910', 2);
        SET @metric = CONVERT(int, SCOPE_IDENTITY());
        INSERT dbo.Metrics (ProductId, SubmissionTimestamp, CampaignCreatorId,
            ScreenshotLink, VideoLink, NumViews, NumLikes, NumComments, NumShares,
            NumSaves, VideoDuration, QRCodeLink, CalculatedClientPayment,
            CalculatedCreatorPayout, MetricStatus)
        VALUES (@metric, '20260910', @enrollment,
            CONCAT(N'https://example.invalid/captura/', @i),
            CONCAT(N'https://example.invalid/video/', @i),
            @views, @views / 20, @views / 100, @views / 200, @views / 150, 30,
            CONCAT(N'https://example.invalid/qr/', @i), @clientAmount, @creatorAmount, @status);
        IF @status = 4
            INSERT dbo.Transactions (CreationDate, Amount, Type, Direction, EntityId, ProductId, Description)
            VALUES ('20260912', @creatorAmount, 4, 2, @creator, @metric,
                N'DEMO: pago completo; metrica marcada Settled manualmente');
        SET @i += 1;
    END;
    -- Comprobaciones: se revierten todas las inserciones si los datos no son coherentes.
    IF (SELECT COUNT(*) FROM dbo.Metrics) <> 6
        THROW 50003, 'Cantidad inesperada de metricas.', 1;
    IF EXISTS (
        SELECT 1 FROM dbo.Metrics m
        WHERE m.MetricStatus IN (2,4) AND
        (m.CalculatedClientPayment <> (m.NumViews / 1000) * 10
         OR m.CalculatedCreatorPayout <> (m.NumViews / 1000) * 8))
        THROW 50004, 'Los importes no respetan los bloques completos de mil.', 1;
    IF EXISTS (
        SELECT 1 FROM dbo.Metrics m
        JOIN dbo.CampaignCreators cc ON cc.CampaignCreatorId = m.CampaignCreatorId
        WHERE m.MetricStatus = 4 AND m.CalculatedCreatorPayout <>
        COALESCE((SELECT SUM(t.Amount) FROM dbo.Transactions t
          WHERE t.ProductId = m.ProductId AND t.EntityId = cc.CreatorId
            AND t.Type = 4 AND t.Direction = 2), 0))
        THROW 50005, 'El pago registrado no coincide con la metrica liquidada.', 1;
    IF (SELECT SUM(CalculatedClientPayment) FROM dbo.Metrics) <> 50
       OR (SELECT SUM(CalculatedCreatorPayout) FROM dbo.Metrics) <> 40
        THROW 50006, 'Totales inesperados.', 1;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
SELECT e.Name AS Creator, m.NumViews, m.NumViews / 1000 AS CompleteBlocks,
    m.MetricStatus, m.CalculatedClientPayment, m.CalculatedCreatorPayout
FROM dbo.Metrics m
JOIN dbo.CampaignCreators cc ON cc.CampaignCreatorId = m.CampaignCreatorId
JOIN dbo.Entities e ON e.EntityId = cc.CreatorId
ORDER BY e.Name;
