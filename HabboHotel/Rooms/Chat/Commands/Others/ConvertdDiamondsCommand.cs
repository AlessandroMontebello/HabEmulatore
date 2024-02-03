﻿using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Items;
using System;
using System.Data;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.SpecialPvP
{
    class ConvertdDiamondsCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            int TotalValue = 0;
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
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
                    UserRoom.SendWhisperChat("Al momento non hai diamanti nel tuo inventario!");
                    return;
                }

                foreach (DataRow Row in Table.Rows)
                {
                    Item Item = Session.GetHabbo().GetInventoryComponent().GetItem(Convert.ToInt32(Row[0]));
                    if (Item == null)
                        continue;

                    if (!Item.GetBaseItem().ItemName.StartsWith("DI_") && !Item.GetBaseItem().ItemName.StartsWith("DIA_") && !Item.GetBaseItem().ItemName.StartsWith("DF_"))
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
                        Session.GetHabbo().Duckets += Value;
                        Session.SendPacket(new HabboActivityPointNotificationComposer(Session.GetHabbo().Duckets, Value));
                        Session.GetHabbo().UpdateActivityPointsBalance();
                    }
                }

                if (TotalValue > 0)
                    Session.SendNotification("Tutti i tuoi diamanti nell'inventario sono stati trasferiti nel tuo portafoglio !\r\r(Totale di: " + TotalValue + " diamanti!");
                else
                    Session.SendNotification("Apparentemente non ha altri oggetti intercambiabili!");
            }
            catch
            {
                Session.SendNotification("Spiacenti, si è verificato un errore durante lo scambio dei tuoi diamanti, contatta un amministratore!");
            }
        }
    }
}
