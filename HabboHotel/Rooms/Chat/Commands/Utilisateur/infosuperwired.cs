using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd{    class infosuperwired : IChatCommand    {        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)        {            Session.SendPacket(new NuxAlertComposer("habbopages/infosuperwired.txt"));            return;        }    }}