using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class Fiestaalert : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            if ((double)Session.GetHabbo().last_dj > AkiledEnvironment.GetUnixTimestamp() - 60.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 60 secondi per utilizzare nuovamente il comando", 1);
                return;
            }

            string AlertMessage = "<i>¡Que Rumbiiitah Mijo!</i>" +
            "\r\r" +
            "L'utente <b> <font><font color=\"#58ACFA\">"
                 + Session.GetHabbo().Username + "</font></font></b> È impazzito e invita l'intero albergo alla grande festa. <b><br><br>Vieni a goderti la bella atmosfera e ad ascoltare la musica dei nostri migliori DJ, così animeranno l'evento e gli daranno un tocco di follia.<b><br><br><font><font color=\"# DB0003 \">Te lo perderai? CI SARANNO PREMI, LOTTERIE E MOLTO ALTRO!</font></font>";

            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer("partyalert", "Comunicazione dallo Staff", AlertMessage, "Vai alla festa!", Session.GetHabbo().CurrentRoomId, ""), Session.Langue);

            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertparty", string.Format("Alerta Fiesta: {0}", AlertMessage));
            Session.GetHabbo().last_dj = AkiledEnvironment.GetIUnixTimestamp();
            return;

        }
    }
}
