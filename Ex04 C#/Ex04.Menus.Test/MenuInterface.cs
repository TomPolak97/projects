using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class MenuInterface : IChosenActionItemListener
    {
        private MainMenu m_MainMenuInterface;

        public MenuInterface()
        {
            m_MainMenuInterface = buildWholeMenu();
        }

        public MainMenu MainMenu
        {
            get
            {
                return m_MainMenuInterface;
            }
        }

        private MainMenu buildWholeMenu()
        {
            MainMenu mainMenu = new MainMenu("*Interface Main Menu*");
            mainMenu.FirstMenuTitle = "**Interface Main Menu**";

            MenuItem showDateOrTime = new MenuItem(mainMenu, "Show Date/Time");
            MenuItem versionAndSpaces = new MenuItem(mainMenu, "Version and Spaces");

            ActionItem showTime = new ActionItem(showDateOrTime, "Show Time");
            ActionItem showDate = new ActionItem(showDateOrTime, "Show Date");
            ActionItem countSpaces = new ActionItem(versionAndSpaces, "Count Spaces");
            ActionItem showVersion = new ActionItem(versionAndSpaces, "Show Version");

            mainMenu.SubItems.Add(showDateOrTime);
            mainMenu.SubItems.Add(versionAndSpaces);
            showDateOrTime.SubItems.Add(showTime);
            showDateOrTime.SubItems.Add(showDate);
            versionAndSpaces.SubItems.Add(countSpaces);
            versionAndSpaces.SubItems.Add(showVersion);

            countSpaces.AddListener(this);
            showVersion.AddListener(this);
            showTime.AddListener(this);
            showDate.AddListener(this);

            return mainMenu;
        }

        public void MakeAction(ActionItem i_ActionItem)
        {
            string actionTitle = i_ActionItem.Title;

            Console.Clear();
            switch (actionTitle)
            {
                case "Show Time":
                    showTimeMethod(i_ActionItem);
                    break;

                case "Show Date":
                    showDateMethod(i_ActionItem);
                    break;

                case "Count Spaces":
                    countSpacesMethod(i_ActionItem);
                    break;

                case "Show Version":
                    showVersionMethod(i_ActionItem);
                    break;
            }
        }

        private static void showTimeMethod(ActionItem i_ChosenActionItem)
        {
            string time = DateTime.Now.ToShortTimeString();

            Console.WriteLine("The current time is {0}", time);
        }

        private static void showDateMethod(ActionItem i_ChosenActionItem)
        {
            string date = DateTime.Now.ToShortDateString();

            Console.WriteLine("The current date is {0}", date);
        }

        private static void countSpacesMethod(ActionItem i_ChosenActionItem)
        {
            int countSpaces = 0;

            Console.WriteLine("Please enter a sentence");
            string sentence = Console.ReadLine();
            foreach (char c in sentence)
            {
                if (c == ' ')
                {
                    countSpaces++;
                }
            }

            Console.WriteLine("There are {0} spaces in your sentence.", countSpaces);
            Console.WriteLine();
        }

        private static void showVersionMethod(ActionItem i_ChosenActionItem)
        {
            Console.WriteLine("Version: 22.2.4.8950");
            Console.WriteLine();
        }
    }
}