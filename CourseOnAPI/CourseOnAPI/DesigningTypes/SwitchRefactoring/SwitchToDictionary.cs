using System;
using System.Collections.Generic;

namespace DesigningTypes.SwitchRefactoring {
    public class DataToDataExample {
        public string DataToData(string state) {
            switch (state) {
                case "Florida":
                    return "Tallahassee";
                case "Idaho":
                    return "Boise";
                case "Arizona":
                    return "Phoenix";
                case "South Carolina":
                    return "Columbia";
                default:
                    throw new ArgumentException();
            }
        }

        public string DataToDataWithDictionary(string state) {
            var stateToCapitolMap = new Dictionary<string, string>() {
                {"Florida", "Tallahassee"},
                {"Idaho", "Boise"},
                {"Arizona", "Phoenix"},
                {"South Carolina", "Columbia"}
            };
            return stateToCapitolMap[state];
        }
    }

    public class DataToActionExample {
        public enum Move {
            Up,
            Down,
            Left,
            Right,
            Combo
        }

        public void Perform(Move move) {
            switch (move) {
                case Move.Up:
                    MoveUp();
                    break;
                case Move.Down:
                    MoveDown();
                    break;
                case Move.Left:
                    MoveLeft();
                    break;
                case Move.Right:
                    MoveRight();
                    break;
                case Move.Combo:
                    MoveUp();
                    MoveUp();
                    MoveDown();
                    MoveDown();
                    break;
            }
        }

        public void PerformWithDictionary(Move move) {
            var moveMap = new Dictionary<Move, Action>() {
                {Move.Up, MoveUp},
                {Move.Down, MoveDown},
                {Move.Left, MoveLeft},
                {Move.Right, MoveRight}, {
                    Move.Combo, () => {
                        MoveUp();
                        MoveUp();
                        MoveDown();
                        MoveDown();
                    }
                }
            };
            moveMap[move]();
        }

        private void MoveRight() {}

        private void MoveLeft() {}

        private void MoveDown() {}

        private void MoveUp() {}
    }
}