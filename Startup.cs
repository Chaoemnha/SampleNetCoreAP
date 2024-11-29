using DataAccess.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using DataAccess.ConfigurationManager;
using Microsoft.Extensions.Configuration;
using BusinessAccess.Repository;
using Microsoft.OpenApi.Models;
using BusinessAccess.Service;

namespace SampleNetCoreAPI
{
    //Startup là 1 service, và SampleNetCoreAPI là nơi mà bạn định nghĩa và cấu hình các dịch vụ,
    //middleware và các thiết lập khác cho ứng dụng API của mình
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json");
            //khởi tạo Configuration từ file appSettings.json với mục đích là get ConnectionString
            var connectionStringConfig = builder.Build();
            //Add config from database
            //SD Configuration trên lấy connectionstring
            //và xuống DB get data lên -> add vào Configuration của API
            var config = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddEntityFrameworkConfig(options =>
                options.UseSqlServer(connectionStringConfig.GetConnectionString("SQLServerConnection")));
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // Phương thức này được gọi bởi runtime. Sử dụng phương thức này để thêm service vào container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("SQLServerConnection");
            services.AddDbContext<SamplnetcoredbContext>(options =>
              options.UseSqlServer(sqlConnectionString)
          );
            //đăng ký nó vào Dependency Injection: có nghĩa cứ interface IRepository sẽ được implement ở
            //Repository
            #region Add Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #endregion
            #region Add Service
            services.AddScoped(typeof(IBlogService), typeof(BlogService));
            #endregion
            #region Add Configuration to dependency injection
            services.AddSingleton<IConfiguration>(Configuration);
            #endregion
            services.AddControllersWithViews(option => option.EnableEndpointRouting = false);
            services.AddMvc();
            services.AddSwaggerGen(c => 
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" }); 
            });
        }

        // Phương thức này được gọi bởi runtime. Sử dụng phương thức này để cấu hình đường ống yêu cầu (request pipeline) HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API v1");
                });
            }
            app.UseMvc();
        }
    }
}
