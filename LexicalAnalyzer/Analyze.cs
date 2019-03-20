using System;
using System.IO;
using System.Collections.Generic;

namespace LexicalAnalyzer
{
    public class Analyze
    {
        public string Text { get; set; }
        public int State { get; set; }
        List<List<int>> rules = new List<List<int>>();

        public Analyze(string text)
        {
            Text = text;
            State = 1;
        }

        public string GetResult()
        {
            loadTransitionTable();
            int txtIndex = 0;
            string result = "";
            string currentToken = "";
            char currentChar = Text[txtIndex];

            while (txtIndex <= Text.Length)
            {
                if(txtIndex < Text.Length)
                    currentChar = Text[txtIndex];

                switch (State)
                {
                    case 1:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 2:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 3:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 4:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 5:
                        State = 1;
                        result += currentToken;
                        currentToken = "";
                        txtIndex++;
                        break;
                    case 6:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 7:
                        State = 1;
                        currentToken = "";
                        break;
                    case 8:
                        State = 1;
                        currentToken = "";
                        break;
                    case 9:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 10:
                        State = 1;
                        currentToken = "";
                        result += "<STR>\n";
                        break;
                }
            }

            return result;
        }

        private int getNextState(int currentState, char currentChar)
        {
            return rules[currentState - 1][getCharIndex(currentChar)];
        }

        private void loadTransitionTable()
        {
            string text = File.ReadAllText(FilePaths.Transitiontable);

            foreach (var item in text.Split('\n'))
            {
                var temp = new List<int>();

                foreach (var itm in item.Trim().Split(' '))
                {
                    temp.Add(Convert.ToInt32(itm));
                }

                rules.Add(temp);
            }
        }

        private int getCharIndex(char c)
        {
            if (char.IsLetter(c))
                return 1;

            if (char.IsDigit(c))
                return 2;

            switch(c)
            {
                case '.':
                    return 3;
                case '"':
                    return 4;
                case '\'':  
                    return 5;
                case '_':
                    return 6;
                case '+':
                    return 7;
                case '=':
                    return 8;
                case '-':
                    return 9;
                case '%':
                    return 10;
                case '!':
                    return 11;
                case '>':
                    return 12;
                case '<':
                    return 13;
                case '/':
                    return 14;
                case '*':
                    return 15;
                case '\n':
                    return 16;
                default:
                    return 0;
            }
        }
    }
}
