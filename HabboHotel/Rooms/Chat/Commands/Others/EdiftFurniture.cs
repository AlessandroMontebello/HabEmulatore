using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class EdiftFurniture : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            RoomUser RUser = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            List<Item> Items = Room.GetGameMap().GetRoomItemForSquare(RUser.X, RUser.Y);
            if (Params.Length == 1 || Params[1] == "cmd")
            {
                Session.SendPacket(new NuxAlertComposer("habbopages/itemutilidad.txt"));
                return;
            }
            String Type = Params[1].ToLower();
            int numeroint = 0, FurnitureID = 0;
            String FurnitureName = "";
            String inhe = "";
            DataRow Item = null;
            String opcion = "";
            switch (Type)
            {
                case "newprice":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("INSERT catalog_rares_history VALUES ('','230230','" + IItem.BaseItem + "','" + FurnitureName + "','" + FurnitureName + "','0','0','" + numeroint + "','0','0','0','0','0','0','0',CURRENT_TIMESTAMP,'','" + Session.GetHabbo().Username + "','','','','0','" + numeroint + "');");
                                    dbClient.RunQuery("UPDATE `catalog_rares_history` SET `last_diamonds` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' and rarecat_principal=1 LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Nuovo prezzo raro sul sito web, Item: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "setrare":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("INSERT catalog_rares_history VALUES ('','260260','" + IItem.BaseItem + "','" + FurnitureName + "','" + FurnitureName + "','0','0','" + numeroint + "','0','0','0','0','0','0','0',CURRENT_TIMESTAMP,'','" + Session.GetHabbo().Username + "','','','','0','" + numeroint + "');");
                                    //dbClient.RunQuery("UPDATE `catalog_rares_history` SET `last_diamonds` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' and rarecat_principal=1 LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Nuovo prezzo raro sul sito web, Item: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;

                case "pkakas":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_items` SET `cost_diamonds` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Hai modificato il prezzo in " + name_monedaoficial + " del'Item: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }

                    break;
                case "pdiamonds":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_items` SET `cost_pixels` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Hai modificato il prezzo in diamanti dell'Articolo: " + FurnitureID + " modificato con successo (Importo inserito:" + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }

                    break;
                case "pcredits":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_items` SET `cost_credits` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Hai modificato il prezzo in crediti dell'Articolo: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "preciokk":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_rares_history` SET `cost_diamonds` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' and `principal`=1 LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Hai modificato il prezzo del raro sul sito web del'Item:" + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "preciokkltd":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_rares_history` SET `old_cost_diamonds` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Has cambiado el precio del rare en la web del Item: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "setlastmonth":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_rares_history` SET `last_month` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' and `principal`=1 LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Hai inserito questo raro come oggetto principale: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "setcat":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_rares_history` SET `categoria` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' and `principal`=1 LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Hai cambiato la categoria rara sul sito web del'Item: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "ltdstack":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_items` SET `limited_stack` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("La quantità di LTD disponibile per la vendita per l'Articolo è cambiata: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "ltdsell":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_items` SET `limited_sells` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("L'importo di LTD venduto per l'articolo è cambiato: " + FurnitureID + " modificato con successo (Importo inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "setpage":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_items` SET `page_id` = '" + numeroint + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Cambio de pagina del'Item: " + FurnitureID + " modificato correttamente (ID pagina inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;

                case "setreceta":
                    {
                        try
                        {
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT item_name FROM furniture WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureName = Convert.ToString(Item[0]);
                                    dbClient.RunQuery("INSERT INTO crafting_items VALUES ('" + FurnitureName + "');");
                                }
                                UserRoom.SendWhisperChat("L'oggetto è stato aggiunto alle ricette di creazione");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "countltd":
                    {
                        foreach (Item IItem in Items.ToList())
                        {
                            if (IItem == null)
                                continue;
                            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                            {
                                dbClient.SetQuery("SELECT COUNT(*) FROM items_limited WHERE base_item = '" + IItem.BaseItem + "' LIMIT 1");
                                int unidades = dbClient.GetInteger();
                                FurnitureID = unidades;
                            }
                            UserRoom.SendWhisperChat("Nell'hotel ci sono: " + FurnitureID + ", unità di questo furno.");
                        }
                    }
                    break;
                case "count":
                    {
                        foreach (Item IItem in Items.ToList())
                        {
                            if (IItem == null)
                                continue;
                            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                            {
                                dbClient.SetQuery("SELECT COUNT(*) FROM items WHERE base_item = '" + IItem.BaseItem + "' LIMIT 1");
                                int unidades = dbClient.GetInteger();
                                FurnitureID = unidades;
                            }
                            UserRoom.SendWhisperChat("Nell'hotel ci sono: " + FurnitureID + ", unità di questo furno.");
                        }
                    }
                    break;
                case "id":
                    {
                        foreach (Item IItem in Items.ToList())
                        {
                            if (IItem == null)
                                continue;
                            using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                            {
                                dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                Item = dbClient.GetRow();
                                FurnitureID = Convert.ToInt32(Item[0]);
                            }
                            UserRoom.SendWhisperChat("L'ID dell'articolo è: (" + FurnitureID + ")");
                        }
                    }
                    break;
                case "width":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `width` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Altitudine articolo: " + FurnitureID + "modificato correttamente (valore di altitudine immesso: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "length":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `length` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Longitudine articolo: " + FurnitureID + " modificato correttamente (valore di longitudine immesso: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "effect":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `effect_id` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Effetto del furno: " + FurnitureID + "  modificato correttamente (valore dell'effetto immesso: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "wired":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `wired_id` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Wired ID del Item: " + FurnitureID + " modificato correttamente (valore ID Wired inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "height":
                    {
                        try
                        {
                            inhe = Convert.ToString(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `stack_height` = '" + inhe + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Altezza articolo: " + FurnitureID + " modificato con successo (valore di altezza inserito: " + inhe.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;


                case "heightajust":
                    {
                        try
                        {
                            inhe = Convert.ToString(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `height_adjustable` = '" + inhe + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Altezza regolabile dell'articolo: " + FurnitureID + " modificato con successo (valore dell'altezza regolabile inserito: " + inhe.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "interactioncount":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `interaction_modes_count` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Numero di interazioni con l'articolo: " + FurnitureID + " modificato con successo (valore inserito: " + numeroint.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "vendingid":
                    {
                        try
                        {
                            inhe = Convert.ToString(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `vending_ids` = '" + inhe.ToString() + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Numero di interazioni con l'articolo: " + FurnitureID + " modificato con successo (valore inserito: " + inhe.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore (inserire numeri validi)");
                        }
                    }
                    break;
                case "cansit":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                UserRoom.SendWhisperChat("Inserisci un'opzione valida (sì/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `can_sit` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("can_sit del Item: " + FurnitureID + " modificato con successo");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore");
                        }
                    }
                    break;
                case "canstack":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                UserRoom.SendWhisperChat("Inserisci un'opzione valida (sì/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `can_stack` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("can_stack del Item: " + FurnitureID + " modificato con successo");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "canwalk":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                UserRoom.SendWhisperChat("Inserisci un'opzione valida (sì/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `is_walkable` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("can_walk del Item: " + FurnitureID + " modificato con successo");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;

                case "cantrade":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                UserRoom.SendWhisperChat("Inserisci un'opzione valida (sì/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `allow_trade` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Scambio dell'articolo: " + FurnitureID + " modificato con successo");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "mercadillo":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                UserRoom.SendWhisperChat("Inserisci un'opzione valida (sì/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `is_rare` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Opzione di vendita al mercato degli articoli: " + FurnitureID + " modificato con successo");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "interaction":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `interaction_type` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Interazione con l'oggetto: " + FurnitureID + " modificato con successo. (Valore inserito: " + opcion + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "furniname":
                    {
                        try
                        {
                            inhe = Convert.ToString(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `public_name` = '" + inhe.ToString() + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Nome del Item: " + FurnitureID + " modificato con successo. (Valore inserito: " + inhe.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "cataname":
                    {
                        try
                        {
                            inhe = Convert.ToString(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_items` SET `catalog_name` = '" + inhe.ToString() + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Nome dell'articolo nel cata: " + FurnitureID + " modificato con successo. (Valore inserito: " + inhe.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "catanameweb":
                    {
                        try
                        {
                            inhe = Convert.ToString(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_rares_history` SET `catalog_nameweb` = '" + inhe.ToString() + "' WHERE `item_id` = '" + FurnitureID + "'  LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Nome dell'articolo sul cataweb: " + FurnitureID + " modificato con successo. (Valore inserito: " + inhe.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                case "catanameitem":
                    {
                        try
                        {
                            inhe = Convert.ToString(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.GetRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `catalog_rares_history` SET `catalog_name` = '" + inhe.ToString() + "' WHERE `item_id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                UserRoom.SendWhisperChat("Nome dell'articolo sul cataweb: " + FurnitureID + " modificato con successo. (Valore inserito: " + inhe.ToString() + ")");
                            }
                            AkiledEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Si è verificato un errore.");
                        }
                    }
                    break;
                default:
                    {
                        UserRoom.SendWhisperChat("L'opzione inserita non esiste, per conoscere le opzioni dire :item cmd");
                        return;
                    }

            }

        }
    }
}
