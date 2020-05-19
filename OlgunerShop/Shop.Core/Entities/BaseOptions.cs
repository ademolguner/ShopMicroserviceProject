namespace InfoQ.Core.Entities
{
    public class BaseOptions
    {
        //public Consul Consul { get; set; }
        public Redis Redis { get; set; }
        //public TokenOptions TokenOptions { get; set; }
        public RabbitMq RabbitMq { get; set; }
        public Service[] Services { get; set; }
    }

    public class Consul
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public Check Check { get; set; }
    }

    public class RabbitMq
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }

    public class Redis
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Configuration => $"{Host}:{Port}";
    }

    public class Check
    {
        public string Http { get; set; }
        public int Interval { get; set; }
    }

    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }

    public class Service
    {
        public string ServiceName { get; set; }
        public string ServiceUrl { get; set; }
        public string Port { get; set; }
        public string ServicePath => $"http://{ServiceUrl}:{Port}";
    }
}