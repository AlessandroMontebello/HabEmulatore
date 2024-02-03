
using Akiled.Communication.Packets.Outgoing;
using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.Communication.Packets.Outgoing.WebSocket;
using Akiled.Database.Interfaces;
using Akiled.HabboHotel.GameClients;
using Akiled.HabboHotel.Quests;
using Akiled.HabboHotel.Roleplay.Player;
using Akiled.HabboHotel.Rooms;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Akiled.HabboHotel.Items.Interactors
{
    public class InteractorMineria : FurniInteractor
    {
        public override void OnPlace(GameClient Session, Item Item)
        {
            Item.ExtraData = "0";
            if (Item.InteractingUser <= 0)
                return;
            Item.InteractingUser = 0;
        }

        public override void OnRemove(GameClient Session, Item Item)
        {
            Item.ExtraData = "0";
            if (Item.InteractingUser <= 0)
                return;
            Item.InteractingUser = 0;
        }

        public override void OnTick(Item item) => throw new NotImplementedException();

        public override void OnTrigger(GameClient Session, Item Item, int Request, bool UserHasRights)
        {
            if (Session != null)
                AkiledEnvironment.GetGame().GetQuestManager().ProgressUserQuest(Session, QuestType.FURNI_SWITCH);
            string room_idmina = AkiledEnvironment.GetConfig().data["room_idmina"];
            string str1 = AkiledEnvironment.GetConfig().data["baseitem_idmina"];
            string str2 = AkiledEnvironment.GetConfig().data["baseitem_idexplosivo"];
            string str3 = AkiledEnvironment.GetConfig().data["item_idexplosivo"];
            string str4 = AkiledEnvironment.GetConfig().data["item_idporbora"];
            if (Session.GetHabbo().CurrentRoomId != Convert.ToInt32(room_idmina) || Item.GetBaseItem().Id != Convert.ToInt32(str2))
                return;
            if (Session.GetHabbo().is_mining)
            {
                Session.SendWhisper("Non puoi creare esplosivi mentre estrai!", 1);
            }
            else
            {
                if (Session.GetHabbo().porbora == -1 || Session.GetHabbo().porbora == 0)
                {
                    using (IQueryAdapter queryReactor = AkiledEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery("SELECT * FROM `user_rpitems` WHERE `user_id` = " + Session.GetHabbo().Id.ToString() + " AND `item_id` = '" + Convert.ToInt32(str4).ToString() + "' LIMIT 1;");
                        DataRow row = queryReactor.GetRow();
                        Session.GetHabbo().porbora = row == null ? 0 : (int)row["count"];
                    }
                }
                if (Session.GetHabbo().porbora == -1 || Session.GetHabbo().porbora == 0)
                {
                    Session.SendWhisper("Hai bisogno della polvere da sparo per creare esplosivi, acquistala nel negozio TNT Bot.", 1);
                }
                else
                {
                    RoomUser ThisUser = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabboId(Session.GetHabbo().Id);
                    if (Math.Abs(Item.GetX - ThisUser.X) >= 2 || Math.Abs(Item.GetY - ThisUser.Y) >= 2)
                        Session.SendWhisper("Devi essere più vicino per iniziare a creare esplosivi.", 1);
                    else
                        Task.Run((Func<Task>)(async () =>
                        {
                            try
                            {
                                if (Session.GetHabbo().onfarm)
                                {
                                    Session.GetHabbo().onfarm = false;
                                    Session.SendWhisper("Hai smesso di creare esplosivi.", 1);
                                    ThisUser.ApplyEffect(0);
                                }
                                else
                                {
                                    Session.SendWhisper("Hai iniziato a creare esplosivi, attendi l'elaborazione, non pensare nemmeno di muoverti.", 1);
                                    ThisUser.ApplyEffect(64);
                                    Session.GetHabbo().SendPacketWeb((IServerPacket)new PlaySoundComposer("pico", 2));
                                    Session.GetHabbo().onfarm = true;
                                    int x = 0;
                                    int time = AkiledEnvironment.GetIUnixTimestamp();
                                    while (x <= 30)
                                    {
                                        if (!Session.GetHabbo().onfarm)
                                            return;
                                        if (Math.Abs(Item.GetX - ThisUser.X) >= 2 || Math.Abs(Item.GetY - ThisUser.Y) >= 2)
                                        {
                                            Session.SendWhisper("Hai interrotto il processo di creazione di esplosivi.", 1);
                                            ThisUser.ApplyEffect(0);
                                            Session.GetHabbo().onfarm = false;
                                            return;
                                        }
                                        if (Session.GetHabbo().CurrentRoomId != Convert.ToInt32(room_idmina))
                                        {
                                            Session.GetHabbo().onfarm = false;
                                            return;
                                        }
                                        if (time < AkiledEnvironment.GetIUnixTimestamp())
                                        {
                                            time = AkiledEnvironment.GetIUnixTimestamp();
                                            ++x;
                                        }
                                    }
                                    await Task.Delay(500);
                                    if (Session.GetHabbo().onfarm)
                                    {
                                        Room room = AkiledEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
                                        Session.GetHabbo().onfarm = false;
                                        Random rand = new Random();
                                        int wurm = rand.Next(1, 5);
                                        Session.GetHabbo().Leftpolvora();
                                        Session.SendWhisper("Hai avuto successo " + wurm.ToString() + " Esplosivi creati correttamente!", 1);
                                        room.SendPacket((IServerPacket)new ChatComposer(ThisUser.VirtualId, "@blue@*" + ThisUser.GetClient().GetHabbo().Username + " Ha creato " + wurm.ToString() + " Esplosivi*", 0, ThisUser.LastBubble));
                                        ThisUser.ApplyEffect(0);
                                        RolePlayer Rp = ThisUser.Roleplayer;
                                        Rp.AddInventoryItem(373, wurm);
                                        room = (Room)null;
                                        rand = (Random)null;
                                        Rp = (RolePlayer)null;
                                        room = (Room)null;
                                        rand = (Random)null;
                                        Rp = (RolePlayer)null;
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }));
                }
            }
        }
        public override void OnTrigger2(GameClient Session, Item Ball, int Request)
        {
        }
    }
}
