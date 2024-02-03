using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class DjAlert : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            if ((double)Session.GetHabbo().last_dj > AkiledEnvironment.GetUnixTimestamp() - 60.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 60 secondi per utilizzare nuovamente il comando", 1);
                return;
            }

            if (Params.Length == 1)
            {
                Session.SendWhisper("Si prega di scrivere il nome del DJ per inviare l'avviso");
                return;
            }

            string Message = CommandManager.MergeParams(Params, 1);
            AkiledEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("LOCOSON", "DJ " + Message + " sta trasmettendo in diretta! Sintonizzati subito su RadioFM e goditelo al massimo.", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
            Session.GetHabbo().last_dj = AkiledEnvironment.GetIUnixTimestamp();
            return;
        }
    }
}
