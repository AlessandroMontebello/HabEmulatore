using Akiled.HabboHotel.GameClients;using Akiled.HabboHotel.Rooms.Games;using System;namespace Akiled.HabboHotel.Rooms.Chat.Commands.Cmd{    class pull : IChatCommand    {        public void Execute(GameClient Session, Room Room, RoomUser UserRoom, string[] Params)        {            if (UserRoom.Team != Team.none || UserRoom.InGame)                return;            if (!Room.PushPullAllowed)                return;            if (Params.Length != 2)                return;            RoomUser TargetUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Convert.ToString(Params[1]));            if (TargetUser == null || TargetUser.GetClient() == null || TargetUser.GetClient().GetHabbo() == null)                return;

            if (TargetUser == null)            {                TargetUser.SendWhisperChat(Convert.ToString(Params[1]) + " l'utente non è più qui.");                return;            }

            if (Params.Length == 1)
            {
                TargetUser.SendWhisperChat("Inserisci il nome dell'utente che desideri pullare.");
                return;
            }

            if ((TargetUser.GetClient().GetHabbo().HasFuse("no_accept_use_custom_commands")))
            {
                TargetUser.SendWhisperChat("Questo utente non può essere pullato.");
                return;
            }

            if (!Room.RoomData.PullEnabled && !Room.CheckRights(Session, true) && !Session.GetHabbo().HasFuse("room_override_custom_config"))
            {
                Session.SendWhisper("Ops, a quanto pare il proprietario della stanza ha vietato di pullare nella sua stanza.");
                return;
            }            if (TargetUser.GetClient().GetHabbo().Id == Session.GetHabbo().Id)                return;            if (TargetUser.GetClient().GetHabbo().PremiumProtect && !Session.GetHabbo().HasFuse("fuse_mod"))            {                UserRoom.SendWhisperChat(AkiledEnvironment.GetLanguageManager().TryGetValue("premium.notallowed", Session.Langue));                return;            }            if (Math.Abs(UserRoom.X - TargetUser.X) < 3 && Math.Abs(UserRoom.Y - TargetUser.Y) < 3)            {                UserRoom.OnChat("@red@ * Pulla " + Params[1] + " *", 0, false);                if (UserRoom.RotBody % 2 != 0)                    UserRoom.RotBody--;                if (UserRoom.RotBody == 0)                    TargetUser.MoveTo(UserRoom.X, UserRoom.Y - 1);                else if (UserRoom.RotBody == 2)                    TargetUser.MoveTo(UserRoom.X + 1, UserRoom.Y);                else if (UserRoom.RotBody == 4)                    TargetUser.MoveTo(UserRoom.X, UserRoom.Y + 1);                else if (UserRoom.RotBody == 6)                    TargetUser.MoveTo(UserRoom.X - 1, UserRoom.Y);            }            else            {                UserRoom.SendWhisperChat(Params[1] + " È molto lontano da te.");                return;            }        }    }}