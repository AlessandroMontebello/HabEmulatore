using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms;
using Akiled.HabboHotel.Rooms.Wired;

namespace Akiled.Communication.Packets.Incoming.Structure
{
    class SaveWiredConfigEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Room room = Session.GetHabbo().CurrentRoom;
            Session.SendPacket(new SaveWired());
        }
    }
}