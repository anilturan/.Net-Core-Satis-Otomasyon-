using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Abc.Northwind.Business.Abstract;
using Abc.Northwind.Business.Concrete;
using Abc.Northwind.DataAccess.Abstract;
using Abc.Northwind.DataAccess.Concrete.EntityFramework;
using Abc.Northwind.MvcWebUI.Services;
using Microsoft.AspNetCore.Http;
using Abc.Northwind.MvcWebUI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Abc.Northwind.MvcWebUI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        //Service Yapılandırmalarımızı Gerçekleştiriyoruz. Buraya Service bağımlılıklarımızı ekliyoruz.
        //Örneğin bir Controller'ın içersinde alt katmanlardan gelen interface'lere ihtiyaç duyduğumuzda onun ne olduğunu burada belirtiyoruz. (Dependency Injection yapılandırması)
        public void ConfigureServices(IServiceCollection services)
        {
            //Service'lerimizi eklediğimiz kısım.

            //Eğer ki biri senden IProductService isterse ona new leyip ProductManager döndürür.

            //Her Request'te yeni bir nesne oluşur.
            //Aynı Request'te birden fazla aynı tipte nesneye ihtiyaç duyulursa bu durumda aynı nesne tipinde tek bir nesne oluşur. (Referans type gibi hareket ederler.)
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDAL, EFProductDAL>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();

            //Bunlar Session işlemlerini gerçekleştirmek için yazıldı. Singleton olma sebebi tüm proje boyunca referansın aktif olması.
            services.AddSingleton<ICartSessionService, CartSessionService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<CustomIdentityDbContext>
               (options => options.UseSqlServer("Server=.;Database=NORTHWND2;Integrated Security=true"));
            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
                .AddEntityFrameworkStores<CustomIdentityDbContext>()
                .AddDefaultTokenProviders();

            //Package'da AspNetCore.Session diye aratıp yüklemek gerekiyor.
            services.AddSession();
            //Bu da Session'ı RAM'de tutabilmek için yazılıyor, aksi halde Session etkinleştirilmeli diye hata verecektir.
            //Bunu eklediten sonra aşağıdaki Configure metotuna gidip app.UseSession(); yazarak projemize session eklenmesini sağlıyoruz.
            services.AddDistributedMemoryCache();

            //Her Request'te yeni bir nesne oluşur.
            //Aynı Request'te birden fazla aynı tipte nesneye ihtiyaç duyulursa bu durumda aynı nesne tipinde iki tane farklı nesne oluşacaktır. (Value Type gibi hareket ederler.)
            //services.AddTransient

            //Singleton Pattern gibi çalışır bir kere oluşturulur ve tüm kullanıcı Requestleri için aynı nesne kullanılır. Dolayısıyla Singleton ile tanımlanan nesnenin ortak kullanıma aykırı olmaması gerekir.
            //services.AddSingleton

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //Middleware(Orta Katman) yapılandırmasını yağtığımız yer.
        //State Management'ların yönetildiği yer.
        //Kullandığınız metotları tanımladığımız için çok ciddi bir performans yaratıyor.
        //Loglama, exception gibi middleware eklemeleri yapılabilir.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
