using AccountLedgerAPI;
using AccountLedgerAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcCore(options =>
{
    options.Filters.Add(new AccountLedgerAPIErrorHandler());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.InitializeApplicationSettings();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.
                       AllowAnyOrigin().
                       AllowAnyHeader().
                       WithMethods(new string[] { "Get", "Post", "Put", "Delete" }));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
