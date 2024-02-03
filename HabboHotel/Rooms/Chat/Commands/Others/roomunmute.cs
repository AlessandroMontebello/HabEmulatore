using Akiled.HabboHotel.GameClients;
using System.Collections.Generic;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class roomunmute : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (!Room.RoomMuted)
            {
                Session.SendWhisper("Il muto di questa stanza non è disattivato.");
                return;
            }

            Room.RoomMuted = false;

            List<RoomUser> RoomUsers = Room.GetRoomUserManager().GetRoomUsers();
            if (RoomUsers.Count > 0)
            {
                foreach (RoomUser User in RoomUsers)
                {
                    if (User == null || User.GetClient() == null || User.GetClient().GetHabbo() == null || User.GetClient().GetHabbo().Username == Session.GetHabbo().Username)
                        continue;

                    User.GetClient().SendWhisper("Il muto di questa stanza è stato riattivato, puoi parlare di nuovo normalmente.");
                }

            }
        }
    }
}
