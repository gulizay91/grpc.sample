namespace GrpcServer
{
  using Grpc.Core;
  using Microsoft.Extensions.Logging;
  using System.Threading.Tasks;

  /// <summary>
  /// Defines the <see cref="GreeterService" />.
  /// </summary>
  public class GreeterService : Greeter.GreeterBase
  {
    #region Fields

    /// <summary>
    /// Defines the _logger.
    /// </summary>
    private readonly ILogger<GreeterService> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GreeterService"/> class.
    /// </summary>
    /// <param name="logger">The logger<see cref="ILogger{GreeterService}"/>.</param>
    public GreeterService(ILogger<GreeterService> logger)
    {
      _logger = logger;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The SayHello.
    /// </summary>
    /// <param name="request">The request<see cref="HelloRequest"/>.</param>
    /// <param name="context">The context<see cref="ServerCallContext"/>.</param>
    /// <returns>The <see cref="Task{HelloReply}"/>.</returns>
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
      return Task.FromResult(new HelloReply
      {
        Message = "Hello " + request.Name
      });
    }

    #endregion
  }
}
