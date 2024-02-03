using Akiled.Communication.Packets.Outgoing.Inventory.AvatarEffects;
using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.Catalog.Clothing;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Items;
using Akiled.HabboHotel.Rooms;

namespace Akiled.Communication.Packets.Incoming.Rooms.Furni
{
    class UseSellableClothingEvent : IPacketEvent
    {
        public void Parse(GameClient session, ClientPacket packet)
        {
            if (session == null || session.GetHabbo() == null || !session.GetHabbo().InRoom)
                return;

            Room room = session.GetHabbo().CurrentRoom;
            if (room == null)
                return;

            int itemId = packet.PopInt();

            Item item = room.GetRoomItemHandler().GetItem(itemId);
            if (item == null)
                return;

            if (item.Data == null)
                return;

            if (item.OwnerId != session.GetHabbo().Id)
                return;

            if (item.Data.InteractionType != InteractionType.PURCHASABLE_CLOTHING)
            {
                session.SendNotification("Spiacenti, questo articolo non è impostato come capo di abbigliamento vendibile!");
                return;
            }

            if (item.Data.BehaviourData == 0)
            {
                session.SendNotification("Spiacenti, questo articolo non ha un collegamento alla configurazione dell'abbigliamento, segnalacelo!");
                return;
            }

            if (!AkiledEnvironment.GetGame().GetCatalog().GetClothingManager().TryGetClothing(item.Data.BehaviourData, out ClothingItem clothing))
            {
                session.SendNotification("Wow, non siamo riusciti a trovare questo capo di abbigliamento!");
                return;
            }

            //Quickly delete it from the database.
            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("DELETE FROM `items` WHERE `id` = @ItemId LIMIT 1");
                dbClient.AddParameter("ItemId", item.Id);
                dbClient.RunQuery();
            }

            //Remove the item.
            room.GetRoomItemHandler().RemoveFurniture(session, item.Id);

            session.GetHabbo().GetClothing().AddClothing(clothing.ClothingName, clothing.PartIds);
            session.SendPacket(new FigureSetIdsComposer(session.GetHabbo().GetClothing().GetClothingParts));
            session.SendPacket(new RoomNotificationComposer("figureset.redeemed.success"));
            session.SendWhisper("Se per qualche motivo non riesci a vedere i tuoi nuovi vestiti, ricarica l'hotel!");
        }
    }
}