namespace ClearBlade.API.dotnet.client.core.Models
{
    public class CreateRegistryModel
    {
        public List<object> Credentials { get; set; }
        public string Id { get; set; }
        public List<EventNotificationConfig> EventNotificationConfigs { get; set; }
        public StateNotificationConfig StateNotificationConfig { get; set; }
        public HttpConfig HttpConfig { get; set; }
        public MqttConfig MqttConfig { get; set; }
        public string LogLevel { get; set; }

        public CreateRegistryModel()
        {
            Credentials = new List<object>();
            Id = string.Empty;
            EventNotificationConfigs = new List<EventNotificationConfig>();
            MqttConfig = new MqttConfig();
            LogLevel = string.Empty;
            HttpConfig = new HttpConfig();
            StateNotificationConfig = new StateNotificationConfig();
        }
    }
}
