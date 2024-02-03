using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd
            //if (UserRoom.team != Team.none || UserRoom.InGame)
            //return;

            if (Room.IsRoleplay && !Room.CheckRights(Session))
            {
                RoomUser Bot = Room.GetRoomUserManager().GetBotByName(Username);
                if (Bot == null || Bot.BotData == null)
                    return;

                Session.GetHabbo().Gender = Bot.BotData.Gender;
                Session.GetHabbo().Look = Bot.BotData.Look;
            }
            else
            {

                if (clientByUsername.GetHabbo().PremiumProtect && !Session.GetHabbo().HasFuse("fuse_mod"))
                {
                    UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue("premium.notallowed", Session.Langue));
                    return;
                }

                Session.GetHabbo().Gender = clientByUsername.GetHabbo().Gender;
                Session.GetHabbo().Look = clientByUsername.GetHabbo().Look;
            }