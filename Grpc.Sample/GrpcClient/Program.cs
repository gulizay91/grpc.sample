namespace GrpcClient
{
  using Grpc.Core;
  using Grpc.Net.Client;
  using GrpcServer.Protos;
  using System;
  using System.Threading.Tasks;

  /// <summary>
  /// Defines the <see cref="Program" />.
  /// </summary>
  internal class Program
  {
    #region Methods

    /// <summary>
    /// The Main.
    /// </summary>
    /// <param name="args">The args<see cref="string[]"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    internal static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      using var channel = GrpcChannel.ForAddress("https://localhost:5001");
      var client = new CommunicationService.CommunicationServiceClient(channel);
      string username = "joe doe";
      using var stream = client.Communicate();
      var response = Task.Run(async () => {
        await foreach (var item in stream.ResponseStream.ReadAllAsync())
        {
          Console.WriteLine(item.Message);
        }
      });
      
      Console.WriteLine("enter message to server");
      while (true)
      {
        var text = Console.ReadLine();
        if (string.IsNullOrEmpty(text))
          break;
        await stream.RequestStream.WriteAsync(new DialogMessage()
        {
          Username = username,
          Message = text
        });
      }
      await stream.RequestStream.CompleteAsync();
      await response;
    }

    #endregion
  }
}
