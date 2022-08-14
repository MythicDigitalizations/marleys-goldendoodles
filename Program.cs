using Microsoft.EntityFrameworkCore;
using MarleysGoldendoodles.Models;

var builder = WebApplication.CreateBuilder(args);
var config = default(IConfiguration);

if (builder.Environment.IsDevelopment())
{
    config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();
}
else
{
    config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
}

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MyDatabaseContext>(options =>
        options.UseSqlServer(config.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapPost("/create", async (string firstName, string lastName, string phoneNumber, MyDatabaseContext db) =>
{
    var recordCount = await db.CreateWaitlistEntry(firstName, lastName, phoneNumber);
    return recordCount;
;
})
.WithName("CreateWaitlistEntry");

app.MapPost("/update", async (int id, decimal amountPaid, MyDatabaseContext db) =>
{
    var recordCount = await db.UpdateWaitlistEntry(id, amountPaid);
    return recordCount;
;
})
.WithName("UpdateWaitlistEntry");

app.MapPost("/remove", async (int id, MyDatabaseContext db) =>
{
    var recordCount = await db.RemoveWaitlistEntry(id);
    return recordCount;
;
})
.WithName("RemoveWaitlistEntry");

app.Run();
