﻿using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Roleplay.Player;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class Givemoney : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length != 3)

            if (!Room.IsRoleplay)
                return;
            if (Rp == null)
                return;
            if (RpTwo == null)
                return;
        }
    }
}