using OmarioooCare.DataAccess;
using OmarioooCare.DataRepository;
using OmarioooCare.Models;

var builder = WebApplication.CreateBuilder(args);

// Inject DbConnectionFactory
builder.Services.AddSingleton<IDbConnectionFactory>(ServiceProvider =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("conn")));

// Inject LogingChecking
builder.Services.AddScoped<ILoginService, LoginChecking>();

// Inject the Hospital data
builder.Services.AddScoped<IDataRepository<Hospital>, HospitalRepository>();

// Inject the Patient data
builder.Services.AddScoped<IDataRepository<Patient>, PatientRepository>();

// Inject the Medicalstaff data
builder.Services.AddScoped<IDataRepository<MedicalStaff>, MedicalStaffRepository>();

// Inject the Room data
builder.Services.AddScoped<IDataRepository<Room>, RoomRepository>();

// Inject the Clinic data
builder.Services.AddScoped<IDataRepository<Clinic>, ClinicRepository>();

// Inject the Department data
builder.Services.AddScoped<IDataRepository<Department>, DepartmentRepository>();


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
