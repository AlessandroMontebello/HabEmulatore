﻿using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Roleplay.Player;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class Prison : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length != 2)

            if (!Room.IsRoleplay || !Room.Pvp)
                return;
            if (Rp == null)
                return;
            if (RpTwo == null)
                return;
            {
                return;
            }
            {
                UserRoom.OnChat("*Questo è un arresto " + TargetRoomUser.GetUsername() + "*");
                UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue("rp.prisonnotallowed", Session.Langue));
                return;
            }
                TargetRoomUser.Freeze = true;
                TargetRoomUser.FreezeEndCounter = 0;
                TargetRoomUser.IsSit = true;
                TargetRoomUser.UpdateNeeded = true;

                RpTwo.SendPrison = true;

            //UserRoom.ApplyEffect(737, true);

            if (UserRoom.FreezeEndCounter <= 2)
            {
                UserRoom.Freeze = true;
                UserRoom.FreezeEndCounter = 2;
            }
        }
    }
}