using Akiled.HabboHotel.GameClients;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd

            RoomUser roomuser = room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);


            if (Params.Length == 1)
            {
                TargetUser.SendWhisperChat("Introduce el nombre del usuario que deseas hacer el super pull.");
                return;
            }

            if (!Room.RoomData.SPullEnabled && !Room.CheckRights(Session, true) && !Session.GetHabbo().HasFuse("room_override_custom_config"))
            {
                Session.SendWhisper("Oops, al parecer el due�o de la sala ha prohibido hacer los super pull en su sala.");
                return;
            }

            if ((TargetUser.GetClient().GetHabbo().HasFuse("no_accept_use_custom_commands")))
            {
                Session.SendWhisper("No se puede jalar a este usuario.");
                return;
            }

            if (TargetUser == null)