using Akiled.HabboHotel.GameClients;
using System.Linq;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class RoomBadgeCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Spiacenti, devi inserire il codice badge!");
                return;
            }
            foreach (RoomUser User in Room.GetRoomUserManager().GetUserList().ToList())
            {
                if (User != null && User.GetClient() != null && User.GetClient().GetHabbo() != null)
                {
                    if (!User.GetClient().GetHabbo().GetBadgeComponent().HasBadge(Params[1]))
                    {
                        User.GetClient().GetHabbo().GetBadgeComponent().GiveBadge(Params[1], 0, true);
                        User.GetClient().SendNotification("<font color = '#008000'><font size= '16'><b>Hai ricevuto un nuovo badge!</b></font></font>\n\nEsatto, lo staff è molto felice della tua presenza, quindi ti hanno dato un nuovo badge, controlla il tuo inventario e brilla.");
                    }
                    else
                    {
                        User.GetClient().SendWhisper(Session.GetHabbo().Username + ", Hai già questo badge nel tuo inventario, ma rallegrati, ne stanno arrivando di nuovi per tua fortuna.");
                    }
                }
            }
            Session.SendWhisper("Hai inviato il badge " + Params[1] + " a tutta la stanza!");
        }
    }
}
