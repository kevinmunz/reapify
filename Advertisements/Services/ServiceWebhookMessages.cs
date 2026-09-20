using Advertisements.Models.CampaignModels;
using Advertisements.Models.ClientModels;
using Advertisements.Models.DiscordModels;
using Advertisements.Models.MetricModels;
using System.Text;

namespace Advertisements.Services
{
    public interface IServiceWebhookMessages
    {
        Task<HttpResponseMessage> SendCampaignEndMessage(string webhookURL, string imageURL, Campaign campaign, IEnumerable<Metric> metrics);
        Task<HttpResponseMessage> SendCampaignStartMessage(string webhookURL, string imageURL, Campaign campaign, Client client);
        Task<HttpResponseMessage> SendCCEndMessage(string webhookURL, string imageURL, Campaign campaign);
        Task<HttpResponseMessage> SendCCStartMessage(string webhookURL, string imageURL, Campaign campaign);
        Task<HttpResponseMessage> SendMessage(string webhookURL, string message);
    }

    public class ServiceWebhookMessages : IServiceWebhookMessages
    {

        public async Task<HttpResponseMessage> SendMessage(string webhookURL, string message)
        {
            using var client = new HttpClient();
            var content = new StringContent(message, Encoding.UTF8, "application/json");

            return await client.PostAsync(webhookURL, content);
        }

        public async Task<HttpResponseMessage> SendCampaignStartMessage(string webhookURL, string imageURL, Campaign campaign, Client client)
        {
            var durationDays = (campaign.EndDate - campaign.StartDate).Days;

            var payload = new DiscordWebhookPayload
            {
                content = $"🎉 ¡La campaña Reapify para **{campaign.ClientName}** ha comenzado! 🎉",
                embeds = new List<DiscordEmbed>
                {
                    new DiscordEmbed
                    {
                        title = $"Campaña Reapify | {campaign.ClientName}",
                        description = "¡Es momento de mostrar tus habilidades como **Creador de Contenido** y ganar por cada vista!\nAquí tienes los detalles:",
                        color = 4046592,
                        image = new DiscordImage { url = imageURL },
                        footer = new DiscordFooter { text = "Recuerda leer las reglas de campaña antes de participar." },
                        fields = new List<DiscordField>
                        {
                            new DiscordField
                            {
                                name = $"ℹ️ Acerca de {campaign.ClientName}",
                                value = $"[Click aquí para saber más sobre {campaign.ClientName}.]({client.ReferenceLink})"
                            },
                            new DiscordField { name = "🎯 Objetivo", value = campaign.Goal },
                            new DiscordField { name = "💬 Plataforma", value = campaign.Platform.ToString() },
                            new DiscordField
                            {
                                name = "📅 Fecha Inicio de Creación de Contenido",
                                value = campaign.StartDate.ToString("dd 'de' MMMM 'de' yyyy")
                            },
                            new DiscordField
                            {
                                name = "🏁 Fecha Fin de Creación de Contenido",
                                value = campaign.EndDate.ToString("dd 'de' MMMM 'de' yyyy")
                            },
                            new DiscordField
                            {
                                name = "🕒 Duración de Creación de Contenido",
                                value = $"{durationDays} días"
                            },
                            new DiscordField
                            {
                                name = "💵 Tarifa de pago",
                                value = $"{campaign.CreatorPayoutRate:N2} Bs. por cada 1000 vistas"
                            },
                            new DiscordField
                            {
                                name = "💰 Monto total disponible",
                                value = $"{campaign.CreatorBudget} Bs."
                            },
                            new DiscordField
                            {
                                name = "📝 Descripción de la campaña",
                                value = campaign.Details
                            }
                        }
                    }
                }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(payload);

            using var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(webhookURL, content);
        }

        public async Task<HttpResponseMessage> SendCCStartMessage(string webhookURL, string imageURL, Campaign campaign)
        {
            var message = $@"
            {{
                ""content"": "":rocket: ¡La Creación de Contenido para **{campaign.ClientName}** ha comenzado! :rocket:"",
                ""embeds"": [
                    {{
                        ""title"": ""Inicio de Creación de Contenido | {campaign.ClientName}"",
                        ""image"": {{""url"": ""{imageURL}""}},
                        ""description"": ""¡Es hora de Crear Contenido para **{campaign.ClientName}** 🎬\n\nTodos los creadores que participarán en esta **Campaña Reapify** ya pueden comenzar a crear contenido y subirlo a la plataforma **{campaign.Platform}**. ¡Mucho éxito!"",
                        ""color"": 4046592,
                        ""fields"": [
                            {{
                                ""name"": "":calendar_spiral: Plazo de creación"",
                                ""value"": ""Tienen hasta la fecha **{campaign.EndDate:dd 'de' MMMM 'de' yyyy}** para crear y publicar el contenido!""
                            }}
                        ],
                        ""footer"": {{
                            ""text"": ""Recuerden revisar los lineamientos de la campaña antes de crear contenido. ¡Éxito!""
                        }}
                    }}
                ]
            }}";

            using var httpClient = new HttpClient();
            var content = new StringContent(message, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(webhookURL, content);
        }

        public async Task<HttpResponseMessage> SendCCEndMessage(string webhookURL, string imageURL, Campaign campaign)
        {
            var message = $@"
            {{
                ""content"": "":dart: ¡La campaña Reapify para **{campaign.ClientName}** está por terminar! :dart:"",
                ""embeds"": [
                    {{
                        ""title"": ""Fin de Creación de Contenido | {campaign.ClientName}"",
                        ""image"": {{""url"": ""{imageURL}""}},
                        ""description"": ""¡La fase de creación de contenido ha terminado! :tada:\n\nEs el momento de subir las métricas de tu video para ser considerado en la distribución del presupuesto. ¡Asegúrense de enviarlas a tiempo y estén atentos a la notificación final!"",
                        ""color"": 4046592,
                        ""fields"": [
                            {{
                                ""name"": "":dart: Formulario Google"",
                                ""value"": ""[Haz click aquí para enviar tus métricas]({campaign.GoogleFormsLink})""
                            }},
                            {{
                                ""name"": "":calendar_spiral: Plazo de envío"",
                                ""value"": ""Tienen 3 días para enviar sus métricas!""
                            }}
                        ],
                        ""footer"": {{
                            ""text"": ""Recuerda utilizar tu correo gmail de registro para acceder al formulario.""
                        }}
                    }}
                ]
            }}";

            using var httpClient = new HttpClient();
            var content = new StringContent(message, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(webhookURL, content);
        }

        public async Task<HttpResponseMessage> SendCampaignEndMessage(string webhookURL, string imageURL, Campaign campaign, IEnumerable<Metric> metrics)
        {

            var winningCreators = string.Join("\\n", metrics.Select(m =>
                $"- {m.CreatorName} - **{m.NumViews:N0} Vistas** - {m.CalculatedCreatorPayout} Bs."
            ));

            var message = $@"
            {{
                ""content"": ""🎉 ¡La campaña Reapify para **{campaign.ClientName}** ha terminado! 🎉"",
                ""embeds"": [
                    {{
                        ""title"": ""🏆 Resultados Finales | {campaign.ClientName}"",
                        ""image"": {{""url"": ""{imageURL}""}},
                        ""description"": ""La campaña ha llegado a su fin y estos son los creadores que lograron llevarse una parte del presupuesto. Si no apareces en la lista, significa que el presupuesto de **{campaign.CreatorBudget}** Bs. se agotó antes de alcanzar tu nivel de vistas.\n\n¡Sigue participando en futuras campañas para tener más oportunidades! 🚀"",
                        ""color"": 4046592,
                        ""fields"": [
                            {{
                                ""name"": ""💰 Presupuesto Total"",
                                ""value"": ""**{campaign.CreatorBudget}** Bs.""
                            }},
                            {{
                                ""name"": "":dollar: **Tarifa de pago**"",
                                ""value"": ""{campaign.CreatorPayoutRate:N2} Bs. por cada 1000 vistas""}},
                            {{
                                ""name"": ""🏅 Creadores Ganadores"",
                                ""value"": ""{winningCreators}""
                            }}
                        ],
                        ""footer"": {{
                            ""text"": ""Los pagos se procesarán en un máximo de 5 días hábiles. ¡Gracias por participar!""
                        }}
                    }}
                ]
            }}";

            using var httpClient = new HttpClient();
            var content = new StringContent(message, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(webhookURL, content);
        }

    }
}
