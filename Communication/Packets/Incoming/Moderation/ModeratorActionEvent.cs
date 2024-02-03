using Akiled.Communication.Packets.Outgoing;
using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.Communication.Packets.Incoming.Structure
{
    class ModeratorActionEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
            string AlertMessage = Packet.PopString();
            bool IsCaution = AlertMode != 3;
            {
                AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("publicidad", "El usuario: " + Session.GetHabbo().Username + ", Pub alert MT:" + AlertMessage + ", pulsa aqu� para ir a mirar.", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
                return;
            }
        }
    }
}