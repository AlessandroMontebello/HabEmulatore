using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class InstagramAlert : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            string instagram_alert = (AkiledEnvironment.GetConfig().data["instagram_alert"]);
            string URL = (AkiledEnvironment.GetConfig().data["instagram_url"]);

            string AlertMessage = "<i>Nuovo concorso su Instagram!</i>" +
            "\r\r" +
            "Su Instagram è indetto un nuovo concorso per, <b>" + Session.GetHabbo().Username + ".</b>" +
            "\r\r" +
            "Vai sul nostro Instagram ufficiale, Concorso e vinci, abbiamo sorprese per te<i>Puoi ottenere rari, badge, diamanti," + name_monedaoficial + " e tanto altro ! </i>" +
            "\r\n" +
            "\r\n- <b>Att: " + Session.GetHabbo().Username + ".</b>\r\n";

            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer(instagram_alert, "Comunicazione dallo Staff", AlertMessage, "Vai a Instagram", 0, URL), Session.Langue);

            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertinstagram", string.Format("Alerta Instagram: {0}", AlertMessage));

            return;
        }
    }
}
