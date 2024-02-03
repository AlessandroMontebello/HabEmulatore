using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class HALCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            if ((double)Session.GetHabbo().last_hal > AkiledEnvironment.GetUnixTimestamp() - 60.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 60 secondi per utilizzare nuovamente il comando", 1);
                return;
            }


            if (Params.Length == 1)

            {
                Session.SendWhisper("Si prega di scrivere il link del collegamento e poi il messaggio.");
                return;
            }
            if (Params.Length == 2)

            {
                Session.SendWhisper("Per favore scrivi il messaggio che invierai dopo il link.");
                return;
            }
            string URL = Params[1];
            string Message = CommandManager.MergeParams(Params, 2);
            AkiledEnvironment.GetGame().GetClientManager().SendMessage(new RoomNotificationComposer("Messaggio del team amministrativo", "<font color = '#000000'><font>" + Message + " </font></font>\r\n <font color = '#008000'><font><b>" + Session.GetHabbo().Username, "</b></font></font>", URL, URL));
            Session.GetHabbo().last_hal = AkiledEnvironment.GetIUnixTimestamp();
        }
    }
}
