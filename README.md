# Reapify

I originally developed Reapify as a creator campaign management system for my own business. It helps coordinate participants, review content performance, and record the payments associated with each campaign.

This portfolio version includes a demo database with fictional records. I created the Reapify name, logos, and brand identity.

## Features

- Manage clients, creators, and business sectors.
- Create campaigns with budgets, schedules, platforms, and payment rates.
- Enroll creators and review their metrics and supporting evidence.
- Calculate payments for complete blocks of 1,000 views within the campaign budget.
- Record deposits and payouts linked to campaigns or metrics.
- Import metrics from Excel and generate PDF reports.
- Prepare and send campaign messages through Discord webhooks using your own configuration.

## Screenshots

### Workspace overview

A central workspace for clients, creators, campaigns, and payments.

![Workspace overview](docs/screenshots/01-overview.png)

### Campaign management

Campaign budgets, schedules, and status in one place.

![Campaign management](docs/screenshots/02-campaigns.png)

### Performance and payouts

Review content metrics and calculate payments within the campaign budget.

![Campaign metrics](docs/screenshots/03-campaign-metrics.png)

### Transaction history

Deposits and creator payouts linked to their related records.

![Transaction history](docs/screenshots/04-transactions.png)

### Campaign report

A generated PDF summarizing campaign performance and financial results.

![Campaign report](docs/screenshots/05-campaign-report.png)

## Technology and structure

ASP.NET Core MVC on .NET 8, Razor, Bootstrap, SQL Server, and Dapper. ClosedXML handles Excel imports, while QuestPDF generates reports. Edit view models use explicit mappings.

| Directory | Contents |
|---|---|
| `Advertisements/Controllers` | MVC actions and workflow coordination |
| `Advertisements/Models` | Entities, view models, and PDF composition |
| `Advertisements/Services` | Dapper repositories, payment calculations, and messaging |
| `Advertisements/Views` | Razor views and shared components |
| `Advertisements/wwwroot` | Styles, scripts, and static assets |
| `database` | SQL schema, stored procedures, and fictional seed data |

The solution retains the technical name `Advertisements`; the product is Reapify.

## Run the demo

### Requirements

- .NET 8 SDK or a compatible SDK, and the ASP.NET Core 8 runtime.
- SQL Server or SQL Server Express LocalDB, with SSMS to run the database scripts.
- Visual Studio with ASP.NET support, or the .NET CLI.
- Access to NuGet for package restore and to the CDNs used by Feather and Simple-DataTables.

### Database setup

1. Run [database/01-schema.sql](database/01-schema.sql) in SSMS. It creates `Reapify_Portfolio_Demo`, its 11 tables, and 53 stored procedures. Run it once on an instance where that database does not already exist.
2. Run [database/02-demo-data.sql](database/02-demo-data.sql). It requires empty tables and rejects an existing dataset to prevent duplicate records.

These scripts create a separate database. You do not need the original business database or my credentials.

### Connection settings

The checked-in development configuration uses `(localdb)\MSSQLLocalDB` with Windows integrated authentication. If you created the demo database on that instance, no private settings file is required.

For a different SQL Server instance, copy the public configuration example from the repository root:

```powershell
Copy-Item Advertisements/appsettings.Local.example.json Advertisements/appsettings.Local.json
```

If you already have a local settings file, edit it instead of overwriting it. Set the server to your own SQL Server instance and keep the database name `Reapify_Portfolio_Demo`.

For example, a local SQL Server Express instance using Windows authentication can use:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=Reapify_Portfolio_Demo;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

If your server requires SQL authentication, configure your own database user and password in the local file instead of using integrated authentication. No shared credentials are needed.

`appsettings.Local.example.json` is public and contains no credentials. `appsettings.Local.json` is excluded by `.gitignore` and is loaded only in the Development environment. Environment variables such as `ConnectionStrings__DefaultConnection` can override these settings. Do not commit your local file or database backups.

### Start the application

From the repository root:

```powershell
dotnet restore Advertisements.sln
cd Advertisements
dotnet run --launch-profile https
```

Open the URL printed in the console. Alternatively, open `Advertisements.sln` in Visual Studio and run the `https` profile.

## Suggested walkthrough

1. Open **Campaigns** and find the **Cafe Nube - DEMO** campaign.
2. Use **View Enrollments** to see its participants and **View Metrics** to review their results.
3. Select **Calculate costs** and inspect the calculated amounts.
4. Record a payout for an accepted metric through **Create Transaction**.
5. Manually mark that metric as **Settled** and recalculate: its amounts are preserved.
6. Generate a report through **Export PDF Report**.

The demo's video, screenshot, form, and webhook URLs use `example.invalid`. They are placeholders, not working services. Testing Discord delivery requires your own webhook and sends an external message.

## Payment rules

The workflow is **review → accept → calculate → record payment → mark Settled**. Recording a transaction does not automatically change a metric's status.

- Only complete blocks count: 999 views = 0 blocks; 1,000 and 1,500 views = 1 block.
- Accepted metrics are processed by descending view count, with ascending ID as a tie-breaker.
- When an amount exceeds the remaining budget, it is capped at that balance, following the original business rule.
- `Settled` metrics retain their historical amounts. Their client cost is deducted before allocating the remaining budget and remains included in campaign totals.
- Recalculation is rejected if the settled client cost exceeds the budget.
- Amounts are rounded to two decimal places. Metrics and campaign totals are saved within a single SQL transaction.

### Initial demo dataset

Budget: **BOB 500**. Rate: **BOB 10 per block**. Commission: **20%**.

| Fictional creator | Views | Status | Client amount | Creator amount |
|---|---:|---|---:|---:|
| Alba Demo | 999 | Accepted | 0 | 0 |
| Bruno Demo | 1,000 | Accepted | 10 | 8 |
| Celia Demo | 1,500 | Accepted | 10 | 8 |
| Dario Demo | 3,400 | Settled | 30 | 24 |
| Elena Demo | 2,800 | Pending | 0 | 0 |
| Fabio Demo | 7,200 | Rejected | 0 | 0 |

The seed script creates one client, six creators, one campaign, six enrollments, six metrics, and two transactions. Initial totals are **BOB 50 for the client and BOB 40 for creators**; BOB 24 is recorded as paid and BOB 16 remains pending. The client deposit is BOB 500. These figures describe a fresh dataset before any demo changes.

## Scope and limitations

- Intended for local demonstration; authentication and roles are not implemented yet.
- `Settled` is assigned manually, without enforcing that a sufficient payment transaction exists.
- Some forms allow editing amounts and relationships. Further validation and antiforgery protection across all workflows remain to be implemented.
- Excel export is not implemented; metric import and PDF reports are available.
- Automated tests are not included.

## Ownership and licenses

The interface uses Bootstrap and custom styles.

See [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md) for library and font notices, and the [NuGet inventory](licenses/NUGET-INVENTORY.md) for direct and transitive dependencies. Each dependency retains its own terms. I do not grant a general license for the Reapify source code; the brand and logos belong to me.


