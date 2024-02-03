using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using System.Linq;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class massduckets : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            int Amount;
            if (int.TryParse(Params[1], out Amount))
            {
                if (Amount < 0 || Amount > 1000)
                {
                    Session.SendWhisper("Per favore inserisci un numero. (1-1000)");
                    return;
                }

                foreach (GameClient client in AkiledEnvironment.GetGame().GetClientManager().GetClients.ToList())
                {
                    if (client.GetHabbo() != null)
                    {

                        client.GetHabbo().Duckets += Amount;
                        client.SendMessage(new HabboActivityPointNotificationComposer(client.GetHabbo().Duckets, Amount));

                        if (client.GetHabbo().Id != Session.GetHabbo().Id)
                            client.SendNotification(Session.GetHabbo().Username + " ti ha mandato " + Amount.ToString() + " Diamanti!");
                    }
                }
                Session.SendWhisper("Hai inviato " + Amount + " Diamanti a tutto l'hotel online!");
                return;
            }
            else
            {
                Session.SendWhisper("È possibile inserire solo quantità numeriche.", 34);
                return;
            }
        }
    }
}