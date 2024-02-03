using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms.Games;
using System;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class abrazar : IChatCommand
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

            if (TargetRoom == null)
                return;

            RoomUser roomuser = TargetRoom.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (roomuser == null)
                return;

            if (Params.Length != 2)
                return;

            RoomUser TargetUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Params[1]);

            if (Params.Length == 1)
            {
                Session.SendWhisper("Inserisci il nome dell'utente che vuoi baciare.");
                return;
            }

            if (!TargetRoom.RoomData.BesarEnabled && !TargetRoom.CheckRights(Session, true) && !Session.GetHabbo().HasFuse("room_override_custom_config"))
            {
                Session.SendWhisper("Ops, a quanto pare il proprietario della stanza ha vietato i baci nella sua stanza.");
                return;
            }

            if ((TargetUser.GetClient().GetHabbo().HasFuse("no_accept_use_custom_commands")))
            {
                Session.SendWhisper("Non puoi baciare questo utente.");
                return;
            }

            if (TargetUser == null)
            {
                Session.SendWhisper(Convert.ToString(Params[1]) + " l'utente non è più qui.");
                return;
            }

            if (TargetUser.GetClient().GetHabbo().Id == Session.GetHabbo().Id)
                return;

            if (TargetUser.GetClient().GetHabbo().PremiumProtect && !Session.GetHabbo().HasFuse("fuse_mod"))
            {
                roomuser.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue("premium.notallowed", Session.Langue));
                return;
            }


            RoomUser ThisUser = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (ThisUser == null)
                return;

            if (Session.GetHabbo().Rank <= 3)
            {
                if (!((Math.Abs(TargetUser.X - ThisUser.X) >= 2) || (Math.Abs(TargetUser.Y - ThisUser.Y) >= 2)))
                {

                    Room.SendPacket(new ChatComposer(ThisUser.VirtualId, "@red@ * bacio " + Params[1] + " *", 0, ThisUser.LastBubble));
                    System.Threading.Thread.Sleep(500);
                    Room.SendPacketWeb(new PlaySoundComposer("kiss", 2)); //Type = Trax
                    Room.SendPacket(new ChatComposer(TargetUser.VirtualId, "@red@ * Sono stato baciato da :$ *" + Session.GetHabbo().Username + " <3 *", 0, ThisUser.LastBubble));
                    System.Threading.Thread.Sleep(500);
                    ThisUser.ApplyEffect(9);
                    System.Threading.Thread.Sleep(500);
                    TargetUser.ApplyEffect(9);
                    TargetUser.ApplyEffect(0);
                    ThisUser.ApplyEffect(0);
                    Session.GetHabbo().last_kiss = AkiledEnvironment.GetIUnixTimestamp();
                    return;

                }
                else
                {
                    Session.SendWhisper("@green@ Oops, " + Params[1] + " non è abbastanza vicino!");
                }
            }
            if (Session.GetHabbo().Rank >= 4)
            {
                Room.SendPacket(new ChatComposer(ThisUser.VirtualId, "@red@ * Abbraccio " + Params[1] + " *", 0, ThisUser.LastBubble));
                System.Threading.Thread.Sleep(500);
                Room.SendPacket(new ChatComposer(TargetUser.VirtualId, "@red@ * Sono stato abbracciato da :$ *" + Session.GetHabbo().Username + " <3 *", 0, ThisUser.LastBubble));
                System.Threading.Thread.Sleep(500);
                ThisUser.ApplyEffect(9);
                TargetUser.ApplyEffect(9);
                System.Threading.Thread.Sleep(5000);
                TargetUser.ApplyEffect(0);
                ThisUser.ApplyEffect(0);
                Session.GetHabbo().last_kiss = AkiledEnvironment.GetIUnixTimestamp();
                return;
            }

        }
    }
}
