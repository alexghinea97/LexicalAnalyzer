using System;
using System.Collections.Generic;

namespace LexicalAnalyzer
{
    public static class Keywords
    {
        public static readonly List<string> sKeywords = new List<string>{
                "using","import","include","asm","auto","bool","break","case","catch","char","class","const","const_cast",
                "continue","default","delete","do","double","dynamic_cast","else","enum","explicit",
                "export","extern","false","float","for","friend","goto","if","inline","int","long",
                "main","mutable","namespace","new","operator","private","protected","public",
                "register","reinterpret_cast","return","short","signed","sizeof","static",
                "static_cast","struct","switch","template","this","throw","true","try","typedef",
                "typeid","typename","union","unsigned","using","virtual","void","volatile","wchar_t","while"};

        public static bool isKeyword(string sToken)
        {
            if (sToken.Length > 16 || (sToken).Length == 0)
                return false;

            return sKeywords.Exists(element => (sToken.ToLower()).Remove(sToken.Length - 1) == element) ||
                sKeywords.Exists(element => (sToken.ToLower()) == element);
        }
    }
}
