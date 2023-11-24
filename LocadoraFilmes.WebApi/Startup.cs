using LocadoraFilmes.Infra.IoC.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace LocadoraFilmes.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocadoraFilmes.WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Autenticao baseada em Json Web Token (JWT)",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "LocadoraFilmes.WebApi.xml");
                var xmlModelPath = Path.Combine(AppContext.BaseDirectory, "LocadoraFilmes.Application.xml");

                //documentando a api(arquivos swagger)
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(xmlModelPath);
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            //m�todo de extens�o criado para deixar mais organizado o c�digo
            //decidi fazer uma simples classe para gerar o JWT, n�o vi a nessecidade de utilizar
            //o identityserver4 por ser um projeto pequeno, adicionei somente o cpf do usu�rio na claim
            services.AddJwtBearerAutentication(Configuration);
            services.AddControllers();

            //m�todo de extens�o criado para deixar mais organizado o c�digo
            //e para o webapi n�o depender diretamente das classe que precisam ser criadas
            services.AddDataServices(Configuration);

            //m�todo de extens�o criado para deixar mais organizado o c�digo
            //e para o webapi n�o depender diretamente das classe que precisam ser criadas
            services.AddSerives();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocadoraFilmes.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            //usado o app.UseExceptionHandler para capturar exce��es inesperadas,
            //assim posso retornar uma mensagem padr�o para o usu�rio e n�o precisarei
            //fazer isso para cada endpoint
            app.ConfigureExceptionHandler();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
