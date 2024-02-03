using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms.Games;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.SpecialPvP
{
    class GolpeCommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            if (UserRoom.Team != Team.none || UserRoom.InGame)
                return;

            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            if ((double)Session.GetHabbo().last_kiss > AkiledEnvironment.GetUnixTimestamp() - 30.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 30 secondi per utilizzare nuovamente il comando", 1);
                return;
            }

            if (Params.Length == 1)
            {
                Session.SendWhisper("Per favore scrivi il nome dell'utente che vuoi colpire.");
                return;
            }

            if (!TargetRoom.RoomData.GolpeEnabled && !TargetRoom.CheckRights(Session, true) && !Session.GetHabbo().HasFuse("room_override_custom_config"))
            {
                Session.SendWhisper("Siamo spiacenti, ma il proprietario della stanza ha disabilitato questo comando oppure non sei autorizzato ad accedere alla stanza.");
                return;
            }

            GameClient TargetClient = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient == null)
            {
                Session.SendWhisper("Questo utente non è nella stanza virtuale o è offline.");
                return;
            }
            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabboId(TargetClient.GetHabbo().Id);
            if (TargetUser == null)
            {
                Session.SendWhisper("Spiacenti, si è verificato un errore.");
            }
            if (TargetClient.GetHabbo().Username == Session.GetHabbo().Username)
            {
                Session.SendWhisper("Vuoi colpirti? lol!");
                return;
            }
            if ((TargetClient.GetHabbo().Username == "Emmanuelrtpo") || (TargetClient.GetHabbo().Username == "Norman") || (TargetClient.GetHabbo().Username == "Nicofer"))
            {
                Session.SendWhisper("Questo utente non può essere colpito.");
                return;
            }

            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (!((Math.Abs(TargetUser.X - ThisUser.X) >= 2) || (Math.Abs(TargetUser.Y - ThisUser.Y) >= 2)))
            {
                Room.SendPacketWeb(new PlaySoundComposer("golpe", 2)); //Type = Trax
                Room.SendPacket(new ChatComposer(ThisUser.VirtualId, "@red@ *" + Params[1] + " è stato colpito in faccia*", 0, ThisUser.LastBubble));
                Room.SendPacket(new ChatComposer(TargetUser.VirtualId, "@cyan@ Che diavolo di uomo!", 0, ThisUser.LastBubble));

                if (TargetUser.RotBody == 4)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 0)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 6)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 2)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 3)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 1)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 7)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }

                if (ThisUser.RotBody == 5)
                {
                    TargetUser.Statusses.Add("lay", "1.0 null");
                    TargetUser.Z -= 0.35;
                    TargetUser.IsSit = true;
                    TargetUser.UpdateNeeded = true;
                    TargetUser.ApplyEffect(157);
                }
                Session.GetHabbo().last_golpe = AkiledEnvironment.GetIUnixTimestamp();


            }
            else
            {
                Session.SendWhisper("@green@ Oops, " + Params[1] + " Non è abbastanza vicino!");
            }

        }
    }
}