using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Http.Json;
using static System.Console;
using Posts;

HttpClient client = new HttpClient();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Enter API", Description = "Lose your shit", Version = "v1" });
});
    
var app = builder.Build();
    
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enter API V1");
});
    
app.MapGet("/getAllEntries", async () => {
   var response = await client.GetAsync("http://localhost:8080/Entries");
   var jsonresponse = await response.Content.ReadAsStringAsync();
   return jsonresponse;
});
app.MapGet("/getOneEntry", async (int id) => {
   var response = await client.GetAsync($"http://localhost:8080/Entries/{id}");
   var jsonResponse = await response.Content.ReadAsStringAsync();
   return jsonResponse;
});
app.MapPost("/Entry", async (Entry entry) => {
   StringContent jsonEntry = new(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
   var response = await client.PostAsync("http://localhost:8080/Entries", jsonEntry);
   var jsonResponse = await response.Content.ReadAsStringAsync();
   return jsonResponse;
});
app.MapPut("/Entry", async (Entry entry, int id) => {
   StringContent jsonEntry = new(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
   var response = await client.PutAsync($"http://localhost:8080/Entries/{id}", jsonEntry);
   var jsonResponse = await response.Content.ReadAsStringAsync();
   return jsonResponse;
});
app.MapDelete("/Entry", async (int id) => {
   var response = await client.DeleteAsync($"http://localhost:8080/Entries/{id}");
   var jsonResponse = await response.Content.ReadAsStringAsync();
   return jsonResponse;
});
app.Run();