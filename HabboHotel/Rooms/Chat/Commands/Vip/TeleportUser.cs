using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    internal class TeleportUser : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Session.GetHabbo().CurrentRoom == null)
                return;
            if (!Room.CheckRights(Session, true))
            {
                Session.SendWhisper("Spiacenti, solo il proprietario della stanza può eseguire questo comando.", 34);
            }
            else
            {
                RoomUser roomUserByHabboId = AkiledEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId).GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
                if (roomUserByHabboId == null)
                    return;
                roomUserByHabboId.TeleportEnabled = !roomUserByHabboId.TeleportEnabled;
                if (roomUserByHabboId.TeleportEnabled)
                    Session.SendWhisper("Hai attivato il teletrasporto in tutta la stanza.", 34);
                else
                    Session.SendWhisper("Hai disabilitato il teletrasporto in tutta la stanza.", 34);
            }
        }
    }
}
