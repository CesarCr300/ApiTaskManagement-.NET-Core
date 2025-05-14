// Extensions/FirebaseJwtExtension.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class FirebaseJwtExtension
{
    public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/" + configuration["Firebase:ProjectId"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/" + configuration["Firebase:ProjectId"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Firebase:ProjectId"],
                    ValidateLifetime = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].ToString();
                        Console.WriteLine($"Auth header recibido: {authHeader}");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Token inválido: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token válido.");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
