using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class FacebookCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci il messaggio per inviarlo.");
                return;
            }
            string facebook_alert = (AkiledEnvironment.GetConfig().data["facebook_alert"]);
            string URL = (AkiledEnvironment.GetConfig().data["facebook_url"]);

            string Message = CommandManager.MergeParams(Params, 1);

            string AlertMessage = "<i>Nuovo Concorso su Facebook!</i>" +
            "\r\r" +
            "C'è un nuovo concorso su Facebook gestito da <b>" + Session.GetHabbo().Username + ".</b>" +
            "\r\r" +
            "<b>Di che si tratta?</b><br>" + Message + "<br><br><font><font color=\"#DB0003\">Per accedere alla pagina Facebook cliccare su Vai a Facebook.</font></font>";

            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer(facebook_alert, "Comunicazione dallo Staff", AlertMessage, "Vai a Facebook", 0, URL), Session.Langue);

            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertfacebook", string.Format("Alerta Facebook: {0}", AlertMessage));

            return;
        }
    }
}
