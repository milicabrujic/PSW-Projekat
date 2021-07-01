using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Repositories;
using PSW_backend.Services.Interfaces;
using PSW_backend.Services;
using Grpc.Core;
using Rs.Ac.Uns.Ftn.Grpc;


namespace PSW_backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private Server server;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the DI container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
          //  private Server server;
            services.AddHttpClient();

            services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });
            services.AddControllers();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            // For Entity Framework  
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            //IRepositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();
            services.AddScoped<IPatientFeedbackRepository, PatientFeedbackRepository>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<IDrugRepository, DrugRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

            //IServices
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IMedicalAppointmentService, MedicalAppointmentService>();
            services.AddScoped<IPatientFeedbackService, PatientFeedbackService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IDrugService, DrugService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();

            //cors for frontend
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:8081");

                });
            });

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext applicationDbContext, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("VueCorsPolicy");

            app.UseMvc();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

             server = new Server
            {
                Services = { NetGrpcService.BindService(new NetGrpcServiceImpl()) },
                Ports = { new ServerPort("localhost", 4111, ServerCredentials.Insecure) }
            };
            server.Start();

            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        private void OnShutdown()
        {
            if (server != null)
            {
                server.ShutdownAsync().Wait();
            }

        }

    }
}
