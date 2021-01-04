using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ghost.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // 此方法由运行时调用。使用此方法向容器添加服务。
        // 有关如何配置应用程序的更多信息，请访问 https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ghost_接口文档",
                    Version = "v1",
                    //接口文档说明
                    //Description = "Ghost HTTP API V1",
                    //添加超链接地址
                    //Contact = new OpenApiContact { Name = "Ghost", Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    //License = new OpenApiLicense { Name = "Ghost", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                c.IncludeXmlComments("Ghost.Api.xml", true);//注释提示
            });
        }

        // 此方法由运行时调用。使用此方法配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ghost v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
