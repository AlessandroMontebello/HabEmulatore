using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class IgnoreAll : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length != 2)
                return;

            GameClient clientByUsername = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);            if (clientByUsername == null || clientByUsername.GetHabbo() == null)
                return;

            clientByUsername.GetHabbo().IgnoreAll = !clientByUsername.GetHabbo().IgnoreAll;

            if (clientByUsername.GetHabbo().IgnoreAll)
                UserRoom.SendWhisperChat("Ignora tutti attivato");
            else
                UserRoom.SendWhisperChat("Ignora tutti i disabilitato");

            using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                queryreactor.RunQuery("UPDATE users SET ignoreall = '" + AkiledEnvironment.BoolToEnum(clientByUsername.GetHabbo().IgnoreAll) + "' WHERE id = " + clientByUsername.GetHabbo().Id);
        }
    }
}
