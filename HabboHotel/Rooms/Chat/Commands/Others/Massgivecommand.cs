using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using System.Linq;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class Massgivecommand : IChatCommand
    {

        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            if (Params.Length == 1)
            {
                Session.SendWhisper("È necessario inserire il tipo di valuta: crediti, diamanti, " + name_monedaoficial + ".", 34);
                return;
            }

            string UpdateVal = Params[1];
            switch (UpdateVal.ToLower())
            {
                case "credits":
                case "crediti":
                    {
                        if (!Session.GetHabbo().HasFuse("give_command"))
                        {
                            Session.SendWhisper("Spiacenti, non disponi delle autorizzazioni necessarie per inviare monete!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                            {
                                foreach (GameClient Target in AkiledEnvironment.GetGame().GetClientManager().GetClients.ToList())
                                {
                                    if (Target == null || Target.GetHabbo() == null || Target.GetHabbo().Username == Session.GetHabbo().Username)
                                        continue;

                                    Target.GetHabbo().Credits = Target.GetHabbo().Credits += Amount;
                                    Target.SendMessage(new CreditBalanceComposer(Target.GetHabbo().Credits));
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("cred", "" + Session.GetHabbo().Username + " ti ho appena mandato " + Amount + " crediti.", ""));

                                }

                                break;
                            }
                            else
                            {
                                Session.SendWhisper("È possibile inserire solo quantità numeriche.", 34);
                                break;
                            }
                        }
                    }

                case "smerald":
                case "smeraldi":
                case "Smeraldi":
                case "sm":
                case "smeraldes":
                    {
                        if (!Session.GetHabbo().HasFuse("give_command"))
                        {
                            Session.SendWhisper("Spiacenti, non disponi delle autorizzazioni necessarie per inviare monete!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                            {
                                foreach (GameClient Target in AkiledEnvironment.GetGame().GetClientManager().GetClients.ToList())
                                {
                                    if (Target == null || Target.GetHabbo() == null || Target.GetHabbo().Username == Session.GetHabbo().Username)
                                        continue;

                                    Target.GetHabbo().Duckets += Amount;
                                    Target.SendMessage(new HabboActivityPointNotificationComposer(Target.GetHabbo().Duckets, Amount));
                                    Target.SendMessage(RoomNotificationComposer.SendBubble("senddiamonds", "" + Session.GetHabbo().Username + " ti ho appena mandato " + Amount + " smeraldi.", ""));
                                }

                                break;
                            }
                            else
                            {
                                Session.SendWhisper("È possibile inserire solo quantità numeriche.", 34);
                                break;
                            }
                        }
                    }

                case "diamanti":
                case "diamantini":
                case "Dia":
                case "diamantino":
                    {
                        if (!Session.GetHabbo().HasFuse("give_command"))
                        {
                            Session.SendWhisper("Spiacenti, non disponi delle autorizzazioni necessarie per inviare monete!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[2], out Amount))
                            {

                                foreach (GameClient client in AkiledEnvironment.GetGame().GetClientManager().GetClients.ToList())
                                {
                                    client.GetHabbo().AkiledPoints += Amount;
                                    client.SendPacket(new HabboActivityPointNotificationComposer(client.GetHabbo().AkiledPoints, Amount, 105));

                                    using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                        queryreactor.RunQuery("UPDATE users SET vip_points = vip_points + " + Amount + " WHERE id = " + client.GetHabbo().Id + " LIMIT 1");

                                    if (client.GetHabbo().Id != Session.GetHabbo().Id)
                                        client.SendNotification(Session.GetHabbo().Username + " ti ha inviato " + Amount.ToString() + " " + name_monedaoficial + "!");
                                }
                                Session.SendWhisper("Hai inviato " + Amount + " " + name_monedaoficial + " all'intero hotel online!");
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Sólo puedes introducir cantidades numerales.", 34);
                                break;
                            }
                        }
                    }

                default:
                    Session.SendWhisper("Attenzione '" + UpdateVal + "' Non è una valuta valida!", 34);
                    break;
            }
        }
    }
}
