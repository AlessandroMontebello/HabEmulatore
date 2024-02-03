using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class GameTime : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (AkiledEnvironment.GetGame().GetAnimationManager().IsActivate())
            {
                string Time = AkiledEnvironment.GetGame().GetAnimationManager().GetTime();
                UserRoom.SendWhisperChat("Prossima animazione di Jack e Daisy in" + Time);
            }
            else
            {
                UserRoom.SendWhisperChat("Disattivazione dell'animazione di Jack e Daisy");
            }
        }
    }
}
