var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<MoodleTrueFalse.Api.Services.XmlGenerationService>();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // هذا السطر يجعل الخادم لا يهتم بحالة الأحرف في أسماء حقول JSON
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
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

// app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
