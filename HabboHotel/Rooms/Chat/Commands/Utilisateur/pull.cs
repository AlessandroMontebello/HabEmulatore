using Akiled.HabboHotel.GameClients;

            if (TargetUser == null)

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
            }