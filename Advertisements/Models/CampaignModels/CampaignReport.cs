using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Advertisements.Models.CampaignModels
{
    public class CampaignReport : IDocument
    {
        private readonly CampaignReportData Data;

        public CampaignReport(CampaignReportData data)
        {
            Data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontFamily("Poppins").FontSize(12));

                page.Header().Row(row =>
                {
                    row.RelativeItem().AlignBottom().Text(text =>
                    {
                        text.Span("Reporte de Campaña Reapify").Bold().FontSize(18).FontColor(Colors.Black);
                        text.Span("! - ").Bold().FontSize(18).FontColor("#3DBF00");
                        text.Span($"{Data.ClientName}").Bold().FontSize(18).FontColor("#3DBF00");
                    });

                    row.ConstantItem(60)
                       .Height(60)
                       .AlignRight()
                       .AlignMiddle()
                       .Image("wwwroot\\assets\\img\\logo\\logo.png");
                       
                });

                page.Content()
                    .PaddingVertical(10)
                    .Column(col =>
                    {
                        col.Item()
                            .PaddingBottom(10)
                            .Text($"{Data.ReportDate:dd 'de' MMMM 'de' yyyy}")
                            .FontColor(Colors.Grey.Darken2);

                        col.Item()
                            .Element(e => e.AlignCenter().Background("#F3F3F3"))
                            .Padding(10)
                            .Text($"La campaña de publicidad para {Data.ClientName} obtuvo un alcance de {Data.TotalViews:N0} vistas " +
                                $"con un engagement del {Data.EngagementRate:P2} en la plataforma de {Data.Platform}.")
                            .FontColor(Colors.Grey.Darken2);

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                        col.Item()
                            .PaddingBottom(10)
                            .Text("Información General")
                            .Bold();

                        col.Item()
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });                                

                                table.Cell()
                                    .PaddingBottom(10)
                                    .Text(text =>
                                    {
                                        text.Span("Fecha Inicio: ").FontColor("#3DBF00");
                                        text.Span($"{Data.StartDate:dd-MM-yyyy}").FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingBottom(10)
                                    .Text(text =>
                                    {
                                        text.Span("Fecha Fin: ").FontColor("#3DBF00");
                                        text.Span($"{Data.EndDate:dd-MM-yyyy}").FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .Text(text =>
                                    {
                                        text.Span("Plataforma: ").FontColor("#3DBF00");
                                        text.Span($"{Data.Platform}").FontColor(Colors.Black);
                                    });

                                table.Cell()                                    
                                    .Text(text =>
                                    {
                                        text.Span("Creadores: ").FontColor("#3DBF00");
                                        text.Span($"{Data.NumCreators} creadores").FontColor(Colors.Black);
                                    });

                            });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                        col.Item()
                            .PaddingBottom(10)
                            .Text("Métricas Principales")
                            .Bold();
                        
                        col.Item()                            
                            .Element(e => e.Background("#F3F3F3"))
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Alcance").FontColor("#3DBF00");
                                        text.Line($"{Data.TotalViews:N0} vistas").Bold().FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Engagement").FontColor("#3DBF00");
                                        text.Line($"{Data.TotalEngagement:N0} interacciones").Bold().FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Tasa de engagement").FontColor("#3DBF00");
                                        text.Line($"{Data.EngagementRate:P2}").Bold().FontColor(Colors.Black);
                                    });
                            });

                        col.Item().PaddingVertical(10)
                            .LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                        col.Item()
                            .PaddingBottom(10)
                            .Text("Resumen Financiero")
                            .Bold();

                        col.Item()
                            .PaddingBottom(10)
                            .Text(text =>
                            {
                                text.Span("Tarifa de costo: ").FontColor("#3DBF00");
                                text.Span($"Bs. {Data.ClientPaymentRate:N2} por cada 1000 vistas").FontColor(Colors.Black);
                            });

                        col.Item()
                            .Element(e => e.Background("#F3F3F3"))
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Presupuesto Inicial: ").FontColor("#3DBF00");
                                        text.Line($"Bs. {Data.ClientBudget:N2}").Bold().FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Optimización por vistas sobrantes: ").FontColor("#3DBF00");
                                        text.Line($"Bs. {Data.ExtraViewsOptimization:N2}").Bold().FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Costo estimado: ").FontColor("#3DBF00");
                                        text.Line($"Bs. {Data.ExpectedClientPayment:N2}").Bold().FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Optimización por Reapify: ").FontColor("#3DBF00");
                                        text.Line($"Bs. {Data.AgencyOptimization:N2}").Bold().FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Costo Final: ").FontColor("#3DBF00");
                                        text.Line($"Bs. {Data.ClientPayment:N2}").Bold().FontColor(Colors.Black);
                                    });

                                table.Cell()
                                    .PaddingTop(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Line("Optimización total: ").FontColor("#3DBF00");
                                        text.Line($"Bs. {Data.TotalOptimization:N2}").Bold().FontColor(Colors.Black);
                                    });

                            });

                    });

                // Pie de página
                page.Footer()
                    .AlignCenter()
                    .Text("Reporte generado por Reapify Network")
                    .FontSize(10)
                    .FontColor(Colors.Grey.Darken2);
            });

            container.Page(page =>
            {
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontFamily("Poppins").FontSize(12));

                page.Header().AlignRight().Row(row =>
                {

                    row.ConstantItem(60)
                       .Height(60)
                       .AlignRight()
                       .AlignMiddle()
                       .Image("wwwroot\\assets\\img\\logo\\logo.png");

                });

                page.Content()
                    .PaddingVertical(10)
                    .Column(col =>
                    {

                        col.Item()                            
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell()
                                    .Element(e => e.Background("#F3F3F3"))
                                    .PaddingVertical(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Span("Creador de Contenido").FontColor("#3DBF00");
                                    });

                                table.Cell()
                                    .Element(e => e.Background("#F3F3F3"))
                                    .PaddingVertical(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Span("Link del Video").FontColor("#3DBF00");
                                    });

                                table.Cell()
                                    .Element(e => e.Background("#F3F3F3"))
                                    .PaddingVertical(15)
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Span("Alcance").FontColor("#3DBF00");
                                    });

                                table.Cell()
                                    .Element(e => e.Background("#F3F3F3"))
                                    .PaddingVertical(15) 
                                    .Text(text =>
                                    {
                                        text.AlignCenter();
                                        text.Span("Engagement").FontColor("#3DBF00");
                                    });

                                foreach (var metric in Data.ReportMetrics)
                                {
                                    table.Cell()
                                        .Element(e => e.Background("#F3F3F3"))
                                        .PaddingBottom(15) 
                                        .Text(text =>
                                        {
                                            text.AlignCenter();
                                            text.Span($"{metric.CreatorName}").FontColor(Colors.Black);
                                        });

                                    table.Cell()
                                        .Element(e => e.Background("#F3F3F3"))
                                        .PaddingBottom(15) 
                                        .Text(text =>
                                        {
                                            text.AlignCenter();
                                            text.Hyperlink("Link", $"{metric.VideoLink}").Underline().FontColor("#3DBF00");
                                        });

                                    table.Cell()
                                        .Element(e => e.Background("#F3F3F3"))
                                        .PaddingBottom(15) 
                                        .Text(text =>
                                        {
                                            text.AlignCenter();
                                            text.Span($"{metric.NumViews:N0} vistas").FontColor(Colors.Black);
                                        });

                                    table.Cell()
                                        .Element(e => e.Background("#F3F3F3"))
                                        .PaddingBottom(15) 
                                        .Text(text =>
                                        {
                                            text.AlignCenter();
                                            text.Span($"{metric.Engagement:N0} interacc.").FontColor(Colors.Black);
                                        });
                                }
                            });

                    });

                page.Footer()
                    .AlignCenter()
                    .Text("Reporte generado por Reapify Network")
                    .FontSize(10)
                    .FontColor(Colors.Grey.Darken2);
            });

        }

    }
}

