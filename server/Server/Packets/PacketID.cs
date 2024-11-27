namespace Server.Packets;

public enum PacketID : byte {
    PING = 0,
    JOIN_QUEUE = 1,
    LEAVE_QUEUE = 2,
    GAME_ADDRESS = 3,
    ADD_GAME_SERVER = 4,
}