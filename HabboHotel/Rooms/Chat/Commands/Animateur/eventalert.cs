using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class eventalert : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (!AkiledEnvironment.GetGame().GetAnimationManager().AllowAnimation())
                return;

            string str = CommandManager.MergeParams(Params, 1);
            str = "<b>[EVENNTO]</b>\r\n" + str + "\r\n- " + Session.GetHabbo().Username;
            AkiledEnvironment.GetGame().GetClientManager().SendSuperNotif("Evento dello staff", str, "game_promo_small", "event:navigator/goto/" + UserRoom.RoomId, "Vai!", true, true);
        }
    }
}
