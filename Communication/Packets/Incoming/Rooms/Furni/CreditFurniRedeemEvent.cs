using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Items;
using Akiled.HabboHotel.Rooms;

namespace Akiled.Communication.Packets.Incoming.Structure
{
    class CreditFurniRedeemEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().InRoom)
                return;

            Room Room = null;
            if (!AkiledEnvironment.GetGame().GetRoomManager().TryGetRoom(Session.GetHabbo().CurrentRoomId, out Room))
                return;

            if (!Room.CheckRights(Session, true))
                return;
            if (Exchange == null)
                return;

            if (Exchange.Data.InteractionType != InteractionType.EXCHANGE)
                return;

            using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
            Room.GetRoomItemHandler().RemoveFurniture(null, Exchange.Id);

                else if (Exchange.GetBaseItem().ItemName.StartsWith("kk_") || Exchange.GetBaseItem().ItemName.StartsWith("kakas_") || Exchange.GetBaseItem().ItemName.StartsWith("kakacoins_") || Exchange.GetBaseItem().ItemName.StartsWith("TEMP_"))
                {
                    Session.GetHabbo().AkiledPoints += Value;
                    Session.SendPacket(new HabboActivityPointNotificationComposer(Session.GetHabbo().AkiledPoints, Value, 105));
                    Session.GetHabbo().UpdateDiamondsBalance();

                    using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                        queryreactor.RunQuery("UPDATE users SET vip_points = vip_points + " + Value + " WHERE id = " + Session.GetHabbo().Id);
                }
                else if (Exchange.GetBaseItem().ItemName.StartsWith("DIA_") || Exchange.GetBaseItem().ItemName.StartsWith("DF_") || Exchange.GetBaseItem().ItemName.StartsWith("DI_"))
                {
                    Session.GetHabbo().Duckets += Value;
                    Session.GetHabbo().UpdateActivityPointsBalance();
                }
                    {
                        RoomUser roomUserByHabbo = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
                        if (roomUserByHabbo != null)
                        {
                            Session.SendPacket(new UserChangeComposer(roomUserByHabbo, true));
                            Room.SendPacket(new UserChangeComposer(roomUserByHabbo, false));
                        }
                    }
        }
    }
}