using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class DadosAlerts : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if ((double)Session.GetHabbo().last_dadosalert > AkiledEnvironment.GetUnixTimestamp() - 60.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 1 minuto per utilizzare nuovamente il comando", 1);
                return;
            }
            AkiledEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("DADOON", "I dadi sono stati aperti da " + Session.GetHabbo().Username + " Vieni a tentare la fortuna scommettendo le tue carte rare o lingotti.", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
            Session.GetHabbo().last_dadosalert = AkiledEnvironment.GetIUnixTimestamp();
            return;

        }
    }
}
