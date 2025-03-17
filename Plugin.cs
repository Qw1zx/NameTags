using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BananaWatch;
using BananaWatch.Pages;
using BepInEx;
using NameTags;
using UnityEngine;

namespace Template
{
    [BepInPlugin("Qwizx.NameTags", "NameTags", "1.0.0")]
    public class Template : BaseUnityPlugin { }
    public class TemplatePage : BananaWatchPage
    {
        public override string Title => "NameTags";
        public override bool PublicPage => true;

        private SelectionHandler selectionHandler = new SelectionHandler();

        private List<string> menuOptions = new List<string>
        {
            "Turn On",
            "Turn Off"
        };

        public override void PageOpened()
        {
            selectionHandler.maxIndex = menuOptions.Count - 1;
            selectionHandler.currentIndex = 0;
        }

        public override string RenderScreenContent()
        {
            string header = "<color=#00ff00>==</color> NameTags <color=#00ff00>==</color>\n\n";

            string content = header;
            for (int i = 0; i < menuOptions.Count; i++)
            {
                content += selectionHandler.SelectionArrow(i, menuOptions[i]) + "\n";
            }

            return content;
        }
        public override void ButtonPressed(BananaWatchButton buttonType)
        {
            switch (buttonType)
            {
                case BananaWatchButton.Up:
                    selectionHandler.MoveSelectionUp();
                    break;
                case BananaWatchButton.Down:
                    selectionHandler.MoveSelectionDown();
                    break;
                case BananaWatchButton.Enter:
                    NameTag name = FindObjectOfType<NameTag>();

                    if (name != null)
                    {
                        switch (selectionHandler.currentIndex)
                        {
                            case 0: 
                                name.enabled = true;
                                Debug.Log("NameTags Enabled");
                                break;

                            case 1:
                                name.enabled = false;
                                Debug.Log("NameTags Disabled");
                                break;
                        }
                    }
                    break;

                case BananaWatchButton.Back:
                    NavigateToMainMenu();
                    break;
            }
        }

    }
}