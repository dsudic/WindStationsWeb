using MQTTnet;
using MQTTnet.Client;
using WindStations.Core.Interfaces;

namespace WindStations.Infrastructure.Services;
public class MqttClientService(IMessagePersistenceService messagePersistenceService) : IMqttClientService
{
    private IMqttClient? _mqttClient;
    private const string Broker = "broker.emqx.io";
    private const string Topic = "windstation/data";

    public async Task ConnectAndSubscribe()
    {
        var mqttFactory = new MqttFactory();

        _mqttClient = mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(Broker).Build();

        _mqttClient.ApplicationMessageReceivedAsync += new Func<MqttApplicationMessageReceivedEventArgs, Task>(HandleReceivedMessage);

        await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(
                f =>
                {
                    f.WithTopic(Topic);
                })
            .Build();

        await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
    }

    private async Task HandleReceivedMessage(MqttApplicationMessageReceivedEventArgs message)
    {
        await messagePersistenceService.SaveAsync([.. message.ApplicationMessage.PayloadSegment]);
    }
}
