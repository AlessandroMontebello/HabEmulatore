using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class GiveBadge : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {
            if (Params.Length != 3)
            {
                Session.SendWhisper("Inserisci il nome dell'utente a cui vuoi inviare un badge!");
                return;
            }


            GameClient TargetClient = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient != null)
            {
                if (!TargetClient.GetHabbo().GetBadgeComponent().HasBadge(Params[2]))
                {
                    TargetClient.GetHabbo().GetBadgeComponent().GiveBadge(Params[2], 0, true);
                    string BadgeCode = Params[2];
                    TargetClient.GetHabbo().GetBadgeComponent().GiveBadge(BadgeCode, 0, true);
                    TargetClient.SendPacket(new ReceiveBadgeComposer(BadgeCode));
                    if (TargetClient.GetHabbo().Id != Session.GetHabbo().Id)
                    {
                        TargetClient.SendMessage(new NewYearComposer(Params[2]));

                        Session.SendWhisper("Gli hai dato il distintivo (" + Params[2] + ") con successo a " + Params[1] + ".");
                    }
                    else
                    {
                        Session.SendMessage(new NewYearComposer(Params[2]));
                        Session.SendWhisper("Gli hai dato il distintivo (" + Params[2] + ") con successo a " + Params[1] + ".");
                    }
                }
                else
                    Session.SendWhisper("\"Wow! Questo utente ha già il badge.(" + Params[2] + ")!");
                return;
            }
            else
            {
                Session.SendNotification(AkiledEnvironment.GetLanguageManager().TryGetValue("input.usernotfound", Session.Langue));
                return;

            }
        }
    }
}
