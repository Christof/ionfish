using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Graphics;

namespace Sandbox
{
    public partial class DemoSelection : Form
    {
        private readonly IEnumerable<Type> mGames;
        public Game Game { get; private set; }
        public bool CloseAll { get; private set; }

        public DemoSelection()
        {
            mGames = GetGameTypes();
            InitializeComponent();
            SelectionBox.Items.AddRange(mGames.Select(x => x.Name).ToArray());
            SelectionBox.SelectedItem = mGames.First().Name;
            SelectionBox.KeyDown += SelectionBoxKeyDown;
            CloseAll = true;
        }

        private static IEnumerable<Type> GetGameTypes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetTypes().Where(x => x.BaseType == typeof (Game));
        }

        private void SelectionBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectGame();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void SelectGame()
        {
            var gameType = mGames.Where(x => x.Name == (string)SelectionBox.SelectedItem).SingleOrDefault();
            Game = ((Game) Activator.CreateInstance(gameType));
            CloseAll = false;
            Close();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            SelectGame(); 
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
