using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
                return;

            //GameClient Client = AkiledEnvironment.GetGame().GetClientManager().GetRandomClient();
            //if (Client == null || Client.GetHabbo() == null)
            //return;

            if (Session == null || Session.GetHabbo() == null)

            using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())