using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3;
using OpenAI.GPT3.Extensions;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var keys = builder.Configuration.GetSection("ApiKeys:OpenAI");

builder.Services.AddOpenAIService(settings => 
    { settings.ApiKey = keys.Value; });


builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy",
                          policy =>
                          {
                              policy.WithOrigins("*")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//var openAiService = app.Services.GetRequiredService<IOpenAIService>();

app.UseAuthorization();

app.MapControllers();

app.UseCors("corsPolicy");

app.MapGet("/",() => "hi!");

app.Run();