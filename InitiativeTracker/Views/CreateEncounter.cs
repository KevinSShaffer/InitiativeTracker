using InitiativeTracker.Components;
using InitiativeTracker.Helpers.Interfaces;
using InitiativeTracker.Models;
using InitiativeTracker.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker.Views
{
    public class CreateEncounter : Component
    {
        private int activeComponentIndex = 0;
        private Component activeComponent => components[activeComponentIndex];
        private IEnumerable<string> monsterNames => encounter.MonsterGroups.Select(group => listBoxLine(group));
        private readonly Encounter encounter = new Encounter();
        private const string quantityLabel = "Quantity: ";
        private readonly List<Component> components = new List<Component>();
        private readonly IRenderer renderer;
        private readonly SearchBox searchBox;
        private readonly TextBox textBox;
        private readonly ListBox listBox;

        public CreateEncounter(IRenderer renderer, IGuesser<string> guesser)
        {
            this.renderer = renderer;
            searchBox =  new SearchBox(renderer, guesser, new Point(2, 1), 30, 19);
            textBox = new TextBox(renderer, new Point(quantityLabel.Length + 2, 21), 2);
            listBox = new ListBox(renderer, new Point(2, 23), 30, 8, monsterNames);

            components.Add(searchBox);
            components.Add(textBox);
            components.Add(listBox);

            CharacterKeyPressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            LeftArrowPressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            RightArrowPressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            BackspacePressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            DeletePressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            HomePressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            EndPressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            UpArrowPressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            DownArrowPressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            EnterPressed += (o, e) => activeComponent.KeyPressed(e.KeyPressed);
            EnterPressed += CreateEncounter_EnterPressed;
            TabPressed += (o, e) => nextActiveComponent();
            ShiftTabPressed += (o, e) => previousActiveComponent();

            renderer.SetWindowSize(32, 32);
        }

        private void CreateEncounter_EnterPressed(object sender, KeyPressedEventArgs e)
        {
            if (activeComponent == searchBox)
            {
                tryAddMonster();
                nextActiveComponent();
            }
            else if (activeComponent == textBox)
            {
                tryAddMonster();
                previousActiveComponent();
            }
            // else if done button pressed
        }

        private string listBoxLine(MonsterGroup monsterGroup)
        {
            string name = monsterGroup.Monster.Name, quantity = monsterGroup.Quantity.ToString();
            return $"{name.PadRight(27 - quantity.Length, ' ')}{quantity}";
        }

        private void tryAddMonster()
        {
            if (!string.IsNullOrWhiteSpace(searchBox.Selected) &&
                !string.IsNullOrWhiteSpace(textBox.Text) &&
                int.TryParse(textBox.Text, out int quantity))
            {
                encounter.MonsterGroups.Add(new MonsterGroup(new Monster(searchBox.Selected), quantity));
                searchBox.Clear();
                textBox.Clear();
            }
        }

        private void nextActiveComponent()
        {
            if (activeComponentIndex == components.Count - 1)
                activeComponentIndex = 0;
            else
                activeComponentIndex++;
        }

        private void previousActiveComponent()
        {
            if (activeComponentIndex == 0)
                activeComponentIndex = components.Count - 1;
            else
                activeComponentIndex--;
        }

        public override void Draw()
        {
            searchBox.Draw();
            renderer.DrawText(new Point(3, 21), quantityLabel);
            textBox.Draw();
            listBox.Draw();

            activeComponent.Focus();
        }

        public override void Focus()
        {
            activeComponent.Focus();
        }
    }
}
