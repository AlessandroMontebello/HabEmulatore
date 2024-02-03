
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    internal class SayBotUser : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length < 3)
                Session.SendWhisper("Spiacenti, inserisci il nome del bot, seguito dal messaggio che vuoi che dica.", 34);
            else if (!Room.CheckRights(Session, true))
            {
                Session.SendWhisper("Spiacenti, solo il proprietario della stanza può eseguire questo comando.", 34);
            }
            else
            {
                string name = Params[1];
                RoomUser botOrPetByName = Room.GetRoomUserManager().GetBotOrPetByName(name);
                if (botOrPetByName == null)
                    return;
                string MessageText = CommandManager.MergeParams(Params, 2);
                if (string.IsNullOrEmpty(MessageText))
                    return;
                botOrPetByName.OnChat(MessageText, botOrPetByName.IsPet ? 0 : 2);
            }
        }
    }
}
