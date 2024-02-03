using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class Oleadabig : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            string oleada_alert = (AkiledEnvironment.GetConfig().data["oleada_alert"]);

            if ((double)Session.GetHabbo().last_oleada > AkiledEnvironment.GetUnixTimestamp() - 60.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 1 minuto per utilizzare nuovamente il comando", 1);
                return;
            }
            string AlertMessage = "C'è una nuova ondata di pubblicità! Se vuoi vincere <b>diversi premi</b> per la partecipazione, vai nella sala pubblicità.<br>Chi ha aperto l'onda? <b> <font color=\"#2E9AFE\"> " + Session.GetHabbo().Username + "</font>.</b>" +

            "<br>Di cosa tratta questo evento?<br><font color='#084B8A'><b>Prova a seguire le istruzioni delle waveguide per partecipare e vincere il tuo premio!</b></font>< br>Ti aspettiamo!" +
            "\r\n";

            AkiledEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("hoteloleada", "C'è una nuova ondata di pubblicità! Per guadagnare vari premi per la partecipazione, vai nella sala pubblicitaria. - Clicca qui.", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
            Session.GetHabbo().last_oleada = AkiledEnvironment.GetIUnixTimestamp();
            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer(oleada_alert, "Comunicazione dallo Staff", AlertMessage, "Portami li!", Session.GetHabbo().CurrentRoomId, ""), Session.Langue);


            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertoleada", string.Format("Alerta de Oleada: {0}", AlertMessage));

        }
    }
}
