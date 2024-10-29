using System.Collections;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace common {
    public class ServerConfig {
        public DbInfo dbInfo { get; set; } = new DbInfo();
        public ServerInfo serverInfo { get; set; } = new ServerInfo();
        public ServerSettings serverSettings { get; set; } = new ServerSettings();

        public static ServerConfig ReadFile(string fileName) {
            using var r = new StreamReader(fileName);

            var result = ReadJson(r.ReadToEnd()) ?? throw new JsonReaderException("Failed to parse JSON: " + fileName);

            return result;
        }

        public static ServerConfig? ReadJson(string json) {
            return JsonConvert.DeserializeObject<ServerConfig>(json);
        }
    }

    public class DbInfo {
        public string Host { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 6379;
        public string Auth { get; set; } = "";
        public int Index { get; set; } = 0;
    }

    public class ServerInfo {
        public ServerType Type { get; set; } = ServerType.World;
        public string Name { get; set; } = "Localhost";
        public string Address { get; set; } = "127.0.0.1";
        public string BindAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 8080;
        public Coordinates Coordinates { get; set; } = new Coordinates();
        public int Players { get; set; } = 0;
        public int MaxPlayers { get; set; } = 100;
        public int QueueLength { get; set; } = 0;
        public bool AdminOnly { get; set; } = false;
        public int MinRank { get; set; } = 0;
        public string InstanceId { get; set; } = "";
        public PlayerList PlayerList { get; set; } = new PlayerList();
    }

    public class ServerSettings {
        public string LogFolder { get; set; } = "./logs";
        public string ResourceFolder { get; set; } = "./resources";
        public string Log4netConfig { get; set; } = "log4net.config";
        public string Version { get; set; } = "1.0.0";
        public int Tps { get; set; } = 20;
        public ServerMode Mode { get; set; } = ServerMode.Single;
        public string Key { get; set; } = "B1A5ED";
        public int MaxConnections { get; set; } = 64;
        public int MaxPlayers { get; set; } = 30;
        public string SendGridApiKey { get; set; } = "";
    }

    public enum ServerType {
        Account,
        World
    }

    public enum ServerMode {
        Single,
        Nexus,
        Realm,
        Marketplace
    }

    public class Coordinates {
        public float latitude { get; set; } = 0;
        public float longitude { get; set; } = 0;
    }

    public class PlayerInfo {
        public string? Name;
        public bool InQueue;
        public int GameInstance;
    }

    public class PlayerList : IEnumerable<PlayerInfo> {
        private readonly ConcurrentDictionary<PlayerInfo, int> PlayerInfo;

        public PlayerList(IEnumerable<PlayerInfo>? playerList = null) {
            PlayerInfo = new ConcurrentDictionary<PlayerInfo, int>();

            if (playerList == null)
                return;

            foreach (var plr in playerList) {
                Add(plr);
            }
        }

        public void Add(PlayerInfo playerInfo) {
            PlayerInfo.TryAdd(playerInfo, 0);
        }

        public void Remove(PlayerInfo playerInfo) {
            if (playerInfo == null)
                return;

            int ignored;
            PlayerInfo.TryRemove(playerInfo, out ignored);
        }

        IEnumerator<PlayerInfo> IEnumerable<PlayerInfo>.GetEnumerator() {
            return PlayerInfo.Keys.GetEnumerator();
        }

        public IEnumerator GetEnumerator() {
            return PlayerInfo.Keys.GetEnumerator();
        }
    }
}