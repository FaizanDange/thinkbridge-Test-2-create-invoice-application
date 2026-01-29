using InvoiceApp.Data;
using InvoiceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Connection - SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=invoice.db"));

// Services DI
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Seed DB
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
        
        if (!db.Invoices.Any())
        {
            var invoice = new InvoiceApp.Models.Invoice { CustomerName = "Faizan Dange" };
            db.Invoices.Add(invoice);
            db.SaveChanges(); // to get ID
            
            db.InvoiceItems.Add(new InvoiceApp.Models.InvoiceItem { Name = "Widget A", Price = 19.99m, InvoiceId = invoice.Id });
            db.InvoiceItems.Add(new InvoiceApp.Models.InvoiceItem { Name = "Widget B", Price = 25.50m, InvoiceId = invoice.Id });
            db.SaveChanges(); // Save first invoice items

            // Add second invoice
            var invoice2 = new InvoiceApp.Models.Invoice { CustomerName = "nadeem" };
            db.Invoices.Add(invoice2);
            db.SaveChanges();
            db.InvoiceItems.Add(new InvoiceApp.Models.InvoiceItem { Name = "demo", Price = 100m, InvoiceId = invoice2.Id });
            db.SaveChanges();
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
