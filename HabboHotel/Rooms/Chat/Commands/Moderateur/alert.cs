using Akiled.Communication.Packets.Outgoing.Structure;
using Akiled.HabboHotel.GameClients;
                {
                    AkiledEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("publicidad", "L'utente: " + Session.GetHabbo().Username + ", Pub cmd:" + message + ", Clicca qui per andare a vedere.", "event:navigator/goto/" + Session.GetHabbo().CurrentRoomId));
                    return;
                }