using Akiled.Communication.Packets.Outgoing;
using Akiled.Database.Interfaces;
using System.Text;
                return;

            Room room = AkiledEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
                    roomUserByHabbo.SetStatus("flatctrl", "0");
                    roomUserByHabbo.UpdateNeeded = true;

                    roomUserByHabbo.GetClient().SendMessage(new YouAreControllerComposer(RoomRightLevels.NONE));