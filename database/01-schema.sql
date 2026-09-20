-- Ejecutar una sola vez en SSMS. Crea una base independiente; no modifica RN_agency.
USE [master]
GO
CREATE DATABASE [Reapify_Portfolio_Demo]
GO
USE [Reapify_Portfolio_Demo]
GO
/****** Object:  Table [dbo].[CampaignCreators]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignCreators](
	[CampaignCreatorId] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CampaignId] [int] NOT NULL,
	[CreatorId] [int] NOT NULL,
 CONSTRAINT [PK_CampaignCreators] PRIMARY KEY CLUSTERED 
(
	[CampaignCreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Campaigns]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campaigns](
	[ProductId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[Scale] [int] NOT NULL,
	[Currency] [int] NULL,
	[ComissionRate] [decimal](18, 2) NOT NULL,
	[ClientBudget] [decimal](18, 2) NOT NULL,
	[CreatorBudget] [decimal](18, 2) NOT NULL,
	[Platform] [int] NOT NULL,
	[Goal] [nvarchar](255) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[ClientPaymentRate] [decimal](18, 2) NOT NULL,
	[CreatorPayoutRate] [decimal](18, 2) NOT NULL,
	[Details] [nvarchar](255) NOT NULL,
	[GoogleFormsLink] [nvarchar](max) NOT NULL,
	[ClientPayment] [decimal](18, 2) NOT NULL,
	[CreatorPayout] [decimal](18, 2) NOT NULL,
	[WebhookURL] [nvarchar](max) NOT NULL,
	[SalesGrowth] [decimal](18, 2) NULL,
	[Stage] [int] NOT NULL,
	[CampaignStatus] [int] NOT NULL,
 CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[EntityId] [int] NOT NULL,
	[IndustryId] [int] NOT NULL,
	[ReferenceLink] [nvarchar](max) NOT NULL,
	[ContactName] [nvarchar](255) NULL,
	[ContactEmail] [nvarchar](255) NULL,
	[ContactPhone] [nvarchar](50) NULL,
	[NIT] [nvarchar](50) NULL,
	[TaxAddress] [nvarchar](255) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Currency] [nvarchar](255) NULL,
	[Region] [nvarchar](255) NULL,
	[Subregion] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Creators]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Creators](
	[EntityId] [int] NOT NULL,
	[ContentType] [int] NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[CI] [nvarchar](20) NULL,
	[RegistrationPhone] [nvarchar](50) NOT NULL,
	[RegistrationEmail] [nvarchar](255) NOT NULL,
	[FollowerRange] [int] NOT NULL,
	[CreatorStatus] [int] NOT NULL,
 CONSTRAINT [PK_Creator] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entities]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entities](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[EntityType] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK__clientes__677F38F5C53EA630] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Industries]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Industries](
	[IndustryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Industries] PRIMARY KEY CLUSTERED 
(
	[IndustryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metrics]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metrics](
	[ProductId] [int] NOT NULL,
	[SubmissionTimestamp] [datetime] NOT NULL,
	[CampaignCreatorId] [int] NOT NULL,
	[ScreenshotLink] [nvarchar](max) NOT NULL,
	[VideoLink] [nvarchar](max) NOT NULL,
	[NumViews] [bigint] NOT NULL,
	[NumLikes] [bigint] NOT NULL,
	[NumComments] [bigint] NOT NULL,
	[NumShares] [bigint] NOT NULL,
	[NumSaves] [bigint] NOT NULL,
	[VideoDuration] [int] NOT NULL,
	[QRCodeLink] [nvarchar](max) NOT NULL,
	[CalculatedClientPayment] [decimal](18, 2) NOT NULL,
	[CalculatedCreatorPayout] [decimal](18, 2) NOT NULL,
	[MetricStatus] [int] NOT NULL,
 CONSTRAINT [PK_Metrics] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ProductType] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[States]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Type] [nvarchar](191) NULL,
	[TimeZone] [nvarchar](255) NULL,
 CONSTRAINT [PK__states__3213E83FA446456E] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Type] [int] NOT NULL,
	[Direction] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CampaignCreators]  WITH CHECK ADD  CONSTRAINT [FK_CampaignCreators_Campaigns1] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaigns] ([ProductId])
GO
ALTER TABLE [dbo].[CampaignCreators] CHECK CONSTRAINT [FK_CampaignCreators_Campaigns1]
GO
ALTER TABLE [dbo].[CampaignCreators]  WITH CHECK ADD  CONSTRAINT [FK_CampaignCreators_Creators] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Creators] ([EntityId])
GO
ALTER TABLE [dbo].[CampaignCreators] CHECK CONSTRAINT [FK_CampaignCreators_Creators]
GO
ALTER TABLE [dbo].[Campaigns]  WITH CHECK ADD  CONSTRAINT [FK_Campaign_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([EntityId])
GO
ALTER TABLE [dbo].[Campaigns] CHECK CONSTRAINT [FK_Campaign_Client]
GO
ALTER TABLE [dbo].[Campaigns]  WITH CHECK ADD  CONSTRAINT [FK_Campaigns_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Campaigns] CHECK CONSTRAINT [FK_Campaigns_Products]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Client_Entity] FOREIGN KEY([EntityId])
REFERENCES [dbo].[Entities] ([EntityId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Client_Entity]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Client_Industries] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industries] ([IndustryId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Client_Industries]
GO
ALTER TABLE [dbo].[Creators]  WITH CHECK ADD  CONSTRAINT [FK_Entity_Creator] FOREIGN KEY([EntityId])
REFERENCES [dbo].[Entities] ([EntityId])
GO
ALTER TABLE [dbo].[Creators] CHECK CONSTRAINT [FK_Entity_Creator]
GO
ALTER TABLE [dbo].[Entities]  WITH CHECK ADD  CONSTRAINT [FK_Entities_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([StateId])
GO
ALTER TABLE [dbo].[Entities] CHECK CONSTRAINT [FK_Entities_States]
GO
ALTER TABLE [dbo].[Metrics]  WITH CHECK ADD  CONSTRAINT [FK_Metrics_CampaignCreators] FOREIGN KEY([CampaignCreatorId])
REFERENCES [dbo].[CampaignCreators] ([CampaignCreatorId])
GO
ALTER TABLE [dbo].[Metrics] CHECK CONSTRAINT [FK_Metrics_CampaignCreators]
GO
ALTER TABLE [dbo].[Metrics]  WITH CHECK ADD  CONSTRAINT [FK_Metrics_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Metrics] CHECK CONSTRAINT [FK_Metrics_Products]
GO
ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_states_countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO
ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_states_countries]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Entity] FOREIGN KEY([EntityId])
REFERENCES [dbo].[Entities] ([EntityId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Entity]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Products]
GO
/****** Object:  StoredProcedure [dbo].[CreateCampaign]    Script Date: 9/18/2026 5:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateCampaign]
	-- Add the parameters for the stored procedure here

	--Product--
	@CreationDate DATETIME,
	--Campaign--
	@ClientId INT,
	@Scale INT,
	@Currency INT,
	@ComissionRate DECIMAL(18,2),
	@ClientBudget DECIMAL(18,2),
	@CreatorBudget DECIMAL(18,2),
	@Platform INT,
	@Goal NVARCHAR(255),
	@StartDate DATETIME,
	@EndDate DATETIME,
	@ClientPaymentRate DECIMAL(18,2),
	@CreatorPayoutRate DECIMAL(18,2),
	@Details NVARCHAR(255),
	@GoogleFormsLink NVARCHAR(MAX),
	@ClientPayment DECIMAL(18,2),
	@CreatorPayout DECIMAL(18,2),
	@WebhookURL NVARCHAR(MAX),
	@SalesGrowth DECIMAL(18,2),
	@Stage INT,
	@CampaignStatus INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	DECLARE @aux INT

	INSERT INTO Products(CreationDate, ProductType)
	VALUES (@CreationDate, 1);

	SET @aux = CAST(SCOPE_IDENTITY() AS INT);

	INSERT INTO Campaigns(ProductId, ClientId, Scale, Currency, ComissionRate, ClientBudget, CreatorBudget, Platform, Goal, StartDate, EndDate,
		ClientPaymentRate, CreatorPayoutRate, Details, GoogleFormsLink, ClientPayment, CreatorPayout,
		WebhookURL, SalesGrowth, Stage, CampaignStatus)
    VALUES (@aux, @ClientId, @Scale, @Currency, @ComissionRate, @ClientBudget, @CreatorBudget, @Platform, @Goal, @StartDate, @EndDate,
		@ClientPaymentRate, @CreatorPayoutRate, @Details, @GoogleFormsLink, @ClientPayment, @CreatorPayout,
		@WebhookURL, @SalesGrowth, @Stage, @CampaignStatus);

	SELECT @aux;
END
GO
/****** Object:  StoredProcedure [dbo].[CreateCampaignCreator]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateCampaignCreator] 
	-- Add the parameters for the stored procedure here
	@CreationDate DATETIME,
	@CampaignId INT,
	@CreatorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO CampaignCreators(CreationDate, CampaignId, CreatorId)
	VALUES(@CreationDate, @CampaignId, @CreatorId)

	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[CreateClient]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateClient]
	-- Add the parameters for the stored procedure here
	
	--Entity--
	@CreationDate DATETIME,
	@Name NVARCHAR(255),
	@StateId INT,
	--Client--
	@IndustryId INT,
	@ReferenceLink NVARCHAR(MAX),
	@ContactName NVARCHAR(255),
	@ContactEmail NVARCHAR(255),
	@ContactPhone NVARCHAR(50),
	@NIT NVARCHAR(50),
	@TaxAddress NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	DECLARE @aux INT

	INSERT INTO Entities(CreationDate, EntityType, Name, StateId)
	VALUES (@CreationDate, 1, @Name, @StateId);

	SET @aux = CAST(SCOPE_IDENTITY() AS INT);

	INSERT INTO Clients(EntityId, IndustryId, ReferenceLink, ContactName, ContactEmail, ContactPhone, NIT, TaxAddress)
    VALUES (@aux, @IndustryId, @ReferenceLink, @ContactName, @ContactEmail, @ContactPhone, @NIT, @TaxAddress)

	SELECT @aux;
END
GO
/****** Object:  StoredProcedure [dbo].[CreateCountry]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateCountry]
	-- Add the parameters for the stored procedure here
	
	@Name NVARCHAR(100),
	@Currency NVARCHAR(255),
	@Region NVARCHAR(255),
	@Subregion NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	INSERT INTO Countries(Name, Currency, Region, Subregion)
	VALUES(@Name, @Currency, @Region, @Subregion);	

	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[CreateCreator]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateCreator]
	-- Add the parameters for the stored procedure here
	
	--Entity--
	@CreationDate DATETIME,
	@Name NVARCHAR(255),
	@StateId INT,
	--Creator--
	@ContentType INT,
	@FirstName NVARCHAR(255),
	@LastName NVARCHAR(255),
	@CI NVARCHAR(20),
	@RegistrationPhone NVARCHAR(50),
	@RegistrationEmail NVARCHAR(255),
	@FollowerRange INT,
	@CreatorStatus INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	DECLARE @aux INT

	INSERT INTO Entities(CreationDate, EntityType, Name, StateId)
	VALUES (@CreationDate, 2, @Name, @StateId);

	SET @aux = CAST(SCOPE_IDENTITY() AS INT);

	INSERT INTO Creators(EntityId, ContentType, FirstName, LastName, CI, RegistrationPhone,
		RegistrationEmail, FollowerRange, CreatorStatus)
    VALUES (@aux, @ContentType, @FirstName, @LastName, @CI, @RegistrationPhone,
	@RegistrationEmail, @FollowerRange, @CreatorStatus)

	SELECT @aux;
END
GO
/****** Object:  StoredProcedure [dbo].[CreateIndustry]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateIndustry]
	-- Add the parameters for the stored procedure here
	
	@Name NVARCHAR(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	INSERT INTO Industries(Name)
	VALUES(@Name);	

	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[CreateMetric]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateMetric]
	-- Add the parameters for the stored procedure here

	--Product--
	@CreationDate DATETIME,
	--Campaign--
	@SubmissionTimeStamp DATETIME,
	@CampaignCreatorId INT,
	@ScreenshotLink NVARCHAR(MAX),
	@VideoLink NVARCHAR(MAX),
	@NumViews BIGINT,
	@NumLikes BIGINT,
	@NumComments BIGINT,
	@NumShares BIGINT,
	@NumSaves BIGINT,
	@VideoDuration INT,
	@QRCodeLink NVARCHAR(MAX),
	@CalculatedClientPayment DECIMAL(18,2),
	@CalculatedCreatorPayout DECIMAL(18,2)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	DECLARE @aux INT

	INSERT INTO Products(CreationDate, ProductType)
	VALUES (@CreationDate, 2);

	SET @aux = CAST(SCOPE_IDENTITY() AS INT);

	INSERT INTO Metrics(ProductId, SubmissionTimeStamp, CampaignCreatorId,
		ScreenshotLink, VideoLink, NumViews, NumLikes, NumComments, NumShares, NumSaves,
		VideoDuration, QRCodeLink, CalculatedClientPayment, CalculatedCreatorPayout, MetricStatus)
    VALUES (@aux, @SubmissionTimeStamp, @CampaignCreatorId,
		@ScreenshotLink, @VideoLink, @NumViews, @NumLikes, @NumComments, @NumShares, @NumSaves,
		@VideoDuration, @QRCodeLink, @CalculatedClientPayment, @CalculatedCreatorPayout, 1);

	SELECT @aux;
END
GO
/****** Object:  StoredProcedure [dbo].[CreateState]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateState]
	-- Add the parameters for the stored procedure here
	
	@CountryId INT,
	@Name NVARCHAR(255),
	@Type NVARCHAR(191),
	@TimeZone NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	INSERT INTO States(CountryId, Name, Type, TimeZone)
	VALUES(@CountryId, @Name, @Type, @TimeZone);	

	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO
/****** Object:  StoredProcedure [dbo].[CreateTransaction]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateTransaction]
	-- Add the parameters for the stored procedure here

	--Product--
	@CreationDate DATETIME,
	@Amount DECIMAL(18,2),
	@Type INT,
	@Direction INT,
	@EntityId INT,
	@ProductId INT,
	@Description NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	INSERT INTO Transactions(CreationDate, Amount, Type, Direction,
		EntityId, ProductId, Description)
	VALUES (@CreationDate, @Amount, @Type, @Direction,
		@EntityId, @ProductId, @Description);

	SELECT CAST(SCOPE_IDENTITY() AS INT);

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCampaignCreator]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCampaignCreator]
	-- Add the parameters for the stored procedure here
	@CampaignCreatorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM CampaignCreators
	WHERE CampaignCreatorId = @CampaignCreatorId
END
GO
/****** Object:  StoredProcedure [dbo].[ExistsMetric]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ExistsMetric]
	-- Add the parameters for the stored procedure here
	@CampaignCreatorId INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 ProductId
	FROM Metrics
	WHERE CampaignCreatorId = @CampaignCreatorId;
END
GO
/****** Object:  StoredProcedure [dbo].[ExistsTransaction]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ExistsTransaction]
	-- Add the parameters for the stored procedure here
	@EntityId INT,
	@ProductId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 TransactionId
	FROM Transactions
	WHERE EntityId = @EntityId
	AND ProductId = @ProductId;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCampaignCreators]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCampaignCreators]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CampaignCreatorId, cac.CreationDate,
		CampaignId, CreatorId, en.Name AS CreatorName,
		cr.RegistrationEmail AS CreatorEmail
	FROM CampaignCreators cac
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	ORDER BY cac.CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCampaigns]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCampaigns] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  pr.ProductId, pr.CreationDate, pr.ProductType, ClientId, en.Name AS ClientName,
		Scale, Currency, ComissionRate, ClientBudget, CreatorBudget, Platform, Goal, StartDate, EndDate, ClientPaymentRate,
		CreatorPayoutRate, Details, GoogleFormsLink, ClientPayment, CreatorPayout, WebhookURL, SalesGrowth, Stage, CampaignStatus
	FROM Products pr
	INNER JOIN Campaigns ca
	ON pr.ProductId = ca.ProductId
	INNER JOIN Entities en
	ON ca.ClientId = en.EntityId
	INNER JOIN Clients cl
	ON cl.EntityId = en.EntityId
	ORDER BY pr.CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllClients]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllClients] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT en.EntityId, CreationDate, EntityType, en.Name, en.StateId,
		st.Name AS StateName,co.CountryId, co.Name AS CountryName,
		cl.IndustryId, i.Name AS IndustryName, ReferenceLink, ContactName, ContactEmail, ContactPhone, NIT, TaxAddress
	FROM Entities en
	INNER JOIN Clients cl
	ON en.EntityId = cl.EntityId
	INNER JOIN Industries i
	ON cl.IndustryId = i.IndustryId
	INNER JOIN States st
	ON en.StateId = st.StateId
	INNER JOIN Countries co
	ON st.CountryId = co.CountryId
	ORDER BY CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCountries]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCountries]
	-- Add the parameters for the stored procedure here
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT CountryId, Name, Currency, Region, Subregion
	FROM Countries
	ORDER BY Name ASC

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCreators]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCreators] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT en.EntityId, CreationDate, EntityType, en.Name, en.StateId,
	st.Name AS StateName, co.CountryId, co.Name AS CountryName, ContentType, FirstName,
	LastName, CI, RegistrationPhone, RegistrationEmail, FollowerRange, CreatorStatus
	FROM Entities en
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	INNER JOIN States st
	ON en.StateId = st.StateId
	INNER JOIN Countries co
	ON st.CountryId = co.CountryId
	ORDER BY CreatorStatus ASC, CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllEntities]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllEntities] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT EntityId, CreationDate, EntityType, Name, StateId		
	FROM Entities	
	ORDER BY CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllIndustries]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllIndustries]
	-- Add the parameters for the stored procedure here
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT IndustryId, Name
	FROM Industries
	ORDER BY Name ASC

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllMetrics]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllMetrics] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  pr.ProductId, pr.CreationDate, pr.ProductType, SubmissionTimeStamp, me.CampaignCreatorId,
		cac.CampaignId, cac.CreatorId,
		en.Name AS CreatorName, ScreenshotLink, VideoLink, NumViews, NumLikes, NumComments, NumShares,
		NumSaves, VideoDuration, QRCodeLink, CalculatedClientPayment, CalculatedCreatorPayout, MetricStatus
	FROM Products pr
	INNER JOIN Metrics me
	ON pr.ProductId = me.ProductId
	INNER JOIN CampaignCreators cac
	ON me.CampaignCreatorId = cac.CampaignCreatorId
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON cr.EntityId = en.EntityId
	ORDER BY cac.CampaignId DESC, NumViews DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllProducts] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  ProductId, CreationDate, ProductType
	FROM Products
	ORDER BY CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllStates]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllStates]
	-- Add the parameters for the stored procedure here
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT StateId, CountryId, Name, Type, TimeZone
	FROM States
	ORDER BY CountryId ASC

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllTransactions]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllTransactions]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT TransactionId, tr.CreationDate, Amount, Type, Direction, tr.EntityId,
		en.Name AS EntityName, en.EntityType, tr.ProductId, pr.ProductType, Description
	FROM Transactions tr
	INNER JOIN Entities en
	ON tr.EntityId = en.EntityId
	INNER JOIN Products pr
	ON tr.ProductId = pr.ProductId
	ORDER BY tr.CreationDate DESC



END
GO
/****** Object:  StoredProcedure [dbo].[GetCampaignById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCampaignById] 
	-- Add the parameters for the stored procedure here
	@CampaignId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  pr.ProductId, pr.CreationDate, pr.ProductType, ClientId, en.Name AS ClientName,
	Scale, Currency, ComissionRate, ClientBudget, CreatorBudget, Platform, Goal, StartDate, EndDate, ClientPaymentRate,
	CreatorPayoutRate, Details, GoogleFormsLink, ClientPayment, CreatorPayout, WebhookURL, SalesGrowth, Stage, CampaignStatus
	FROM Products pr
	INNER JOIN Campaigns ca
	ON pr.ProductId = ca.ProductId
	INNER JOIN Entities en
	ON ca.ClientId = en.EntityId
	INNER JOIN Clients cl
	ON cl.EntityId = en.EntityId
	WHERE pr.ProductId = @CampaignId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCampaignCreatorById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCampaignCreatorById]
	-- Add the parameters for the stored procedure here
	@CampaignCreatorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CampaignCreatorId, cac.CreationDate,
		CampaignId, CreatorId, en.Name AS CreatorName,
		cr.RegistrationEmail AS CreatorEmail
	FROM CampaignCreators cac
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	WHERE CampaignCreatorId = @CampaignCreatorId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCampaignCreatorByIds]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCampaignCreatorByIds]
	-- Add the parameters for the stored procedure here
	@CampaignId INT,
	@CreatorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CampaignCreatorId, cac.CreationDate,
		CampaignId, CreatorId, en.Name AS CreatorName,
		cr.RegistrationEmail AS CreatorEmail
	FROM CampaignCreators cac
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	WHERE CampaignId = @CampaignId
	AND CreatorId = @CreatorId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCampaignCreatorsByCampaign]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCampaignCreatorsByCampaign]
	-- Add the parameters for the stored procedure here
	@CampaignId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CampaignCreatorId, cac.CreationDate,
		CampaignId, CreatorId, en.Name AS CreatorName,
		cr.RegistrationEmail AS CreatorEmail
	FROM CampaignCreators cac
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	WHERE CampaignId = @CampaignId
	ORDER BY cac.CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetClientById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetClientById] 
	-- Add the parameters for the stored procedure here
	@ClientId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT en.EntityId, CreationDate, EntityType, en.Name, en.StateId,
		st.Name AS StateName,co.CountryId, co.Name AS CountryName,
		cl.IndustryId, i.Name AS IndustryName, ReferenceLink, ContactName, ContactEmail, ContactPhone, NIT, TaxAddress
	FROM Entities en
	INNER JOIN Clients cl
	ON en.EntityId = cl.EntityId
	INNER JOIN Industries i
	ON cl.IndustryId = i.IndustryId
		INNER JOIN States st
	ON en.StateId = st.StateId
	INNER JOIN Countries co
	ON st.CountryId = co.CountryId
	WHERE en.EntityId = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCountryById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCountryById]
	-- Add the parameters for the stored procedure here
	@CountryId INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT CountryId, Name, Currency, Region, Subregion
	FROM Countries
	WHERE CountryId = @CountryId

END
GO
/****** Object:  StoredProcedure [dbo].[GetCreatorByEmail]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCreatorByEmail] 
	-- Add the parameters for the stored procedure here
	@Email NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT en.EntityId, CreationDate, EntityType, en.Name, en.StateId,
		st.Name AS StateName, co.CountryId, co.Name AS CountryName, ContentType, FirstName,
		LastName, CI, RegistrationPhone, RegistrationEmail, FollowerRange, CreatorStatus
	FROM Entities en
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	INNER JOIN States st
	ON en.StateId = st.StateId
	INNER JOIN Countries co
	ON st.CountryId = co.CountryId
	WHERE cr.RegistrationEmail = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[GetCreatorById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCreatorById] 
	-- Add the parameters for the stored procedure here
	@CreatorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT en.EntityId, CreationDate, EntityType, en.Name, en.StateId,
		st.Name AS StateName, co.CountryId, co.Name AS CountryName, ContentType, FirstName,
		LastName, CI, RegistrationPhone, RegistrationEmail, FollowerRange, CreatorStatus
	FROM Entities en
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	INNER JOIN States st
	ON en.StateId = st.StateId
	INNER JOIN Countries co
	ON st.CountryId = co.CountryId
	WHERE en.EntityId = @CreatorId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCreatorsIdsByCampaign]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCreatorsIdsByCampaign]
	-- Add the parameters for the stored procedure here
	@CampaignId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CreatorId
	FROM CampaignCreators cac
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON en.EntityId = cr.EntityId
	WHERE CampaignId = @CampaignId
	ORDER BY cac.CreationDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetEntityById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEntityById] 
	-- Add the parameters for the stored procedure here
	@EntityId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT EntityId, CreationDate, EntityType, Name, StateId		
	FROM Entities	
	WHERE EntityId = @EntityId
END
GO
/****** Object:  StoredProcedure [dbo].[GetIndustryById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetIndustryById]
	-- Add the parameters for the stored procedure here
	@IndustryId INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT IndustryId, Name
	FROM Industries
	WHERE IndustryId = @IndustryId

END
GO
/****** Object:  StoredProcedure [dbo].[GetMetricByCampaignCreator]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMetricByCampaignCreator] 
	-- Add the parameters for the stored procedure here
	@CampaignCreatorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 pr.ProductId, pr.CreationDate, pr.ProductType, SubmissionTimeStamp, me.CampaignCreatorId,
		cac.CampaignId, cac.CreatorId,
		en.Name AS CreatorName, ScreenshotLink, VideoLink, NumViews, NumLikes, NumComments, NumShares,
		NumSaves, VideoDuration, QRCodeLink, CalculatedClientPayment, CalculatedCreatorPayout, MetricStatus
	FROM Products pr
	INNER JOIN Metrics me
	ON pr.ProductId = me.ProductId
	INNER JOIN CampaignCreators cac
	ON me.CampaignCreatorId = cac.CampaignCreatorId
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON cr.EntityId = en.EntityId
	WHERE me.CampaignCreatorId = @CampaignCreatorId
END
GO
/****** Object:  StoredProcedure [dbo].[GetMetricById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMetricById] 
	-- Add the parameters for the stored procedure here
	@MetricId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  pr.ProductId, pr.CreationDate, pr.ProductType, SubmissionTimeStamp, me.CampaignCreatorId,
		cac.CampaignId, cac.CreatorId,
		en.Name AS CreatorName, ScreenshotLink, VideoLink, NumViews, NumLikes, NumComments, NumShares,
		NumSaves, VideoDuration, QRCodeLink, CalculatedClientPayment, CalculatedCreatorPayout, MetricStatus
	FROM Products pr
	INNER JOIN Metrics me
	ON pr.ProductId = me.ProductId
	INNER JOIN CampaignCreators cac
	ON me.CampaignCreatorId = cac.CampaignCreatorId
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON cr.EntityId = en.EntityId
	WHERE pr.ProductId = @MetricId
END
GO
/****** Object:  StoredProcedure [dbo].[GetMetricsByCampaign]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMetricsByCampaign] 
	-- Add the parameters for the stored procedure here
	@CampaignId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  pr.ProductId, pr.CreationDate, pr.ProductType, SubmissionTimeStamp, me.CampaignCreatorId,
		cac.CampaignId, cac.CreatorId,
		en.Name AS CreatorName, ScreenshotLink, VideoLink, NumViews, NumLikes, NumComments, NumShares,
		NumSaves, VideoDuration, QRCodeLink, CalculatedClientPayment, CalculatedCreatorPayout, MetricStatus
	FROM Products pr
	INNER JOIN Metrics me
	ON pr.ProductId = me.ProductId
	INNER JOIN CampaignCreators cac
	ON me.CampaignCreatorId = cac.CampaignCreatorId
	INNER JOIN Entities en
	ON cac.CreatorId = en.EntityId
	INNER JOIN Creators cr
	ON cr.EntityId = en.EntityId
	WHERE cac.CampaignId = @CampaignId
	ORDER BY NumViews DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProductById] 
	-- Add the parameters for the stored procedure here
	@ProductId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  ProductId, CreationDate, ProductType
	FROM Products
	WHERE ProductId = @ProductId
END
GO
/****** Object:  StoredProcedure [dbo].[GetStateById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStateById]
	-- Add the parameters for the stored procedure here
	@StateId INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT StateId, CountryId, Name, Type, TimeZone
	FROM States
	WHERE StateId = @StateId

END
GO
/****** Object:  StoredProcedure [dbo].[GetStatesByCountry]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetStatesByCountry]
	-- Add the parameters for the stored procedure here
	@CountryId INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT StateId, CountryId, Name, Type, TimeZone
	FROM States
	WHERE CountryId = @CountryId
	ORDER BY CountryId ASC

END
GO
/****** Object:  StoredProcedure [dbo].[GetTransactionById]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTransactionById]
	-- Add the parameters for the stored procedure here
	@TransactionId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT TransactionId, tr.CreationDate, Amount, Type, Direction, tr.EntityId,
		en.Name AS EntityName, en.EntityType, tr.ProductId, pr.ProductType, Description
	FROM Transactions tr
	INNER JOIN Entities en
	ON tr.EntityId = en.EntityId
	INNER JOIN Products pr
	ON tr.ProductId = pr.ProductId
	WHERE TransactionId = @TransactionId



END
GO
/****** Object:  StoredProcedure [dbo].[GetTransactionByIds]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTransactionByIds]
	-- Add the parameters for the stored procedure here
	@EntityId INT,
	@ProductId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT TransactionId, tr.CreationDate, Amount, Type, Direction, tr.EntityId,
		en.Name AS EntityName, en.EntityType, tr.ProductId, pr.ProductType, Description
	FROM Transactions tr
	INNER JOIN Entities en
	ON tr.EntityId = en.EntityId
	INNER JOIN Products pr
	ON tr.ProductId = pr.ProductId
	WHERE tr.EntityId = @EntityId
	AND tr.ProductId = @ProductId



END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCampaign]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCampaign]

	-- Add the parameters for the stored procedure here

	--Product--
	@ProductId INT,
	@CreationDate DATETIME,
	--Campaign--
	@ClientId INT,
	@Scale INT,
	@Currency INT,
	@ComissionRate DECIMAL(18,2),
	@ClientBudget DECIMAL(18,2),
	@CreatorBudget DECIMAL(18,2),
	@Platform INT,
	@Goal NVARCHAR(255),
	@StartDate DATETIME,
	@EndDate DATETIME,
	@ClientPaymentRate DECIMAL(18,2),
	@CreatorPayoutRate DECIMAL(18,2),
	@Details NVARCHAR(255),
	@GoogleFormsLink NVARCHAR(MAX),
	@WebhookURL NVARCHAR(MAX),
	@SalesGrowth DECIMAL(18,2),
	@Stage INT,
	@CampaignStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Products
    SET CreationDate = @CreationDate
    WHERE ProductId = @ProductId;
	
	UPDATE Campaigns
    SET ClientId = @ClientId, Scale = @Scale, Currency = @Currency, ComissionRate = @ComissionRate, ClientBudget = @ClientBudget,
		Platform = @Platform, Goal = @Goal, CreatorBudget = @CreatorBudget,	StartDate = @StartDate,
		EndDate = @EndDate, ClientPaymentRate = @ClientPaymentRate, CreatorPayoutRate = @CreatorPayoutRate,
		Details = @Details, GoogleFormsLink = @GoogleFormsLink,
		WebHookURL = @WebhookURL, SalesGrowth = @SalesGrowth, Stage = @Stage, CampaignStatus = @CampaignStatus
    WHERE ProductId = @ProductId;
	
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCampaignCreator]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCampaignCreator] 
	-- Add the parameters for the stored procedure here
	@CampaignCreatorId INT,
	@CreationDate DATETIME,
	@CampaignId INT,
	@CreatorId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE CampaignCreators
	SET CreationDate = @CreationDate, CampaignId = @CampaignId,
		CreatorId = @CreatorId
	WHERE CampaignCreatorId = @CampaignCreatorId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCampaignPayments]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCampaignPayments]

	-- Add the parameters for the stored procedure here

	--Product--
	@CampaignId INT,	
	@ClientPayment DECIMAL(18,2),
	@CreatorPayout DECIMAL(18,2)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	UPDATE Campaigns
    SET ClientPayment = @ClientPayment, CreatorPayout = @CreatorPayout
    WHERE ProductId = @CampaignId;	
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateClient]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateClient]
	-- Add the parameters for the stored procedure here
	
	--Entity--
	@EntityId INT,
	@CreationDate DATETIME,
	@Name NVARCHAR(255),
	@StateId INT,
	--Client--
	@IndustryId INT,
	@ReferenceLink NVARCHAR(MAX),
	@ContactName NVARCHAR(255),
	@ContactEmail NVARCHAR(255),
	@ContactPhone NVARCHAR(50),
	@NIT NVARCHAR(50),
	@TaxAddress NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	UPDATE Entities
    SET CreationDate = @CreationDate,
		Name = @Name, StateId = @StateId
    WHERE EntityId = @EntityId;

    -- Actualiza tabla hija Client
    UPDATE Clients
    SET IndustryId = @IndustryId, ReferenceLink = @ReferenceLink,
		ContactName = @ContactName, ContactEmail = @ContactEmail,
		ContactPhone = @ContactPhone, NIT = @NIT, TaxAddress = @TaxAddress
    WHERE EntityId = @EntityId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCountry]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCountry]
	-- Add the parameters for the stored procedure here
	@CountryId INT,
	@Name NVARCHAR(100),
	@Currency NVARCHAR(255),
	@Region NVARCHAR(255),
	@Subregion NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	UPDATE Countries
	SET Name = @Name, Currency = @Currency, Region = @Region,
		Subregion = @Subregion
	WHERE CountryId = @CountryId


END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCreator]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCreator]
	-- Add the parameters for the stored procedure here
	
	--Entity--
	@EntityId INT,
	@CreationDate DATETIME,
	@Name NVARCHAR(255),
	@StateId INT,
	--Creator--
	@ContentType INT,
	@FirstName NVARCHAR(255),
	@LastName NVARCHAR(255),
	@CI NVARCHAR(20),
	@RegistrationPhone NVARCHAR(50),
	@RegistrationEmail NVARCHAR(255),
	@FollowerRange INT,
	@CreatorStatus INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	UPDATE Entities
    SET CreationDate = @CreationDate,
		Name = @Name, StateId = @StateId
    WHERE EntityId = @EntityId;

    -- Actualiza tabla hija Client
    UPDATE Creators
    SET	ContentType = @ContentType, FirstName = @FirstName, LastName = @LastName, CI = @CI, RegistrationPhone = @RegistrationPhone,
		RegistrationEmail = @RegistrationEmail, FollowerRange = @FollowerRange, CreatorStatus = @CreatorStatus
    WHERE EntityId = @EntityId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateIndustry]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateIndustry]
	-- Add the parameters for the stored procedure here
	@IndustryId INT,
	@Name NVARCHAR(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	UPDATE Industries
	SET Name = @Name
	WHERE IndustryId = @IndustryId


END
GO
/****** Object:  StoredProcedure [dbo].[UpdateMetric]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateMetric]

	-- Add the parameters for the stored procedure here

	--Product--
	@ProductId INT,
	@CreationDate DATETIME,
	--Campaign--
	@SubmissionTimeStamp DATETIME,
	@CampaignCreatorId INT,
	@ScreenshotLink NVARCHAR(MAX),
	@VideoLink NVARCHAR(MAX),
	@NumViews BIGINT,
	@NumLikes BIGINT,
	@NumComments BIGINT,
	@NumShares BIGINT,
	@NumSaves BIGINT,
	@VideoDuration INT,
	@QRCodeLink NVARCHAR(MAX),
	@CalculatedClientPayment DECIMAL(18,2),
	@CalculatedCreatorPayout DECIMAL(18,2),
	@MetricStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Products
    SET CreationDate = @CreationDate
    WHERE ProductId = @ProductId;

    -- Actualiza tabla hija Metric
    UPDATE Metrics
    SET SubmissionTimeStamp = @SubmissionTimeStamp, CampaignCreatorId = @CampaignCreatorId,
		ScreenshotLink = @ScreenshotLink, VideoLink = @VideoLink,
		NumViews = @NumViews, NumLikes = @NumLikes, NumComments = @NumComments, NumShares = @NumShares,
		NumSaves = @NumSaves,  VideoDuration = @VideoDuration, QRCodeLink = @QRCodeLink,
		CalculatedClientPayment = @CalculatedClientPayment,
		CalculatedCreatorPayout = @CalculatedCreatorPayout, MetricStatus = @MetricStatus
    WHERE ProductId = @ProductId;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateMetricPayments]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateMetricPayments]

	-- Add the parameters for the stored procedure here

	--Product--
	@MetricId INT,	
	@CalculatedClientPayment DECIMAL(18,2),
	@CalculatedCreatorPayout DECIMAL(18,2)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	UPDATE Metrics
    SET CalculatedClientPayment = @CalculatedClientPayment, CalculatedCreatorPayout = @CalculatedCreatorPayout
    WHERE ProductId = @MetricId;	
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateState]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateState]
	-- Add the parameters for the stored procedure here
	@StateId INT,
	@CountryId INT,
	@Name NVARCHAR(255),
	@Type NVARCHAR(191),
	@TimeZone NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	UPDATE States
	SET CountryId = @CountryId, Name = @Name,
		Type = @Type, TimeZone = @TimeZone
	WHERE StateId = @StateId


END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTransaction]    Script Date: 9/18/2026 5:11:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTransaction]
	-- Add the parameters for the stored procedure here

	--Product--
	@TransactionId INT,
	@CreationDate DATETIME,
	@Amount DECIMAL(18,2),
	@Type INT,
	@Direction INT,
	@EntityId INT,
	@ProductId INT,
	@Description NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	UPDATE Transactions
	SET CreationDate = @CreationDate, Amount = @Amount, Type = @Type, Direction = @Direction,
		EntityId = @EntityId, ProductId = @ProductId, Description = @Description
	WHERE TransactionId = @TransactionId

END
GO


