using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC;

namespace SkidMod
{
    class Utils
    {
        public static List<string> yoinkList = new List<string>();

        public static QuickMenu Get_quick_menu()
        {
            return QuickMenu.prop_QuickMenu_0;
        }
        public static GameObject Make_Btn(String name,float x,float y, Action listener)
        {
            var quickMenu = Get_quick_menu();
            Transform b = UnityEngine.Object.Instantiate<GameObject>(quickMenu.transform.Find("CameraMenu/BackButton").gameObject).transform;
            float num = quickMenu.transform.Find("UserInteractMenu/ForceLogoutButton").localPosition.x - quickMenu.transform.Find("UserInteractMenu/BanButton").localPosition.x;
            float num2 = quickMenu.transform.Find("UserInteractMenu/ForceLogoutButton").localPosition.x - quickMenu.transform.Find("UserInteractMenu/BanButton").localPosition.x;
            b.localPosition = new Vector3(b.localPosition.x + num * x, b.localPosition.y + num2 * y, b.localPosition.z);
            b.name = name;
            b.SetParent(quickMenu.transform.Find("ShortcutMenu"), false);
            b.GetComponentInChildren<Text>().text = name;
            b.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            b.GetComponent<Button>().onClick.AddListener(listener);
            b.GetComponentInChildren<UiTooltip>().text = name;

            return b.gameObject;
        }

        public static PlayerManager get_player_manager()
        {
            return PlayerManager.prop_PlayerManager_0;
        }

        public static Il2CppSystem.Collections.Generic.List<Player> get_all_player()
        {
            if (PlayerManager.field_Private_Static_PlayerManager_0 == null) return null;
            return PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0;
        }

        public static List<string> logAvas()
        {
            var users_active = get_all_player();
            if (users_active == null) return null;
            for (var c = 0; c < users_active.Count; c++)
            {
                var user = users_active[c];
                if (user == null || user.prop_VRCAvatarManager_0 == null || user.field_Private_APIUser_0 == null) continue;
                if (user.field_Private_VRCAvatarManager_0 == null) continue;
                if (user.field_Private_VRCAvatarManager_0.field_Private_ApiAvatar_0 == null) continue;
                if (VRCPlayer.field_Internal_Static_VRCPlayer_0 == null) continue;
                if (user.field_Private_APIUser_0.id == VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_Player_0.field_Private_APIUser_0.id) continue;
                if (user.prop_VRCAvatarManager_0.enabled == false) continue;
                var contains = yoinkList.Contains(user.field_Private_VRCAvatarManager_0.field_Private_ApiAvatar_0.id);
                if(contains == false)
                {
                    yoinkList.Add(user.field_Private_VRCAvatarManager_0.field_Private_ApiAvatar_0.id);
                }
            }
            return yoinkList;
        }

    }
}
