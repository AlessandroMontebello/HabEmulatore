using Akiled.Communication.Packets.Outgoing;
using Akiled.Database.Interfaces;

namespace Akiled.Communication.Packets.Incoming.Structure
                return;

            Room room = AkiledEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
                        roomUserByHabbo.SetStatus("flatctrl", "0");
                        roomUserByHabbo.UpdateNeeded = true;

                        roomUserByHabbo.GetClient().SendPacket(new YouAreControllerComposer(RoomRightLevels.NONE));