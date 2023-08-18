using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        private List<MenuItem> m_SubItems;
        private MenuItem m_ParentItem;
        private string m_Title;

        public MenuItem(MenuItem i_ParentItem, string i_Title)
        {
            m_Title = i_Title;
            m_ParentItem = i_ParentItem;
            m_SubItems = new List<MenuItem>();
        }

        public List<MenuItem> SubItems
        {
            get
            {
                return m_SubItems;
            }
        }

        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
            }
        }

        public MenuItem Parent
        {
            get
            {
                return m_ParentItem;
            }
            set
            {
                m_ParentItem = value;
            }
        }

        public void Show()
        {
            bool userWantToQuit = false;
            int optionFromMenu;

            while (userWantToQuit == false)
            {
                Console.Clear();
                displayMenu();
                optionFromMenu = validChoice();
                if (optionFromMenu == 0)
                {
                    userWantToQuit = true;
                }
                else
                {
                    this.SubItems[optionFromMenu - 1].MakeNextStepByItemType();
                }
            }
        }

        private int validChoice()
        {
            string choice = Console.ReadLine();
            int choiceByInt = 0;

            choiceByInt = int.Parse(choice);
            while (choiceByInt < 0 || choiceByInt > this.SubItems.Count)
            {
                Console.WriteLine("your choice is invalid. please try again");
                choice = Console.ReadLine();
                choiceByInt = int.Parse(choice);
            }

            return choiceByInt;
        }

        protected virtual void MakeNextStepByItemType()
        {
            this.Show();
        }

        private void displayMenu()
        {
            int count = 1;

            if (this is MainMenu)
            {
                Console.WriteLine((this as MainMenu).FirstMenuTitle);
            }
            else
            {
                int index = this.Parent.SubItems.IndexOf(this);
                index++;
                Console.WriteLine("{0}. {1}", index.ToString(), this.Title.ToString());
            }
            Console.WriteLine("=======================");
            foreach (MenuItem item in this.SubItems)
            {
                Console.WriteLine("{0}. {1}", count, item.Title.ToString());
                count++;
            }

            if (this is MainMenu)
            {
                Console.WriteLine("0. Exit");
            }
            else
            {
                Console.WriteLine("0. Back");
            }

            if (this is MainMenu)
            {
                Console.WriteLine("Please enter your choice (1-{0} or 0 to exit)", this.SubItems.Count);
            }
            else
            {
                Console.WriteLine("Please enter your choice (1-{0} or 0 to go back)", this.SubItems.Count);
            }
        }
    }
}
