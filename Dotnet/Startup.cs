using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static DotNet6Mediator.ApplicationLayer.ApplicationLayerConfiguration;
using static DotNet6Mediator.InfrastructureLayer.InfrastructureLayerConfiguration;

namespace DotNet6Mediator
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructureLayer(this.Configuration); 
            services.AddApplicationLayer();
            
        }

        public void Configure(WebApplication app)
        {
            app.UseApplicationLayer();
        }
    }
}
