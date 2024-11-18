namespace Server.Packets;

public enum PacketID : byte {
    PING = 0,
    JOIN_QUEUE = 1,
    LEAVE_QUEUE = 2,
    SEND_MESSAGE = 3,

}