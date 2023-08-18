using System;

namespace Ex04.Menus.Delegates
{
    public class MainMenu : MenuItem
    {
        private string m_FirstMenuTitle;

        public MainMenu(string i_Title) : base(null, i_Title) { }

        public string FirstMenuTitle
        {
            get
            {
                return m_FirstMenuTitle;
            }
            set
            {
                m_FirstMenuTitle = value;
            }
        }
    }
}
