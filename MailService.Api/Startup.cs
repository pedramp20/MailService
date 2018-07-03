namespace MailService.Api
{
    using AutoMapper;

    using MailService.Api.Models;
    using MailService.Api.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // auto mapper is added to mapp obj to obj
            services.AddAutoMapper();

            services.AddMvc();

            services.AddTransient<IMailService, SendGridMailService>();
            services.AddTransient<IMailService, MailGunMailService>();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // The mail secrets are configured to be injected in runtime
            services.Configure<MailSecrets>(Configuration.GetSection("MailServicesSecrets"));
        }
    }
}