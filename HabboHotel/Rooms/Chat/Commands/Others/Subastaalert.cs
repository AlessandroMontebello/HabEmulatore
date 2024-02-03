using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class Subastaalert : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            string subasta_alert = (AkiledEnvironment.GetConfig().data["subasta_alert"]);
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            string AlertMessage = "L'utenete <b> <font><font color=\"#12adfb\">"
                 + Session.GetHabbo().Username + "</font></font></b> ha aperto l'asta delle rarità <b><br>Vieni a fare un'offerta per i rari unici offerti dallo <b>Staff</b>, che hanno un prezzo iniziale in " + name_monedaoficial + " e devi offrire solo per l'importo che che hai nel tuo portafoglio " + name_monedaoficial + ".<br><br><font><font color=\"#DB0003\">Se non hai " + name_monedaoficial + " sufficienti per l'offerta, ignora questo avviso e continua a fare le tue cose, qualsiasi tipo di sabotaggio non è accettato, altrimenti verrai penalizzato.</font></font>" +
           "\r\n";

            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer(subasta_alert, "Comunicazione dallo Staff", AlertMessage, "Vai alla Subasta", Session.GetHabbo().CurrentRoomId, ""), Session.Langue);


            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertsubasta", string.Format("Alerta de Subasta: {0}", AlertMessage));

        }
    }
}
