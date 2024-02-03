using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms;

namespace Akiled.Communication.Packets.Incoming.Structure
{
    class MoveAvatarEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            Room currentRoom = Session.GetHabbo().CurrentRoom;
            {
                pX = User.SetX + (User.SetX - pX);
                pY = User.SetY + (User.SetY - pY);
            }
        }
    }
}