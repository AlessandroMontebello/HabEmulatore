using Akiled.Communication.Packets.Outgoing;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.HotelView;

namespace Akiled.Communication.Packets.Incoming.Structure
{
    class GetPromoArticlesEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            HotelViewManager currentView = AkiledEnvironment.GetGame().GetHotelView();
        }
    }
}