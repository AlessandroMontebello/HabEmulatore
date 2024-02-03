using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class CDAALERT : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            string AlertMessage = "<i>Apri il Centro assistenza!</i>" + "\r\r" + "L'utente <b> <font><font color=\"#58ACFA\">" + Session.GetHabbo().Username + "</font></font></b> Il Centro assistenza è stato aperto <b><br>Vieni a rispondere alle tue domande sull'hotel con le nostre <b>Guide</b> preparate a rispondere a quasi tutti i tipi di domande, purché non siano domande scherzose.<br><br><font><font color =\\ \"#DB0003\\\">Se una guida non riesce a risolvere la tua domanda, rivolgiti a un supervisore per chiarire la risposta.</font></font>";

            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer("cdaalert", "Comunicazione dallo Staff", AlertMessage, "Vai nella stanza del CDA", Session.GetHabbo().CurrentRoomId, ""), Session.Langue);

            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertcda", string.Format("Alerta CDA: {0}", AlertMessage));

            return;

        }
    }
}
