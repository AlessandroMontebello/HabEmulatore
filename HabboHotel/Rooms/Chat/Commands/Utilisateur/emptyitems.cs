using Akiled.HabboHotel.GameClients;
            bool EmptyAll = (Params.Length > 1 && Params[1] == "all");

            Session.GetHabbo().GetInventoryComponent().ClearItems(EmptyAll);