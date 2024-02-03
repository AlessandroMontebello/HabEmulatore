using Akiled.HabboHotel.GameClients;
using System.Collections.Generic;
using System.Linq;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class CloseDiceCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            RoomUser roomUser = Room?.GetRoomUserManager()?.GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (roomUser == null)
            {
                return;
            }

            List<Items.Item> userBooth = Room.GetRoomItemHandler().GetFloor.Where(x => x != null && Gamemap.TilesTouching(
                x.Coordinate, roomUser.Coordinate) && x.Data.InteractionType == Items.InteractionType.dice).ToList();

            if (userBooth.Count != 5)
            {
                Session.SendWhisper("Devi avere 5 dadi vicino a te.", 34);
                return;
            }

            userBooth.ForEach(x =>
            {
                x.ExtraData = "0";
                x.UpdateState();
            });

            Session.SendWhisper("I dadi sono stati chiusi correttamente", 34);
        }
    }
}
