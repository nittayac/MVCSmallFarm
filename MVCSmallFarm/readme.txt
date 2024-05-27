"Server=DESKTOP-VIK7108;Database=HaveANiceday;Trusted_Connection=True"



// For add EF Core in the MVC project (Database First Approach)
Scaffold-DbContext "Server=DESKTOP-VIK7108;Database=HaveANiceday;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\db



// For add/update column in table  (Database First Approach) (This code dosen't work properly)
Scaffold-DbContext "Server=DESKTOP-VIK7108;Database=HaveANiceday;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\db  -Tables Product -Force


Scaffold-DbContext "Server=DESKTOP-VIK7108;Database=HaveANiceday;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\db  -Tables dbo.Product,ProductType,Suppliers -f


// For add/update column in table  (Database First Approach : For a whole entites)
Scaffold-DbContext "Server=DESKTOP-VIK7108;Database=HaveANiceday;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\dbs -Force


Scaffold-DbContext "Server=DESKTOP-VIK7108;Database=SmallFarm;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\dbs -Force


 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("SmallFarm"));
    }