using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class PremioUser : IChatCommand
    {
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, RoomUser UserRoom, string[] Params)
        {
            string name_monedaoficial = (AkiledEnvironment.GetConfig().data["name_monedaoficial"]);
            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci il nome.");
                return;
            }

            GameClient Target = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (Target == null)
            {
                Session.SendWhisper("Spiacenti, questo utente non è stato trovato!");
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


            if (Session.GetHabbo().CurrentRoom == TargetClient.GetHabbo().CurrentRoom)
            {

                int Amount;

                if (int.TryParse(Params[2], out Amount))
                {

                    if (Amount < 0 || Amount > 4)
                    {
                        Session.SendWhisper("Per favore inserisci un numero. (1-4)");
                        return;
                    }

                    Target.GetHabbo().Duckets += Amount;
                    Target.SendPacket(new HabboActivityPointNotificationComposer(Target.GetHabbo().Duckets, Amount));



                    if (Target.GetHabbo().Id != Session.GetHabbo().Id)
                        Target.SendNotification(Session.GetHabbo().Username + " ti ha inviato " + Amount.ToString() + " Diamanti!");
                    Session.SendWhisper("Hia inviato " + Amount + " Smeraldi a " + Target.GetHabbo().Username + "!");
                    AkiledEnvironment.GetGame().GetClientManager().SendMessage(RoomNotificationComposer.SendBubble("ganador", "" + Target.GetHabbo().Username + " ha appena vinto l'evento. congratulazioni :)", ""));

                }
                else
                {
                    Session.SendWhisper("Ops, gli importi sono solo in cifre...!");

                }


            }

        }
    }
}
