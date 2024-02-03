using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class RPALERT : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            if ((double)Session.GetHabbo().last_rp > AkiledEnvironment.GetUnixTimestamp() - 100.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("Debes esperar 100 segundos, para volver a usar el comando", 1);
                return;
            }

            string roleplay_alert = (AkiledEnvironment.GetConfig().data["roleplay_alert"]);
            string AlertMessage = "<i>NUOVO EVENTO ROLEPLAY!</i>" +
            "\r\r" +
            "L'utente <b><font color=\"#58ACFA\">"
                 + Session.GetHabbo().Username + "</font></font></b> sta promuovendo un nuovo evento, <b><br>È un gioco di tipo <b>RolePlay</b> in cui devi dimostrare le tue abilità speciali per vincere l'evento.<br><br>";

            AkiledEnvironment.GetGame().GetClientWebManager().SendMessage(new NotifAlertComposer(roleplay_alert, "Comunicazione dallo Staff", AlertMessage, "Vai alla stanza RP", Session.GetHabbo().CurrentRoomId, ""), Session.Langue);

            AkiledEnvironment.GetGame().GetModerationManager().LogStaffEntry(Session.GetHabbo().Id, Session.GetHabbo().Username, 0, string.Empty, "alertrp", string.Format("Alerta RP: {0}", AlertMessage));
            Session.GetHabbo().last_rp = AkiledEnvironment.GetIUnixTimestamp();
            return;

        }
    }
}
