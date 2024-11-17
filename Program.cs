using InvoiceTransformation.Application.Interfaces;
using InvoiceTransformation.Application.Services;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Invoice Transformation API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    c.OperationFilter<ErrorResponsesOperationFilter>();
});

builder.Services.AddScoped<ITransformService, TransformService>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://dev-otrwvksw.us.auth0.com/";
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://dev-otrwvksw.us.auth0.com/",
            ValidateAudience = true,
            ValidAudience = "https://invoice-transformation.cti.com",
            ValidateLifetime = true
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        var problemDetails = new
        {
            status = 500,
            title = "Internal Server Error",
            detail = exception?.Message ?? "An unexpected error occurred.",
            errors = Array.Empty<string>()
        };

        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();

app.Run();

public class ErrorResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add("400", new OpenApiResponse
        {
            Description = "Bad Request - Validation Error",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Example = new OpenApiObject
                        {
                            ["status"] = new OpenApiInteger(400),
                            ["title"] = new OpenApiString("Bad Request"),
                            ["detail"] = new OpenApiString("The 'xml' field is required and cannot be empty."),
                            ["errors"] = new OpenApiArray
                            {
                                new OpenApiString("Field 'xml' is missing or empty.")
                            }
                        }
                    }
                }
            }
        });

        operation.Responses.Add("401", new OpenApiResponse
        {
            Description = "Unauthorized - Invalid or missing token"
        });

        operation.Responses.Add("500", new OpenApiResponse
        {
            Description = "Internal Server Error"
        });
    }
}
