using System;
using Buy_And_Sell_House_MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Buy_And_Sell_House_MVC.Areas.Identity.IdentityHostingStartup))]
namespace Buy_And_Sell_House_MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Buy_And_Sell_House_MVCIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Buy_And_Sell_House_MVCIdentityContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<Buy_And_Sell_House_MVCIdentityContext>();
            });
        }
    }
}