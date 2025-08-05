// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Features;
using UrlShortener.Features.Data;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IUrlMapDb, InMemoryUrlMapDb>();
builder.Services.AddTransient<IUrlShortener, UrlShortener.Features.Features.UrlShortener>();  

var app = builder.Build();