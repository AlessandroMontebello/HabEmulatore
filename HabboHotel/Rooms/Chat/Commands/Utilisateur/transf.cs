using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
            if (UserRoom.Team != Team.none || UserRoom.InGame)
                return;

            if (Params.Length == 3 || Params.Length == 2)
                    Room.SendPacket(new UsersComposer(UserRoom));