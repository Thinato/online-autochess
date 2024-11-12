using gameServer.networking.packets;
using gameServer.networking.packets.incoming;
using gameServer.game;

namespace gameServer.networking.handlers;

class PongHandler : PacketHandlerBase<Pong>
{
    public override PacketId ID => PacketId.PONG;

    protected override void HandlePacket(Client client, Pong packet)
    {
        client.Manager.Logic.AddPendingAction(t => Handle(client, packet, t));
    }

    private void Handle(Client client, Pong packet, RealmTime t)
    {
        client.Player?.Pong(t, packet);
    }
}
