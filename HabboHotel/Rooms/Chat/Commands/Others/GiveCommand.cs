using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class GiveCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            string name_monedaoficial2 = (AkiledEnvironment.GetConfig().data["name_monedaoficial2"]);
            if (Params.Length == 1)
            {
                Session.SendWhisper("Per favore inserisci! (Crediti, Smeraldi, Diamanti)");
                return;
            }

            GameClient Target = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("Spiacenti, questo utente non è stato trovato!");
                return;
            }

            string UpdateVal = Params[2];
            switch (UpdateVal.ToLower())
            {
                case "creditos":
                case "credito":
                case "crediti":
                    {
                        if (!Session.GetHabbo().HasFuse("give_command"))
                        {
                            Session.SendWhisper("Spiacenti, non disponi delle autorizzazioni necessarie per utilizzare questo comando!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[3], out Amount))
                            {
                                Target.GetHabbo().Credits = Target.GetHabbo().Credits += Amount;
                                Target.SendPacket(new CreditBalanceComposer(Target.GetHabbo().Credits));

                                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                    Target.SendNotification(Session.GetHabbo().Username + " ti ha inviato " + Amount.ToString() + " Credito/i!");
                                Session.SendWhisper("Hai inviato " + Amount + " Credito/i a " + Target.GetHabbo().Username + "!");
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Ops, quantità solo in numeri..");
                                break;
                            }
                        }
                    }

                case "esmeralda":
                case "esmeraldas":
                case "esme":
                case "smeraldi":
                    {
                        if (!Session.GetHabbo().HasFuse("give_command"))
                        {
                            Session.SendWhisper("Spiacenti, non disponi dei permessi necessari per inviare Smeraldi!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[3], out Amount))
                            {
                                Target.GetHabbo().Duckets += Amount;
                                Target.SendPacket(new HabboActivityPointNotificationComposer(Target.GetHabbo().Duckets, Amount));

                                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                    Target.SendNotification(Session.GetHabbo().Username + " ti ha inviato " + Amount.ToString() + " " + name_monedaoficial2 + "!");
                                Session.SendWhisper("Hai inviato " + Amount + " " + name_monedaoficial2 + " a " + Target.GetHabbo().Username + "!");
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Ops, quantità solo in numeri..");
                                break;
                            }
                        }
                    }

                case "diamanti":
                case "diamante":

                    {
                        if (!Session.GetHabbo().HasFuse("give_command"))
                        {
                            Session.SendWhisper("Spiacenti, non disponi dei permessi necessari per inviare Smeraldi!");
                            break;
                        }
                        else
                        {
                            int Amount;
                            if (int.TryParse(Params[3], out Amount))
                            {

                                Target.GetHabbo().AkiledPoints += Amount;
                                Target.SendPacket(new HabboActivityPointNotificationComposer(Target.GetHabbo().AkiledPoints, Amount, 105));

                                using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                                    queryreactor.RunQuery("UPDATE users SET vip_points = vip_points + " + Amount + " WHERE id = " + Target.GetHabbo().Id + " LIMIT 1");

                                if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                                    Target.SendNotification(Session.GetHabbo().Username + " ti ha inviato " + Amount.ToString() + " " + name_monedaoficial + "!");
                                Session.SendWhisper("Hai inviato " + Amount + " " + name_monedaoficial + " a " + Target.GetHabbo().Username + "!");
                                break;
                            }
                            else
                            {
                                Session.SendWhisper("Ops, quantità solo in numeri..!");
                                break;
                            }
                        }
                    }

                default:
                    Session.SendWhisper("'" + UpdateVal + "' Non è una valuta valida.");
                    break;
            }
        }
    }
}
