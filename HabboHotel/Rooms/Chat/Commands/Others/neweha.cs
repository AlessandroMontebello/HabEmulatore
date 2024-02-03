using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class neweha : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Session != null)
            {
                if (Room != null)
                {
                    if ((double)Session.GetHabbo().last_eha > AkiledEnvironment.GetUnixTimestamp() - 100.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
                    {
                        Session.SendWhisper("È necessario attendere 100 secondi per utilizzare nuovamente il comando", 1);
                        return;
                    }

                    if (Params.Length == 1)
                    {
                        Session.SendWhisper("Per favore scrivi il gioco che stai aprendo.", 1);
                        return;
                    }
                    string event_alert = (AkiledEnvironment.GetConfig().data["event_alert"]);
                    string Message = CommandManager.MergeParams(Params, 1);

                    string AlertMessage = "C'è un nuovo evento in corso proprio adesso! Se vuoi vincere <b>Diamanti o Punti VIP</b> partecipa subito. Questo è un gioco creato da <b> <font color=\"#2E9AFE\"> " + Session.GetHabbo().Username + "</font>.</b>" +
                    "\r\r" +
                    "Se vuoi partecipare, fai clic sul pulsante in basso <b>Vai alla sala eventi</b> e potrai partecipare lì.<br>Di cosa tratta questo evento?<br><font color='#FF0040'><b>"
                    + Message + "</b></font><br>Ti Aspettiamo!" +
                    "\r\n";


                    AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer(event_alert, "Comunicazione dallo Staff", AlertMessage, "Vai alla sala dell'evento", Session.GetHabbo().CurrentRoomId, ""), Session.Langue);

                    AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("gameopen", "L'utente: " + Session.GetHabbo().Username + " - Hai appena aperto un gioco/evento, fai attenzione al tuo turno.", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
                    AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertjuego", string.Format("Alerta Juego: {0}", Message));
                    Session.GetHabbo().last_eha = AkiledEnvironment.GetIUnixTimestamp();
                    return;

                }
            }
        }
    }
}

