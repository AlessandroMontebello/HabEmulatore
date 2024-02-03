using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Items;
using System;
using System.Data;


namespace Akiled.HabboHotel.Rooms.Chat.Commands.SpecialPvP
{
    class ConvertCreditsCommand : IChatCommand
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

                    if (!Item.GetBaseItem().ItemName.StartsWith("CF_") && !Item.GetBaseItem().ItemName.StartsWith("CFC_"))
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
                        Session.GetHabbo().Credits += Value;
                        Session.SendPacket(new CreditBalanceComposer(Session.GetHabbo().Credits));
                    }
                }

                if (TotalValue > 0)
                    Session.SendNotification("Tutti i tuoi crediti nell'inventario sono stati trasferiti nel tuo portafoglio!\r\r(Totale di: " + TotalValue + " crediti!");
                else
                    Session.SendNotification("Apparentemente non ha altri oggetti intercambiabili!");
            }
            catch
            {
                Session.SendNotification("Spiacenti, si è verificato un errore durante lo scambio dei tuoi crediti, contatta un amministratore!");
            }
        }
    }
}
