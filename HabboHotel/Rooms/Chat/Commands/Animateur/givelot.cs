using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using System;
using System.Collections.Generic;
using System.Data;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd

            int ExtraBox_Item = 0;
            int BadgeBox_Item = 0;
            using (IQueryAdapter dbQuery = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbQuery.SetQuery("SELECT * FROM `game_configextrabox` LIMIT 1");
                DataTable gUsersTable = dbQuery.GetTable();

                foreach (DataRow Row in gUsersTable.Rows)
                {
                    ExtraBox_Item = Convert.ToInt32(Row["ExtraBox_Item"]);
                    BadgeBox_Item = Convert.ToInt32(Row["BadgeBox_Item"]);
                }
            }

            ItemData ItemData = null;
            if (!AkiledEnvironment.GetGame().GetItemManager().GetItem(ExtraBox_Item, out ItemData))
                return;

            int NbBadge = AkiledEnvironment.GetRandomNumber(1, 2);

            ItemData ItemDataBadge = null;
            if (!AkiledEnvironment.GetGame().GetItemManager().GetItem(BadgeBox_Item, out ItemDataBadge))
                return;

            List<Item> Items = ItemFactory.CreateMultipleItems(ItemData, roomUserByHabbo.GetClient().GetHabbo(), "", NbLot);
            {
                if (roomUserByHabbo.GetClient().GetHabbo().GetInventoryComponent().TryAddItem(PurchasedItem))
                {
                    roomUserByHabbo.GetClient().SendPacket(new FurniListNotificationComposer(PurchasedItem.Id, 1));
                }
            }