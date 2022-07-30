using CoingateProvider;
using FileDataStorage;
using SmtpGoogleProvider;
using GSES2_BTC.Core.Contracts.Data;
using GSES2_BTC.Core.Contracts.Data.Messaging;
using GSES2_BTC.Core.Contracts.Service.ExchangeRateProvider;
using GSES2_BTC.Core.Contracts.Service.MessagingProvider;
using GSES2_BTC.Core.Services.MessagingProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IExchangeRateService, ExchangeRateService>();
builder.Services.AddTransient<IMessageSender, EmailSender>();
builder.Services.AddTransient<IRecieverRepo<Reciever>>(x => new UserRepo(builder.Configuration.GetConnectionString("DefaultConnectiion")));
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


