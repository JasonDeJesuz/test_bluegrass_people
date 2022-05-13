using System.Reflection;
using Bluegrass.Shared.DTO.Variance;

namespace Bluegrass.Core.Extensions;

public static class Extensions
{
    public static List<VarianceDTO> Compare<T>(this T val1, T val2) {
        var variances = new List<VarianceDTO>();
        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            var name = prop.Name;
            var oldVal = prop.GetValue(val1);
            var newVal = prop.GetValue(val2);
            if (oldVal != newVal) // When the Old Value and the new Value do not align
            {
                variances.Add(new VarianceDTO()
                {
                    Prop = name,
                    valA = oldVal != null ? oldVal.ToString() : string.Empty,
                    valB = newVal != null ? newVal.ToString() : string.Empty
                });
            }
        }

        return variances;
    }
}