using Entity.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using MovieStreamingServiceApi.Middleware.JwtAuthenticationMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieStreamingServiceApi.Controller;
using MovieStreamingServiceApi;
using System.Text;
using Exception;
using Entity.Dto;
using Service;
using Repository;
using Contract.IService;
using Contract.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Model state action filter
builder.Services.AddControllers( config => 
{
    config.Filters.Add(new ModelStateActionFilter());
}).ConfigureApiBehaviorOptions(
    options =>
    {
    options.SuppressModelStateInvalidFilter=true;
    }
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuration for ApiDbContext
builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"),b=>b.MigrationsAssembly("Entity")));

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

//Configuration for auto mapper
builder.Services.AddScoped<MappingProfile>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Handle Custom exception and internal server error globally
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            Console.WriteLine($"Something went wrong:{contextFeature.Error}");
            context.Response.ContentType = "application/json";
            BaseCustomException? baseException = contextFeature.Error as BaseCustomException;
            //If the thrown exception is BaseCustomException
            if (baseException != null)
            {
                context.Response.StatusCode = baseException.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto
                {
                    StatusCode = baseException.StatusCode,
                    Message = baseException.Messages,
                    Description = baseException.Description,
                    Error = baseException.Error,
                })); 
            }
            // //For handling the internal server globally
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDto
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error",
                    Description = "An unexpected error occurred while processing your request."
                })); 
            }
        }
    });
});


app.UseMiddleware<JwtAuthenticationMiddleware>();

app.UseMiddleware<JwtLogoutMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
