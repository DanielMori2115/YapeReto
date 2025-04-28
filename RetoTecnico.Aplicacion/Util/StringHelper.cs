using System.Text.Json;

namespace RetoTecnico.Aplicacion.Util;

public static class StringHelper
{
    public static string ToJson(this object value)
    {
        return JsonSerializer.Serialize(value);
    }
}
