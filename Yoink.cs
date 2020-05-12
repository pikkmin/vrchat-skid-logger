using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MelonLoader;
using System.IO;

namespace SkidMod
{

    public class mButton
    {
        private static GameObject mbtn = null;
        public GameObject GameObj;
        public Button Btn;
        public Text Title;


        public static GameObject MenuBTN
        {
            get
            {
                if (mbtn == null)
                {

                    GameObject l = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/ReportWorldButton");
                    GameObject p = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/");
                    MelonModLogger.Log(p.GetComponentsInChildren<Transform>().ToString());
                    var newMenuButton = (GameObject)GameObject.Instantiate(l, p.transform);
                    MelonModLogger.Log(newMenuButton.GetType().ToString());
                    newMenuButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    newMenuButton.SetActive(false);
                    var pos = newMenuButton.transform.localPosition;
                    newMenuButton.transform.position = new Vector3(pos.x, pos.y + 150f);
                    mbtn = newMenuButton;
                }
                return mbtn;
            }
        }

        public static mButton Create(string ButtonTitle, float x, float y)
        {
            var list = new mButton();
            list.GameObj = GameObject.Instantiate(MenuBTN.gameObject, MenuBTN.transform);
            list.Btn = list.GameObj.GetComponentInChildren<Button>();
            
            list.Btn.onClick.RemoveAllListeners();
            var pos = list.GameObj.transform.localPosition;
            list.GameObj.transform.localPosition = new Vector3(pos.x + x, pos.y + (80f * y));
            list.Title = list.GameObj.GetComponentInChildren<Text>() ;
            list.Title.text = ButtonTitle;
            list.GameObj.SetActive(true);
            return list;
        }

        public void SetAction(Action v)
        {
          
            Btn.onClick = new Button.ButtonClickedEvent();
            Btn.onClick.AddListener(v);
        }
    }



public static class BuildInfo
    {
        public const string Name = "PikSkidMod"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Pikmin"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.1"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }
    public class Skid :MelonMod
    {
        public static mButton m;
        public static bool avaLogger = true;
        static float last_routine;
        public static Boolean debug = false;
        public List<string> logList;
        System.Random random = new System.Random();
        string Session;

        public void saveLog()
        {
            logList = Utils.logAvas();
            MelonModLogger.Log(logList.Count.ToString());

            string now = DateTime.Now.ToString("yyyy-dd-M--HH");
            string path = "Log-" + Session + '-' + now + ".txt";
            File.WriteAllText(path, String.Empty);
            using (StreamWriter sw = File.AppendText(path))
            {

                foreach (string s in logList)
                {
                    if (debug) { MelonModLogger.Log(s); }

                    sw.WriteLine(s);
                }
                sw.Close();
            }
        }


        public override void VRChat_OnUiManagerInit()
        {
            Session =  random.Next(0, 999).ToString();

            var quickMenu = Utils.Get_quick_menu().transform.Find("ShortcutMenu");

            if(quickMenu != null  && Utils.Get_quick_menu().transform.Find("ShortcutMenu/BuildNumText") != null )
            {
                var menu = Utils.Make_Btn("Ban furries", -4f, 4f,
                    new Action(() =>
                    {
                        saveLog();
                    }
                    ));

            }

        }
        public override void OnUpdate()
        {
            try
            {
                if (Time.time > last_routine && Utils.get_player_manager() != null)
                {
                    last_routine = Time.time + 30;

                    if (avaLogger)
                    {
                        saveLog();
                    }

                    MelonModLogger.Log("logging");
                }
            }
            catch (Exception e)
            {
                MelonModLogger.Log("Error in main loop " + e.Message + " in " + e.Source + " Stack: " + e.StackTrace);
            }
        }
    }

}