﻿using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms.Games;
using System.Threading.Tasks;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.SpecialPvP
{
    class vomitar : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (UserRoom.Team != Team.none || UserRoom.InGame)
                return;

            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            if ((double)Session.GetHabbo().last_fumar > AkiledEnvironment.GetUnixTimestamp() - 30.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 30 secondi per utilizzare nuovamente il comando", 1);
                return;
            }

            if (!TargetRoom.RoomData.CrispyEnabled && !TargetRoom.CheckRights(Session, true) && !Session.GetHabbo().HasFuse("room_override_custom_config"))
            {
                Session.SendWhisper("Siamo spiacenti, ma il proprietario della stanza ha disabilitato questo comando.");
                return;
            }

            else
            {

                RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
                if (ThisUser == null)
                    return;

                Task.Run(async delegate
                {
                    UserRoom.OnChat("@red@ * " + Session.GetHabbo().Username + ", Vedo tutto girare *", 0, false);
                    await Task.Delay(2000);
                    UserRoom.ApplyEffect(53);
                    await Task.Delay(4000);
                    UserRoom.OnChat("@purple@ * " + Session.GetHabbo().Username + ", Voglio vomitare *", 0, false);
                    await Task.Delay(500);
                    UserRoom.ApplyEffect(0);
                    await Task.Delay(5000);
                    UserRoom.ApplyEffect(53);
                    await Task.Delay(6000);
                    UserRoom.OnChat("@green@ * " + Session.GetHabbo().Username + ", Accidenti, ho vomitato tutto*", 0, false);
                    UserRoom.ApplyEffect(11);
                    await Task.Delay(2000);
                    UserRoom.ApplyEffect(0);
                    Session.GetHabbo().last_fumar = AkiledEnvironment.GetIUnixTimestamp();
                });
            }
        }
    }
}