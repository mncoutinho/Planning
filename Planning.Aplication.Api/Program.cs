using Planning.Domain.Core.Services;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BasicViewService>();
builder.Services.AddControllers();
var app = builder.Build();
var bvs = app.Services.CreateScope().ServiceProvider.GetRequiredService<BasicViewService>();

bvs.SetStart(DateTime.Today);
bvs.SetEnd(DateTime.Today.AddMonths(12));
bvs.LoadFromFile().GetAwaiter().GetResult();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
