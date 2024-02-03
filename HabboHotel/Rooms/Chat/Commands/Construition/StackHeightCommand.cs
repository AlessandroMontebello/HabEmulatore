using Akiled.Core;
using Akiled.HabboHotel.GameClients;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class StackHeightCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (!Room.CheckRights(Session, true))
                return;

            RoomUser user = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (user == null)
                return;

            if (Params.Length == 1)
            {
                Session.SendWhisper("Scrivi il valore.", 34);
                return;
            }
            else if (Params[1].Equals("clear", StringComparison.Ordinal) || Params[1].Equals("limpiar", StringComparison.Ordinal))
            {
                user.ConstruitZMode = false;
                user.ConstruitHeigth = 0;
                Session.SendWhisper("Comando disabilitato.", 34);
                return;
            }
            
            if (!double.TryParse(Params[1], out double value))
                return;

            if (value < 0 || value > 100)
            {
                Session.SendWhisper("Tra 1 e 100.", 34);
                return;
            }

            user.ConstruitZMode = true;
            user.ConstruitHeigth = value;
            Session.SendWhisper("Valore cambiato in: " + value, 34);
        }
    }
}
