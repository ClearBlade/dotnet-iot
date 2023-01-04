﻿namespace ClearBlade.API.dotnet.client.core.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Config
    {
        public DateTime CloudUpdateTime { get; set; }
        public string Version { get; set; }

        public Config()
        {
            Version = String.Empty;
        }
    }

    public class Credential
    {
        public string ExpirationTime { get; set; }
        public PublicKey PublicKey { get; set; }

        public Credential()
        {
            ExpirationTime = String.Empty;
            PublicKey = new PublicKey();
        }
    }

    public class DeviceCollection
    {
        public List<DeviceModel> Devices { get; set; }

        public DeviceCollection()
        {
            Devices = new List<DeviceModel>();
        }
    }

    /// <summary>
    /// Class that represents a device
    /// </summary>
    public class DeviceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NumId { get; set; }
        public List<Credential> Credentials { get; set; }
        public string LastHeartbeatTime { get; set; }
        public string LastEventTime { get; set; }
        public string LastStateTime { get; set; }
        public string LastConfigAckTime { get; set; }
        public string LastConfigSendTime { get; set; }
        public bool Blocked { get; set; }
        public string LastErrorTime { get; set; }
        public LastErrorStatus LastErrorStatus { get; set; }
        public Config Config { get; set; }
        public State State { get; set; }
        public string LogLevel { get; set; }
        public Metadata Metadata { get; set; }
        public GatewayConfig GatewayConfig { get; set; }

        public DeviceModel()
        {
            Id = string.Empty;
            Name = string.Empty;
            NumId = string.Empty;
            Credentials = new List<Credential>();
            LastHeartbeatTime = string.Empty;
            LastEventTime = string.Empty;
            LastStateTime = string.Empty;
            LastConfigAckTime = string.Empty;
            LastConfigSendTime = string.Empty;
            LastErrorTime = string.Empty;
            LastErrorStatus = new LastErrorStatus();
            Config = new Config();
            State = new State();
            LogLevel = "NONE";
            Metadata = new Metadata();
            GatewayConfig = new GatewayConfig();
        }
    }

    public class GatewayConfig
    {
        public string lastAccessedGatewayId { get; set; }
        public string lastAccessedGatewayTime { get; set; }

        public GatewayConfig()
        {
            lastAccessedGatewayId = string.Empty;
            lastAccessedGatewayTime = string.Empty;
        }
    }

    public class LastErrorStatus
    {
        public int code { get; set; }
        public string message { get; set; }

        public LastErrorStatus()
        {
            message = string.Empty;
        }
    }

    public class Metadata
    {
    }

    public class PublicKey
    {
        public string format { get; set; }
        public string key { get; set; }

        public PublicKey()
        {
            format = string.Empty;
            key = string.Empty;
        }
    }

    public class State
    {
        public string updateTime { get; set; }
        public string binaryData { get; set; }

        public State()
        {
            updateTime = String.Empty;
            binaryData = String.Empty;
        }
    }


}
