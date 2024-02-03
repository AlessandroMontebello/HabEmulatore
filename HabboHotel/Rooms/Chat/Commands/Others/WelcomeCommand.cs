using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class WelcomeCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            RoomUser user = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Username);
            RoomUser target = Room.GetRoomUserManager().GetRoomUserByHabbo(Params[1].ToString());
            string name_hotel = (AkiledEnvironment.GetConfig().data["namehotel_text"]);
            if (target.GetClient() == null)
            {
                Session.SendWhisper("Scrivi il nome dell'utente a cui vuoi dare il benvenuto in hotel.");
                return;
            }
            else
            {
                Room.SendPacket(new ChatComposer(user.VirtualId, " Benvenuto a " + name_hotel + ": Spero che ti divertirai qui e ricordati di invitare i tuoi amici. grazie <3", 0, 34));
            }
        }
    }
}
