using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
                string hdcodecolor = "";

                look = look.Replace(hdcode, hdcodenoface);

                currentRoom.SendPacket(new UserChangeComposer(UserRoom, false));