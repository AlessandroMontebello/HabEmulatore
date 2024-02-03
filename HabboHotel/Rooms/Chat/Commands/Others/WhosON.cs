using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.SpecialPvP
{
    class WhosON : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            int OnlineUsers = AkiledEnvironment.GetGame().GetClientManager().Count;
            int RoomCount = AkiledEnvironment.GetGame().GetRoomManager().Count;
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            if (RoomCount == 1)
            {
                UserRoom.SendWhisperChat("Ci sono: " + OnlineUsers + " e " + RoomCount + " Stanze connesse, dentro " + name_hotel + ".");
            }
            else
            {
                UserRoom.SendWhisperChat("Ci sono: " + OnlineUsers + " e " + RoomCount + " Stanze connesse, dentro " + name_hotel + ".");
            }
        }
    }
}
