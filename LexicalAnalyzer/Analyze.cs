using System;
using System.IO;
using System.Collections.Generic;

namespace LexicalAnalyzer
{
    public class Analyze
    {
        public string Text { get; set; }
        public int State { get; set; }
        List<List<int>> Rules = new List<List<int>>();
        public string Result { get; set; }
        public Dictionary<string, int> UniqueValues = new Dictionary<string, int>();

        public Analyze(string text)
        {
            Text = text += '\n';
            State = 1;
            Result = "";
            loadTransitionTable();
        }

        public string GetResult()
        {
            int txtIndex = 0;
            string currentTokenValue = "";
            char currentChar = Text[txtIndex];

            while (txtIndex <= Text.Length)
            {
                if (txtIndex < Text.Length)
                    currentChar = Text[txtIndex];

                if (currentChar == '\n')
                    Result += '\n';

                switch (State)
                {
                    case 1:
                        State = getNextState(State, currentChar);
                        if(currentChar != '\n')
                            currentTokenValue += currentChar;
                        txtIndex++;
                        break;
                    case 2:
                    case 3:
                    case 4:
                        State = getNextState(State, currentChar);
                        currentTokenValue += currentChar;
                        txtIndex++;
                        break;
                    case 5:
                    case 35:
                        State = 1;
                        Result += currentTokenValue;
                        currentTokenValue = "";
                        break;
                    case 6:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 7:
                    case 8:
                        State = 1;
                        currentTokenValue = "";
                        break;
                    case 9:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 11:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        txtIndex++;
                        break;
                    case 13:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        if (char.IsDigit(currentChar) || currentChar == '.')
                        {
                            txtIndex++;
                        }
                        break;
                    case 10:
                    case 12:
                    case 14:
                    case 15:
                    case 17:
                    case 18:
                    case 20:
                    case 21:
                    case 23:
                    case 24:
                    case 26:
                    case 27:
                    case 28:
                    case 30:
                    case 31:
                    case 32:
                        Result += getToken(currentTokenValue);
                        currentTokenValue = "";
                        break;
                    case 16:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '=')
                        {
                            txtIndex++;
                        }
                        break;
                    case 19:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '<')
                        {
                            txtIndex++;
                        }
                        break;
                    case 22:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '>')
                        {
                            txtIndex++;
                        }
                        break;
                    case 25:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '+' || currentChar == '=')
                        {
                            txtIndex++;
                        }
                        break;
                    case 29:
                        currentTokenValue += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '-' || currentChar == '=')
                        {
                            txtIndex++;
                        }
                        break;
                    case 33:
                        State = getNextState(State, currentChar);
                        if (currentChar == '_' || char.IsDigit(currentChar) || char.IsLetter(currentChar)
                            || currentChar == '\n' || currentChar == ' ')
                        {
                            currentTokenValue += currentChar;

                            if (currentChar == ' ')
                                Result += ' ';

                            txtIndex++;
                        }
                        break;
                    case 34:
                        if (Keywords.isKeyword(currentTokenValue))
                            Result += currentTokenValue;
                        else
                            Result += getToken(currentTokenValue);
                        currentTokenValue = "";
                        break;
                }
            }

            return Result;
        }

        private int getNextState(int currentState, char currentChar)
        {
            return Rules[currentState - 1][getCharIndex(currentChar)];
        }

        private string getToken(string currentTokenValue)
        {
            var currentState = State;
            State = 1;

            if (currentTokenValue == string.Empty)
                return string.Empty;

            if (UniqueValues.ContainsKey(currentTokenValue))
            {
                return currentTokenValue;
            }

            UniqueValues.Add(currentTokenValue, 1);
            switch (currentState)
            {
                case 10:
                    return "<LITERAL> " + currentTokenValue;
                case 12:
                    return "<FLOAT> " + currentTokenValue;
                case 14:
                    return "<INTEGER> " + currentTokenValue;
                case 15:
                case 18:
                case 21:
                case 24:
                case 28:
                case 32:
                    return "<OPR> " + currentTokenValue;
                case 34:
                    return "<Identificator> " + currentTokenValue;
                default:
                    return "";
            }
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

                Rules.Add(temp);
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


