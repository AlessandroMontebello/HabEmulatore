using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms;

namespace Akiled.Communication.Packets.Incoming.Structure
{
    class SitEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Room room = Session.GetHabbo().CurrentRoom;
        }
    }
}