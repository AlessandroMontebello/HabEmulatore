using Akiled.Communication.Packets.Outgoing;
using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Rooms.Games;
using System;
using System.Threading;

namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
{
    class sexcommand : IChatCommand
    {
        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)
        {

            if (UserRoom.Team != Team.none || UserRoom.InGame)
                return;

            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            if ((double)Session.GetHabbo().last_sex > AkiledEnvironment.GetUnixTimestamp() - 30.0 && !Session.GetHabbo().HasFuse("override_limit_command"))
            {
                Session.SendWhisper("È necessario attendere 30 secondi per utilizzare nuovamente il comando", 1);
                return;
            }

            if (TargetRoom == null)
                return;

            RoomUser roomUserByHabbo1 = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
            if (roomUserByHabbo1 == null)
                return;

            if (Params.Length == 0)
            {
                Session.SendWhisper("Scegli l'utente con cui vuoi farlo..", 0);
            }
            if (!TargetRoom.RoomData.SexEnabled && !Room.CheckRights(Session, true) && !Session.GetHabbo().HasFuse("room_override_custom_config"))
            {
                Session.SendWhisper("Siamo spiacenti, ma il proprietario della stanza ha disabilitato questo comando.");
                return;
            }
            else
            {
                RoomUser roomUserByHabbo2 = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Params[1]);
                GameClient clientByUsername = AkiledEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
                if (clientByUsername.GetHabbo().Username == Session.GetHabbo().Username)
                {

                    RoomUser Self = Room.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
                    if (roomUserByHabbo1 == Self)
                    {
                        Session.SendWhisper("Ehi, so che lo vorresti, ma non puoi farti andare in culo!");
                        return;
                    }
                }
                else if (clientByUsername.GetHabbo().CurrentRoomId == Session.GetHabbo().CurrentRoomId && (Math.Abs(checked(roomUserByHabbo1.X - roomUserByHabbo2.X)) < 2 && Math.Abs(checked(roomUserByHabbo1.Y - roomUserByHabbo2.Y)) < 2))
                {
                    if (string.IsNullOrEmpty(Session.GetHabbo().sexWith) && (clientByUsername.GetHabbo().Username != Session.GetHabbo().sexWith && Session.GetHabbo().Username != clientByUsername.GetHabbo().sexWith))
                    {
                        Session.GetHabbo().sexWith = clientByUsername.GetHabbo().Username;
                        clientByUsername.SendNotification(Session.GetHabbo().Username + " Vuole davvero farlo con te, accetti il ​​suo invito?" + Session.GetHabbo().Username + ", scrivi :sex " + Session.GetHabbo().Username + " se lo accetti.");
                        Session.SendNotification("Il tuo desiderio di sesso è stato trasmesso, se la persona accetterà ne avrai moltissimo.");
                    }
                    else if (roomUserByHabbo2 != null)
                    {
                        if (clientByUsername.GetHabbo().sexWith == Session.GetHabbo().Username)
                        {
                            if (roomUserByHabbo2.GetClient() != null && roomUserByHabbo2.GetClient().GetHabbo() != null)
                            {
                                if (clientByUsername.GetHabbo().CurrentRoomId == Session.GetHabbo().CurrentRoomId && (Math.Abs(checked(roomUserByHabbo1.X - roomUserByHabbo2.X)) < 2 && Math.Abs(checked(roomUserByHabbo1.Y - roomUserByHabbo2.Y)) < 2))
                                {
                                    clientByUsername.GetHabbo().sexWith = (string)null;
                                    Session.GetHabbo().sexWith = (string)null;
                                    if (Session.GetHabbo().Gender == "m")
                                    {
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Toglie mutandine " + Params[1] + " succhia quel clistoride *", 0, 0), false);
                                        Thread.Sleep(2000);
                                        roomUserByHabbo1.ApplyEffect(9);
                                        roomUserByHabbo2.ApplyEffect(9);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Bagnalo con la lingua e succhia molto bene il suo cosino." + Session.GetHabbo().Username + ", mooorderleee la testolina.*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Mangia quel delizioso toto " + Params[1] + ", la rende fantastica come mai prima d'ora nella sua fottuta vita*", 0, 0), false);
                                        roomUserByHabbo1.ApplyEffect(502);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Grida il mio nome, " + Params[1] + " Chiedi e chiedi più pene, che è quello che ti piace di più!*", 0, 0), false);
                                        roomUserByHabbo1.ApplyEffect(503);
                                        Room.SendPacket((IServerPacket)new ShoutMessageComposer(roomUserByHabbo2.VirtualId, " " + Session.GetHabbo().Username + " ", 0, 0), false);
                                        roomUserByHabbo2.ApplyEffect(501);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "aaaaaaai, aaaaah, mettilo papi... mettilo... così, che bontà, è ancora più grosso*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo1.ApplyEffect(501);
                                        roomUserByHabbo2.ApplyEffect(503);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Mettendolo dentro lentamente, tesoro, sto per esplodere :oo*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Apri quelle gambe per me ed eccitami " + Session.GetHabbo().Username + ", così potrai bagnarmi il viso nel latte*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo1.ApplyEffect(501);
                                        roomUserByHabbo2.ApplyEffect(502);
                                        roomUserByHabbo1.ApplyEffect(503);
                                        roomUserByHabbo2.ApplyEffect(500);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Darleeee, è molto duro dentro " + Params[1] + "* *ooooh oooh oooooh*", 0, 0), false);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Mordendoti le labbra lo rendi fantastico amore mio continua così ashhh ahhhh aiii!*", 0, 0), false);
                                        Thread.Sleep(2000);
                                        roomUserByHabbo1.ApplyEffect(503);
                                        roomUserByHabbo2.ApplyEffect(500);
                                        roomUserByHabbo1.ApplyEffect(0);
                                        roomUserByHabbo2.ApplyEffect(0);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Toglie il reggiseno " + Params[1] + "*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo1.ApplyEffect(9);
                                        roomUserByHabbo2.ApplyEffect(9);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*butta dalla finestra*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo1.ApplyEffect(502);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Oh, adoro come queste tette siano tutte rifatte, bb " + Session.GetHabbo().Username + "*", 0, 0), false);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Mmmmmm* Che deliziose amore mio, quei capezzoli rosa, MORDI", 0, 0), false);
                                        Thread.Sleep(4000);
                                        roomUserByHabbo1.ApplyEffect(502);
                                        roomUserByHabbo2.ApplyEffect(503);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Amore, toccami mentre mi succhi le tette---*", 0, 0), false);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*oooh, ooooooh, ooooh, tocca la tua piccola cosa con il mio dito amore mio.*", 0, 0), false);
                                        Thread.Sleep(2000);
                                        roomUserByHabbo1.ApplyEffect(501);
                                        roomUserByHabbo2.ApplyEffect(500);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Mettimi a 90 bb, " + Session.GetHabbo().Username + " mettilooooo*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*OOHH que rico questo culo che sembra un cuore <3!", 0, 0), false);
                                        Thread.Sleep(1000);
                                        roomUserByHabbo1.ApplyEffect(502);
                                        roomUserByHabbo2.ApplyEffect(503);
                                        roomUserByHabbo1.ApplyEffect(0);
                                        roomUserByHabbo2.ApplyEffect(0);
                                        Thread.Sleep(4000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Bagnami la faccia nel latte, " + Session.GetHabbo().Username + " si bb.*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo2.ApplyEffect(542);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*OOHH OOHH que rico questo culo che sembra un cuore <3!", 0, 0), false);
                                        Thread.Sleep(3000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Uff vita mia sei il migliore, così ricco!", 0, 0), false);
                                    }
                                    else
                                    {
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Togliti i pantaloni " + Params[1] + "*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo1.ApplyEffect(9);
                                        roomUserByHabbo2.ApplyEffect(9);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*butta dalla finestra*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo1.ApplyEffect(502);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Ayyy, amore, succhiami Richoo " + Session.GetHabbo().Username + "*", 0, 0), false);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Mmm* Che bontà amore mio, ce l'hai davvero grande, MORDE", 0, 0), false);
                                        Thread.Sleep(4000);
                                        roomUserByHabbo1.ApplyEffect(502);
                                        roomUserByHabbo2.ApplyEffect(503);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Amore, toccami mentre mi succhi le tette---*", 0, 0), false);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*oooh, ooooooh, ooooh, tocca la tua piccola cosa con il mio dito amore mio.*", 0, 0), false);
                                        Thread.Sleep(2000);
                                        roomUserByHabbo1.ApplyEffect(501);
                                        roomUserByHabbo2.ApplyEffect(500);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Mettimi a 90 bb, " + Session.GetHabbo().Username + " mettilooooo*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*OOHH que rico questo culo che sembra un cuore <3!", 0, 0), false);
                                        Thread.Sleep(1000);
                                        roomUserByHabbo1.ApplyEffect(502);
                                        roomUserByHabbo2.ApplyEffect(503);
                                        roomUserByHabbo1.ApplyEffect(0);
                                        roomUserByHabbo2.ApplyEffect(0);
                                        Thread.Sleep(4000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo1.VirtualId, "*Bagnami la faccia nel latte, " + Session.GetHabbo().Username + " si bb.*", 0, 0), false);
                                        Thread.Sleep(3000);
                                        roomUserByHabbo2.ApplyEffect(542);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*OOHH que rico questo culo che sembra un cuore <3!", 0, 0), false);
                                        Thread.Sleep(3000);
                                        Room.SendPacket((IServerPacket)new ChatComposer(roomUserByHabbo2.VirtualId, "*Uff vita mia, sei il migliore che rico!", 0, 0), false);
                                        Session.GetHabbo().last_sex = AkiledEnvironment.GetIUnixTimestamp();

                                    }
                                }
                                else
                                    Session.SendWhisper("Avvicinati, che non sei il nero di whatsapp.", 0);
                            }
                            else
                                Session.SendWhisper("Si è verificato un errore e l'utente non è stato trovato.", 0);
                        }
                        else
                            Session.SendWhisper("Maiale, ti avverto, sei stato rifiutato, masturbati come puoi.", 0);
                    }
                    else
                        Session.SendWhisper("Questa persona sembra non essere nella stanza.", 0);
                }
                else
                    Session.SendWhisper("Sei troppo lontano per fare sesso!", 0);
            }

        }
    }
}
