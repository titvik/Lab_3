using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace lab3
{
    public static class SessionExtensions
    {
        public static void SetObj<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize<T>(value));
        }
        
        public static T GetObj<T>(this ISession session, string key)
        {
            
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}