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
            string currentToken = "";
            char currentChar = Text[txtIndex];

            while (txtIndex <= Text.Length)
            {
                if (txtIndex < Text.Length)
                    currentChar = Text[txtIndex];

                switch (State)
                {
                    case 1:
                        State = getNextState(State, currentChar);
                        if (currentChar == '\n')
                            Result += '\n';
                        if(State == 33)
                        {
                            Result += currentToken;
                            currentToken = "";
                            currentToken += currentChar;
                        }
                        else
                        {
                            Result += currentChar;
                        }
                        txtIndex++;
                        break;
                    case 2:
                    case 3:
                    case 4:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '\n')
                            Result += '\n';
                        txtIndex++;
                        break;
                    case 5:
                        State = 1;
                        Result += currentToken;
                        currentToken = "";
                        break;
                    case 6:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '\n')
                            Result += '\n';
                        txtIndex++;
                        break;
                    case 7:
                    case 8:
                        State = 1;
                        currentToken = "";
                        break;
                    case 9:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '\n')
                            Result += '\n';
                        txtIndex++;
                        break;
                    case 10:
                        State = 1;
                        currentToken = "";
                        Result += "<STR>";
                        break;
                    case 11:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '\n')
                            Result += '\n';
                        txtIndex++;
                        break;
                    case 12:
                        State = 1;
                        currentToken = "";
                        Result += "<FLOAT>";
                        break;
                    case 13:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (char.IsDigit(currentChar) || currentChar == '.')
                        {
                            if (currentChar == '\n')
                                Result += '\n';
                            txtIndex++;
                        }
                        break;
                    case 14:
                        State = 1;
                        currentToken = "";
                        Result += "<INTEGER>";
                        break;
                    case 15:
                        State = 1;
                        currentToken = "";
                        Result += "<OPR>";
                        break;
                    case 16:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '=')
                        {
                            if (currentChar == '\n')
                                Result += '\n';
                            txtIndex++;
                        }
                        break;
                    case 17:
                    case 18:
                        State = 1;
                        currentToken = "";
                        Result += "<OPR>";
                        break;
                    case 19:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '<')
                        {
                            if (currentChar == '\n')
                                Result += '\n';
                            txtIndex++;
                        }
                        break;
                    case 20:
                    case 21:
                        State = 1;
                        currentToken = "";
                        Result += "<OPR>";
                        break;
                    case 22:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '>')
                        {
                            if (currentChar == '\n')
                                Result += '\n';
                            txtIndex++;
                        }
                        break;
                    case 23:
                    case 24:
                        State = 1;
                        currentToken = "";
                        Result += "<OPR>";
                        break;
                    case 25:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '+' || currentChar == '=')
                        {
                            if (currentChar == '\n')
                                Result += '\n';
                            txtIndex++;
                        }
                        break;
                    case 26:
                    case 27:
                    case 28:
                        State = 1;
                        currentToken = "";
                        Result += "<OPR>";
                        break;
                    case 29:
                        currentToken += currentChar;
                        State = getNextState(State, currentChar);
                        if (currentChar == '-' || currentChar == '=')
                        {
                            if (currentChar == '\n')
                                Result += '\n';
                            txtIndex++;
                        }
                        break;
                    case 30:
                    case 31:
                    case 32:
                        State = 1;
                        currentToken = "";
                        Result += "<OPR>";
                        break;
                    case 33:

                        State = getNextState(State, currentChar);
                        if (currentChar == '_' || char.IsDigit(currentChar) || char.IsLetter(currentChar)
                            || currentChar == '\n' || currentChar == ' ')
                        {
                            currentToken += currentChar;
                            if (currentChar == '\n')
                                Result += '\n';

                            if (currentChar == ' ')
                                Result += ' ';

                            txtIndex++;
                        }
                        break;
                    case 34:
                        if (Keywords.isKeyword(currentToken))
                            Result += currentToken;
                        else
                            Result += "<ID>";
                        State = 1;
                        currentToken = "";
                        break;
                }
            }

            return Result;
        }

        private int getNextState(int currentState, char currentChar)
        {
            return Rules[currentState - 1][getCharIndex(currentChar)];
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


