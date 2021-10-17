namespace GrpcServer.Services
{
  using Grpc.Core;
  using GrpcServer.Protos;
  using Microsoft.Extensions.Logging;
  using System;
  using System.Threading.Tasks;

  /// <summary>
  /// Defines the <see cref="CommunicationGrpcService" />.
  /// </summary>
  public class CommunicationGrpcService : CommunicationService.CommunicationServiceBase
  {
    #region Fields

    /// <summary>
    /// Defines the _logger.
    /// </summary>
    private readonly ILogger<CommunicationGrpcService> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CommunicationGrpcService"/> class.
    /// </summary>
    /// <param name="logger">The logger<see cref="ILogger{CommunicationGrpcService}"/>.</param>
    public CommunicationGrpcService(ILogger<CommunicationGrpcService> logger)
    {
      _logger = logger;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The Communicate.
    /// </summary>
    /// <param name="requestStream">The requestStream<see cref="IAsyncStreamReader{DialogMessage}"/>.</param>
    /// <param name="responseStream">The responseStream<see cref="IServerStreamWriter{DialogMessage}"/>.</param>
    /// <param name="context">The context<see cref="ServerCallContext"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public override async Task Communicate(IAsyncStreamReader<DialogMessage> requestStream, IServerStreamWriter<DialogMessage> responseStream, ServerCallContext context)
    {
      if (requestStream != null && !await requestStream.MoveNext())
        return;

      try
      {
        Console.WriteLine($"Receive Message: '{requestStream.Current.Message}' from Client: '{requestStream.Current.Username}'");
        await responseStream.WriteAsync(new DialogMessage()
        {
          Username = "john doe",
          Message = $"Reply 'john doe' from the server @ : {DateTime.UtcNow:s} to {requestStream.Current.Username}"
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);
      }
    }

    #endregion
  }
}
