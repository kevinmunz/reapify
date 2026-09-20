using Advertisements.Services;
using Advertisements.Models.CampaignModels;
using Advertisements.Models.ClientModels;
using Advertisements.Models.CreatorModels;
using Advertisements.Models.MetricModels;
using Advertisements.Models.TransactionModels;

// Exercise every inherited data property so new model fields cannot silently disappear in edit forms.
static void Verify<T>(Func<T, object> convert) where T : new()
{
    var source = new T();
    var properties = typeof(T).GetProperties().Where(p => p.CanWrite).ToArray();
    for (int i = 0; i < properties.Length; i++)
    {
        var property = properties[i];
        var type = property.PropertyType;
        object value = type == typeof(string) ? $"sample-{property.Name}"
            : type == typeof(int) ? i + 11
            : type == typeof(decimal) ? 17.25m + i
            : type == typeof(DateTime) ? new DateTime(2026, 9, 1).AddDays(i)
            : type.IsEnum ? Enum.GetValues(type).GetValue(0)
            : throw new Exception($"Add a sample for {property.Name}: {type}");
        property.SetValue(source, value);
    }
    var target = convert(source);
    if (ReferenceEquals(source, target)) throw new Exception("Expected a new edit model");
    foreach (var property in properties)
    {
        var actual = target.GetType().GetProperty(property.Name)?.GetValue(target);
        if (!Equals(property.GetValue(source), actual))
            throw new Exception($"{typeof(T).Name}.{property.Name} was not preserved");
    }
    Console.WriteLine($"PASS {typeof(T).Name}: {properties.Length} data fields preserved");
}
Verify<Campaign>(source => EditViewModels.From(source));
Verify<Client>(source => EditViewModels.From(source));
Verify<Creator>(source => EditViewModels.From(source));
Verify<Metric>(source => EditViewModels.From(source));
Verify<Transaction>(source => EditViewModels.From(source));
