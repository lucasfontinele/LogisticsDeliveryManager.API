using Npgsql;

namespace LogisticsDeliveryManager.Infrastructure.Configuration;

internal static class SupabaseConnectionStringFactory
{
    public static string Build(string connectionUrl)
    {
        if (string.IsNullOrWhiteSpace(connectionUrl))
        {
            throw new InvalidOperationException(
                $"A connection string do Supabase nao foi configurada. Defina a chave '{SupabaseSettings.ConnectionUrlKey}' no appsettings.");
        }

        if (!connectionUrl.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)
            && !connectionUrl.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
        {
            return connectionUrl;
        }

        var uri = new Uri(connectionUrl);
        var credentials = uri.UserInfo.Split(':', 2);

        if (credentials.Length != 2)
        {
            throw new InvalidOperationException("A URL de conexao do Supabase esta invalida. O usuario e a senha nao puderam ser identificados.");
        }

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = uri.AbsolutePath.Trim('/'),
            Username = Uri.UnescapeDataString(credentials[0]),
            Password = Uri.UnescapeDataString(credentials[1]),
            SslMode = SslMode.Require,
        };

        ApplyQueryStringOptions(connectionStringBuilder, uri.Query);

        return connectionStringBuilder.ConnectionString;
    }

    private static void ApplyQueryStringOptions(NpgsqlConnectionStringBuilder connectionStringBuilder, string queryString)
    {
        if (string.IsNullOrWhiteSpace(queryString))
        {
            return;
        }

        var queryParameters = queryString.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var parameter in queryParameters)
        {
            var parts = parameter.Split('=', 2);
            var key = Uri.UnescapeDataString(parts[0]);
            var value = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : string.Empty;

            switch (key.ToLowerInvariant())
            {
                case "sslmode":
                    if (Enum.TryParse<SslMode>(value, ignoreCase: true, out var sslMode))
                    {
                        connectionStringBuilder.SslMode = sslMode;
                    }
                    break;
                case "pooling":
                    if (bool.TryParse(value, out var pooling))
                    {
                        connectionStringBuilder.Pooling = pooling;
                    }
                    break;
                case "timeout":
                    if (int.TryParse(value, out var timeout))
                    {
                        connectionStringBuilder.Timeout = timeout;
                    }
                    break;
                case "commandtimeout":
                case "command_timeout":
                    if (int.TryParse(value, out var commandTimeout))
                    {
                        connectionStringBuilder.CommandTimeout = commandTimeout;
                    }
                    break;
            }
        }
    }
}
