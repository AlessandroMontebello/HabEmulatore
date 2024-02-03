using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using System;
using System.Data;


namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class RewardCommand : IChatCommand
    {
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, RoomUser UserRoom, string[] Params)
        {
            Room TargetRoom;
            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci il nome.");
                return;
            }

            GameClient TargetClient = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendWhisper("Questo utente non è nella stanza virtuale o è offline.");
                return;
            }

            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Siamo spiacenti, non è possibile assegnare premi, " + Session.GetHabbo().Username + ".");
                return;
            }

            if (TargetClient.GetHabbo().CurrentRoom == Session.GetHabbo().CurrentRoom)
            {
                int RewardDiamonds = 0;
                int Rewardcredits = 0;
                int RewardDuckets = 0;
                using (IQueryAdapter dbQuery = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                {
                    dbQuery.SetQuery("SELECT * FROM `game_rewardscash` LIMIT 1");
                    DataTable gUsersTable = dbQuery.GetTable();

                    foreach (DataRow Row in gUsersTable.Rows)
                    {
                        Rewardcredits = Convert.ToInt32(Row["credits"]);
                        RewardDiamonds = Convert.ToInt32(Row["Akiledcoins"]);
                        RewardDuckets = Convert.ToInt32(Row["diamantes"]);
                    }
                }
                TargetClient.GetHabbo().Credits += Rewardcredits;
                TargetClient.SendPacket(new CreditBalanceComposer(TargetClient.GetHabbo().Credits));
                TargetClient.GetHabbo().Duckets += RewardDuckets;
                TargetClient.SendPacket(new HabboActivityPointNotificationComposer(TargetClient.GetHabbo().Duckets, RewardDuckets));
                TargetClient.GetHabbo().AkiledPoints += RewardDiamonds;
                TargetClient.SendPacket(new HabboActivityPointNotificationComposer(TargetClient.GetHabbo().AkiledPoints, RewardDiamonds, 105));
                using (IQueryAdapter queryreactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                    queryreactor.RunQuery("UPDATE users SET vip_points = vip_points + " + RewardDiamonds + " WHERE id = " + TargetClient.GetHabbo().Id + " LIMIT 1");
                if (!AkiledEnvironment.GetGame().GetRoomManager().TryGetRoom(Session.GetHabbo().CurrentRoomId, out TargetRoom))
                    return;
                //Session.SendPacket(new RoomForwardComposer(Session.GetHabbo().CurrentRoomId));
                TargetClient.SendPacket(RoomNotificationComposer.SendBubble("ganador", "Ha ricevuto " + Rewardcredits + " Crediti, " + RewardDuckets + " Smeraldi, " + RewardDiamonds + " Diamanti per aver vinto la partita o l'evento.", ""));
                AkiledEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("ganador", "" + TargetClient.GetHabbo().Username + " ha appena vinto l'evento. congratulazioni :)", ""));
            }
        }
    }
}