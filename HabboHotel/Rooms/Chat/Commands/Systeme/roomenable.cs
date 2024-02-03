using Akiled.HabboHotel.GameClients;

            if (!AkiledEnvironment.GetGame().GetEffectsInventoryManager().HaveEffect(NumEnable, Session.GetHabbo().HasFuse("fuse_sysadmin")))
                return;

            foreach (RoomUser User in Room.GetRoomUserManager().GetUserList().ToList())
                if (!User.IsBot)
                {
                    User.ApplyEffect(NumEnable);
                }