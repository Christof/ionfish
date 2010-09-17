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
        private IEnumerable<Type> mGames;
        public Game Game { get; private set; }
        public bool CloseAll { get; private set; }

        public DemoSelection()
        {
            var assembly = Assembly.GetExecutingAssembly();
            mGames = assembly.GetTypes().Where(x => x.BaseType == typeof (Game));
            InitializeComponent();
            SelectionBox.Items.AddRange(mGames.Select(x => x.Name).ToArray());
            SelectionBox.SelectedItem = mGames.First().Name;
            SelectionBox.KeyDown += SelectionBoxKeyDown;
        }

        private void SelectionBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectGame();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Die();
            }
        }

        private void Die()
        {
            CloseAll = true;
            Close();
        }

        private void SelectGame()
        {
            var gameType = mGames.Where(x => x.Name == (string)SelectionBox.SelectedItem).SingleOrDefault();
            Game = ((Game) Activator.CreateInstance(gameType));
            Close();
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            SelectGame(); 
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Die();
        }
    }
}
