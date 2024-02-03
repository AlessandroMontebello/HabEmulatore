using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;using Akiled.HabboHotel.Users;
namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd{    class Superban : IChatCommand    {        public  void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)        {            if (Params.Length < 2)                return;            GameClient clientByUsername = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            Habbo Habbo = AkiledEnvironment.GetHabboByUsername(Params[1]);            if (clientByUsername == null || clientByUsername.GetHabbo() == null)
            {                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.usernotfound", Session.Langue));
                return;
            }            if (Session.Langue != clientByUsername.Langue)
            {
                UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue(string.Format("cmd.authorized.langue.user", clientByUsername.Langue), Session.Langue));
                return;
            }            if (clientByUsername.GetHabbo().Rank >= Session.GetHabbo().Rank)            {                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("action.notallowed", Session.Langue));                 AkiledEnvironment.GetGame().GetClientManager().BanUserAsync(Session, "Robot", (double)788922000, "Il tuo account è stato bannato per motivi di sicurezza", false, false);            }            else            {                int num = 788922000;
                if (Params.Length == 3)
                    int.TryParse(Params[2], out num);                if (num <= 600)                    Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("ban.toolesstime", Session.Langue));                else                {                    string Raison = "";
                    if (Params.Length > 2)
                    {
                        for (int i = 2; i < Params.Length; i++)
                        {
                            Raison += Params[i] + " ";
                        }
                    }
                    else
                    {

                        Raison = "Non è stato specificato alcun motivo.";
                    }

                    string Username = Habbo.Username;
                     AkiledEnvironment.GetGame().GetClientManager().BanUserAsync(clientByUsername, Session.GetHabbo().Username, (double)num, Raison, false, false).ConfigureAwait(false);                    Session.Antipub(Raison, "<CMD>");
                    AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("baneo", "L'utente: " + Username + " è stato bannato, si prega di verificare il motivo del ban, per evitare malintesi"));
                    Session.SendWhisper("Eccellente, hai bannato l'IP dell'utente '" + Username + "' per il motivo: '" + Raison + "'!");                }                if (clientByUsername.GetHabbo().Rank > 8 && Session.GetHabbo().Rank < 12)
                {
                     AkiledEnvironment.GetGame().GetClientManager().BanUserAsync(Session, "Robot", (double)788922000, "Il tuo account è stato bannato per motivi di sicurezza", false, false).ConfigureAwait(false);
                }            }        }    }}