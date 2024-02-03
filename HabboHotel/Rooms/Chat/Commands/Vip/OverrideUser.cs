using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    internal class OverrideUser : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Session.GetHabbo().CurrentRoom == null)
                return;
            if (!Room.CheckRights(Session, true))
                Session.SendWhisper("Spiacenti, solo il proprietario della stanza può eseguire questo comando.", 34);
            else if (UserRoom.AllowOverride)
            {
                UserRoom.AllowOverride = false;
                UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue("override.disabled", Session.Langue));
            }
            else
            {
                UserRoom.AllowOverride = true;
                UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue("override.enabled", Session.Langue));
            }
        }
    }
}