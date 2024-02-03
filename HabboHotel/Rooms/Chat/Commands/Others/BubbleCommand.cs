﻿using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms.Chat.Styles;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class BubbleCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser roomuser = TargetRoom.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (roomuser == null)
                return;

            if (Params.Length == 1)
            {
                Session.SendWhisper("Spiacenti, non hai inserito l'ID");
                return;
            }

            int Bubble = 0;
            if (!int.TryParse(Params[1].ToString(), out Bubble))
            {
                Session.SendWhisper("Per favore, inserire un numero valido.");
                return;
            }

            ChatStyle Style = null;
            if (!AkiledEnvironment.GetGame().GetChatManager().GetChatStyles().TryGetStyle(Bubble, out Style) || (Style.RequiredRight.Length > 0 && !Session.GetHabbo().HasFuse("custom_bubbles")))
            {
                Session.SendWhisper("Spiacenti, non puoi utilizzare questa bolla a causa delle autorizzazioni di classificazione [Raro: 32, 28]!");
                return;
            }

            roomuser.LastBubble = Bubble;
            Session.GetHabbo().CustomBubbleId = Bubble;
            Session.SendWhisper("Panino adattato a: " + Bubble);
        }
    }
}