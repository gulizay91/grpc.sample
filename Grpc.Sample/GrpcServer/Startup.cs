namespace GrpcServer
{
  using GrpcServer.Services;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

  /// <summary>
  /// Defines the <see cref="Startup" />.
  /// </summary>
  public class Startup
  {
    #region Methods

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// <summary>
    /// The Configure.
    /// </summary>
    /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
    /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        //endpoints.MapGrpcService<GreeterService>();
        endpoints.MapGrpcService<CommunicationGrpcService>();

        endpoints.MapGet("/", async context =>
              {
                await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
              });
      });
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    /// <summary>
    /// The ConfigureServices.
    /// </summary>
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc();
    }

    #endregion
  }
}
