using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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

        public IConfiguration Configuration { get; }

        // Phương thức này được gọi bởi runtime. Sử dụng phương thức này để thêm service vào container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("SQLServerConnection") + ";TrustServerCertificate=true";
            services.AddDbContext<SamplnetcoredbContext>(options =>
              options.UseSqlServer(sqlConnectionString)
          );
            services.AddControllersWithViews(option => option.EnableEndpointRouting = false);
            services.AddMvc();
        }

        // Phương thức này được gọi bởi runtime. Sử dụng phương thức này để cấu hình đường ống yêu cầu (request pipeline) HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
