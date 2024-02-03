using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class hotelalert : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Si prega di scrivere il messaggio da inviare");
                return;
            }

            string Message = CommandManager.MergeParams(Params, 1);
            string hotelalert_alert = (AkiledEnvironment.GetConfig().data["hotelalert_alert"]);
            string AlertMessage = "<i>Messaggio dallo Staff!</i>" + "\r\r" + "L'utente <b> <font><font color=\"#58ACFA\">" + Session.GetHabbo().Username + "</font></font></b> fa il seguente annuncio. <br><br>" + Message + "</b> <br><br><font><font color=\"#DB0003\">Grazie per l'attenzione.</font></font>";


            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer(hotelalert_alert, "Annuncio dello staff", AlertMessage, "Ricevuto !", 0, ""), Session.Langue);

            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "staffalert", string.Format("Staff Alert: {0}", AlertMessage));

            return;



        }
    }
}
