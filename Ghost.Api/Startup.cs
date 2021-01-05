using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
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
        // �˷���������ʱ���á�ʹ�ô˷�����������ӷ���
        // �й��������Ӧ�ó���ĸ�����Ϣ������� https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ghost_�ӿ��ĵ�",
                    Version = "v1",
                    //�ӿ��ĵ�˵��
                    //Description = "Ghost HTTP API V1",
                    //��ӳ����ӵ�ַ
                    //Contact = new OpenApiContact { Name = "Ghost", Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    //License = new OpenApiLicense { Name = "Ghost", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });

                // ������ȨС��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // ��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.IncludeXmlComments("Ghost.Api.xml", true);//ע����ʾ 
                //����identityserver4
                //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        Implicit = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri($"{Appsettings.app(new string[] { "Startup", "IdentityServer4", "AuthorizationUrl" })}/connect/authorize"),
                //            Scopes = new Dictionary<string, string> {
                //                { "blog.core.api","ApiResource id" }
                //            }
                //        }
                //    }
                //});

                // Jwt Bearer ��֤�������� oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }

        // �˷���������ʱ���á�ʹ�ô˷�������HTTP����ܵ���
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
