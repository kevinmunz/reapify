using Advertisements.Models.CampaignModels;
using Advertisements.Models.MetricModels;
using Advertisements.Models.ClientModels;
using Advertisements.Models.CreatorModels;
using Advertisements.Models.TransactionModels;

namespace Advertisements.Services;

// Explicit copies for edit forms. Dropdown options are populated by each controller.
public static class EditViewModels
{
    public static CreateCampaignVM From(Campaign source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new CreateCampaignVM
        {
            ProductId = source.ProductId,
            CreationDate = source.CreationDate,
            ProductType = source.ProductType,
            ClientId = source.ClientId,
            Scale = source.Scale,
            Currency = source.Currency,
            ComissionRate = source.ComissionRate,
            ClientBudget = source.ClientBudget,
            CreatorBudget = source.CreatorBudget,
            Platform = source.Platform,
            Goal = source.Goal,
            StartDate = source.StartDate,
            EndDate = source.EndDate,
            ClientPaymentRate = source.ClientPaymentRate,
            CreatorPayoutRate = source.CreatorPayoutRate,
            Details = source.Details,
            GoogleFormsLink = source.GoogleFormsLink,
            ClientPayment = source.ClientPayment,
            CreatorPayout = source.CreatorPayout,
            WebhookURL = source.WebhookURL,
            SalesGrowth = source.SalesGrowth,
            Stage = source.Stage,
            CampaignStatus = source.CampaignStatus,
            ClientName = source.ClientName,
        };
    }

    public static CreateMetricVM From(Metric source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new CreateMetricVM
        {
            ProductId = source.ProductId,
            CreationDate = source.CreationDate,
            ProductType = source.ProductType,
            SubmissionTimestamp = source.SubmissionTimestamp,
            CampaignCreatorId = source.CampaignCreatorId,
            ScreenshotLink = source.ScreenshotLink,
            VideoLink = source.VideoLink,
            NumViews = source.NumViews,
            NumLikes = source.NumLikes,
            NumComments = source.NumComments,
            NumShares = source.NumShares,
            NumSaves = source.NumSaves,
            VideoDuration = source.VideoDuration,
            QRCodeLink = source.QRCodeLink,
            CalculatedClientPayment = source.CalculatedClientPayment,
            CalculatedCreatorPayout = source.CalculatedCreatorPayout,
            MetricStatus = source.MetricStatus,
            CampaignId = source.CampaignId,
            CreatorId = source.CreatorId,
            CreatorName = source.CreatorName,
        };
    }

    public static CreateClientVM From(Client source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new CreateClientVM
        {
            EntityId = source.EntityId,
            CreationDate = source.CreationDate,
            EntityType = source.EntityType,
            Name = source.Name,
            StateId = source.StateId,
            StateName = source.StateName,
            CountryId = source.CountryId,
            CountryName = source.CountryName,
            IndustryId = source.IndustryId,
            ReferenceLink = source.ReferenceLink,
            ContactName = source.ContactName,
            ContactEmail = source.ContactEmail,
            ContactPhone = source.ContactPhone,
            NIT = source.NIT,
            TaxAddress = source.TaxAddress,
            IndustryName = source.IndustryName,
        };
    }

    public static CreateCreatorVM From(Creator source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new CreateCreatorVM
        {
            EntityId = source.EntityId,
            CreationDate = source.CreationDate,
            EntityType = source.EntityType,
            Name = source.Name,
            StateId = source.StateId,
            StateName = source.StateName,
            CountryId = source.CountryId,
            CountryName = source.CountryName,
            ContentType = source.ContentType,
            FirstName = source.FirstName,
            LastName = source.LastName,
            CI = source.CI,
            RegistrationPhone = source.RegistrationPhone,
            RegistrationEmail = source.RegistrationEmail,
            FollowerRange = source.FollowerRange,
            CreatorStatus = source.CreatorStatus,
        };
    }

    public static CreateTransactionVM From(Transaction source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new CreateTransactionVM
        {
            TransactionId = source.TransactionId,
            CreationDate = source.CreationDate,
            Amount = source.Amount,
            Type = source.Type,
            Direction = source.Direction,
            EntityId = source.EntityId,
            ProductId = source.ProductId,
            Description = source.Description,
            EntityName = source.EntityName,
            EntityType = source.EntityType,
            ProductType = source.ProductType,
        };
    }

}
