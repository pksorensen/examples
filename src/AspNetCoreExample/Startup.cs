using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Unity;

namespace WebApplication1
{
    public class MyCoolOptions
    {
        public string Value { get; set; }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Configure Unity container
        public void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterInstance("This string is displayed if container configured correctly", 
                                       "This string is displayed if container configured correctly");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Container could be configured via services as well. 
            // Just be careful not to override registrations
            services.AddSingleton("This string also displayed if container configured correctly");


            services.Configure<MyCoolOptions>(o =>
            {
                o.Value = "This should be displayed";
            });
           
            services.AddSingleton<MyCoolOptions>(
                resolver => resolver.GetRequiredService<IOptions<MyCoolOptions>>().Value);



            // Add MVC as usual
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
