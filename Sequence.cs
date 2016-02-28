using System;
using System.Text.RegularExpressions;

namespace Application
{
    class Sequence
    {
        static string checkInput(string str)
        {
            if (str == "1")
                Console.Write("");

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return null;
            }

            if (str == "0")
                return null;

            foreach (char i in str)
            {
                if ((i != '0') && (i != '1'))
                {
                    str = str.Replace(i.ToString(), new String('1', int.Parse(i.ToString())));
                }
            }
            if (str.Length > 65)
                return null;
            return str;
        }

        static string createArray1(string str)
        {
            string outStr = "";
            int count = 0;
            while ((!str.Equals("1")))
            {
                if (str.Equals("0"))
                    return ("-");
                count++;
                if (count == 500)
                {
                    return "-";
                }
                if (str.Substring(str.Length-2, 2) == "10")
                {
                    str = str.Substring(0, str.Length-1);
                    outStr = "A" + "\n" + outStr;
                    continue;
                }
                if ((str.Length % 2 == 0) && (str.Substring(0, str.Length / 2) == str.Substring(str.Length / 2, str.Length / 2)))
                {
                    str = str.Substring(0, str.Length / 2);
                    outStr = "B" + "\n" + outStr;
                    continue;
                }
                else if (str.Contains("0"))
                {
                    int index = str.IndexOf("0");
                    outStr = "C" + index + "\n" + outStr;
                    str = str.Remove(index, 1);
                    str = str.Insert(index, "111");
                    continue;
                }
                else
                {
                    str = str + "00";
                    outStr = "D" + (str.Length - 2) + "\n" + outStr;
                }         
            }
            return outStr;
        }

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string inputStr = args[0];
                string str = checkInput(inputStr);
                if (str != null)
                {
                    Console.Write(createArray1(str));
                    Console.ReadKey();
                }
                else
                    Console.Write("-");
            }
            else Console.Write("-");
        }
    }
}