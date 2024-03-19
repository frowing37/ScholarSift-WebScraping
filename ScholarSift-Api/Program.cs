using Elasticsearch.Net;
using Nest;
using ScholarSift_Api.Extensions;
using ScholarSift_Data.Concrete;
using ScholarSift_Data.Repostories;
using ScholarSift_Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));
builder.Services.AddSingleton<ArticleService>();
builder.Services.AddSingleton<PDFService>();
builder.Services.AddScoped<ElasticService>();
builder.Services.AddScoped<ElasticRepostory>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddElastic(builder.Configuration);

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