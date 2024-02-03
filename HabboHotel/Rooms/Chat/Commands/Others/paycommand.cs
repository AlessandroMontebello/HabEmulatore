using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class paycommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Spiacenti, devi inserire un nome utente!");
                return;
            }

            if ((double)Session.GetHabbo().last_pay > AkiledEnvironment.GetUnixTimestamp() - 30.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 30 secondi per utilizzare nuovamente il comando", 1);
                return;
            }

            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            string icon_monedaoficial = (AkiledEnvironment.GetConfig().data["icon_monedaoficial"]);
            if (Params.Length == 2)
            {
                Session.SendWhisper("Spiacenti, inserisci il tipo di valuta che desideri trasferire. (crediti/diamanti/" + name_monedaoficial + ")");
                return;
            }

            if (Params.Length == 3)
            {
                Session.SendWhisper("Spiacenti, inserisci l'importo che desideri inviare.");
                return;
            }
            else if (Params[3].Contains("-"))
            {
                Session.SendWhisper("Ops, usalo di nuovo e rimarrai senza un account! (E:3)");
                return;
            }

            GameClient TargetClient = null;
            TargetClient = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendWhisper("Spiacenti, l'utente non ha effettuato l'accesso!");
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Spiacenti, non puoi utilizzare questo comando con il tuo stesso account!");
                return;
            }

            int Amount = Convert.ToInt32(Params[3]);

            var Options = Params[2];
            switch (Options.ToLower())
            {
                case "credits":
                case "creditos":
                case "c":
                case "crediti":
                    {
                        if (Session.GetHabbo().Credits < Amount)
                        {
                            Session.SendWhisper("Spiacenti, non hai abbastanza crediti! (E:1)");
                            return;
                        }

                        if (Amount > Session.GetHabbo().Credits)
                        {
                            Session.SendWhisper("Spiacenti, non hai abbastanza crediti! (E:2)");
                            return;
                        }

                        Session.GetHabbo().Credits -= Amount;
                        Session.SendPacket(new CreditBalanceComposer(Session.GetHabbo().Credits));

                        TargetClient.GetHabbo().Credits += Amount;
                        TargetClient.SendPacket(new CreditBalanceComposer(TargetClient.GetHabbo().Credits));

                        Session.SendWhisper("Hai inviato con successo " + Amount + " crediti dal tuo account all'account di " + TargetClient.GetHabbo().Username + " !");
                        TargetClient.SendWhisper("Hai ricevuto dal utente " + Session.GetHabbo().Username + " " + Amount + " crediti!");

                        Session.GetHabbo().last_pay = AkiledEnvironment.GetIUnixTimestamp();
                        break;
                    }
                case "diamantes":
                case "Diamantes":
                case "dm":
                case "diamonds":
                    {
                        if (Session.GetHabbo().Duckets < Amount)
                        {
                            Session.SendWhisper("Spiacenti, non hai abbastanza diamanti! (E:1)", 34);
                            return;
                        }

                        if (Amount > Session.GetHabbo().Duckets)
                        {
                            Session.SendWhisper("Spiacenti, non hai abbastanza diamanti! (E:2)", 34);
                            return;
                        }

                        Session.GetHabbo().Duckets -= Amount;
                        Session.SendPacket(new HabboActivityPointNotificationComposer(Session.GetHabbo().Duckets, Amount));

                        TargetClient.GetHabbo().Duckets += Amount;
                        TargetClient.SendPacket(new HabboActivityPointNotificationComposer(TargetClient.GetHabbo().Duckets, Amount));

                        Session.SendWhisper("Hai inviato con successo " + Amount + " diamanti dal tuo account all'account di " + TargetClient.GetHabbo().Username + " !", 34);
                        TargetClient.SendWhisper("Hai ricevuto dal utente " + Session.GetHabbo().Username + " " + Amount + " diamanti!", 34);
                        AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("senddiamonds", "L'utente: " + Session.GetHabbo().Username + " ho appena inviato l'importo di : " + Amount + " Diamantes al usuario: " + TargetClient.GetHabbo().Username + ", Bisogna stare attenti alle truffe o ai raggiri."));

                        foreach (GameClient Client in AkiledEnvironment.GetGame().GetClientManager().GetStaffUsers())
                        {
                            if (Client == null || Client.GetHabbo() == null)
                                continue;

                            string type = "<PAY>";
                            Client.GetHabbo().SendWebPacket(new AddChatlogsComposer(Session.GetHabbo().Id, Session.GetHabbo().Username, type + " ho appena inviato l'importo di : " + Amount + " Diamanti al'utente: " + TargetClient.GetHabbo().Username + ", Bisogna stare attenti alle truffe o ai raggiri."));
                        }

                        Session.GetHabbo().last_pay = AkiledEnvironment.GetIUnixTimestamp();
                        break;
                    }
                case "diamanti":
                case "diamante":
                case "Dim":
                case "diamantini":

                    {
                        if (Session.GetHabbo().AkiledPoints < Amount)
                        {
                            Session.SendWhisper("Spiacenti, non ne hai abbastanza" + name_monedaoficial + "! (E:1)");
                            return;
                        }

                        if (Amount > Session.GetHabbo().AkiledPoints)
                        {
                            Session.SendWhisper("Spiacenti, non ne hai abbastanza" + name_monedaoficial + "! (E:2)");
                            return;
                        }

                        Session.GetHabbo().AkiledPoints -= Amount;
                        Session.SendPacket(new HabboActivityPointNotificationComposer(Session.GetHabbo().AkiledPoints, 0, 105));

                        using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                            queryreactor.RunQuery("UPDATE users SET vip_points = vip_points - " + Amount + " WHERE id = " + Session.GetHabbo().Id + " LIMIT 1");

                        TargetClient.GetHabbo().AkiledPoints += Amount;
                        TargetClient.SendPacket(new HabboActivityPointNotificationComposer(TargetClient.GetHabbo().AkiledPoints, Amount, 105));

                        using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                            queryreactor.RunQuery("UPDATE users SET vip_points = vip_points + " + Amount + " WHERE id = " + TargetClient.GetHabbo().Id + " LIMIT 1");

                        Session.SendWhisper("Hai inviato con successo " + Amount + " " + name_monedaoficial + " dal tuo account all'account " + TargetClient.GetHabbo().Username + " !");
                        TargetClient.SendWhisper("Hai ricevuto dal utente " + Session.GetHabbo().Username + " " + Amount + " " + name_monedaoficial + "!");
                        AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble(icon_monedaoficial, "L'utente: " + Session.GetHabbo().Username + " ho appena inviato l'importo di : " + Amount + " " + name_monedaoficial + " al utente: " + TargetClient.GetHabbo().Username + ", Bisogna stare attenti alle truffe o ai raggiri."));

                        foreach (GameClient Client in AkiledEnvironment.GetGame().GetClientManager().GetStaffUsers())
                        {
                            if (Client == null || Client.GetHabbo() == null)
                                continue;

                            string type = "<PAY>";
                            Client.GetHabbo().SendWebPacket(new AddChatlogsComposer(Session.GetHabbo().Id, Session.GetHabbo().Username, type + " ho appena inviato l'importo di : " + Amount + " " + name_monedaoficial + " al utente: " + TargetClient.GetHabbo().Username + ", Bisogna stare attenti alle truffe o ai raggiri."));
                        }

                        Session.GetHabbo().last_pay = AkiledEnvironment.GetIUnixTimestamp();
                        break;

                    }
                default:
                    {
                        Session.SendWhisper("Spiacenti, questa opzione non esiste! Puoi inviare solo: (crediti/diamanti/" + name_monedaoficial + ")");
                        break;
                    }
            }
        }
    }
}