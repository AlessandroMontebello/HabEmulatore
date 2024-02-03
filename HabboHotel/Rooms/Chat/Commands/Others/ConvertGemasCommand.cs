using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Items;
using System;
using System.Data;
namespace Akiled.HabboHotel.Rooms.Chat.Commands.SpecialPvP
{
    class ConvertGemasCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            int TotalValue = 0;

            try
            {
                DataTable Table = null;
                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbClient.SetQuery("SELECT `id` FROM `items` WHERE `user_id` = '" + Session.GetHabbo().Id + "' AND (`room_id`=  '0' OR `room_id` = '')");
                    Table = dbClient.GetTable();
                }

                if (Table == null)
                {
                    UserRoom.SendWhisperChat("Al momento non hai monete nel tuo inventario!");
                    return;
                }

                foreach (DataRow Row in Table.Rows)
                {
                    Item Item = Session.GetHabbo().GetInventoryComponent().GetItem(Convert.ToInt32(Row[0]));
                    if (Item == null)
                        continue;

                    if (!Item.GetBaseItem().ItemName.StartsWith("MODE_") && !Item.GetBaseItem().ItemName.StartsWith("TEMP_"))
                        continue;

                    if (Item.RoomId > 0)
                        continue;

                    string[] Split = Item.GetBaseItem().ItemName.Split('_');
                    int Value = int.Parse(Split[1]);

                    using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.RunQuery("DELETE FROM `items` WHERE `id` = '" + Item.Id + "' LIMIT 1");
                    }

                    Session.GetHabbo().GetInventoryComponent().RemoveItem(Item.Id);

                    TotalValue += Value;

                    if (Value > 0)
                    {
                        Session.GetHabbo().AkiledPoints += Value;
                        Session.SendPacket(new HabboActivityPointNotificationComposer(Session.GetHabbo().AkiledPoints, Value, 105));
                        Session.GetHabbo().UpdateDiamondsBalance();

                        using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                            queryreactor.RunQuery("UPDATE users SET vip_points = vip_points + " + Value + " WHERE id = " + Session.GetHabbo().Id);
                    }
                }

                if (TotalValue > 0)
                    Session.SendNotification("Tutte le monete stagionali presenti nell'inventario sono state inserite nel tuo portafoglio con un !\r\r(Totale di: " + TotalValue + " monete stagionali!");
                else
                    Session.SendNotification("Apparentemente non ha altri oggetti intercambiabili!");
            }
            catch
            {
                Session.SendNotification("Spiacenti, si è verificato un errore durante lo scambio delle monete stagionali, contatta un amministratore!");
            }
        }
    }
}
