using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebBrainfuck.App_Start
{
    class BrainFuckInterpreter
    {
        //private static string VER = "0.0.0.1";
        private static readonly int BUFSIZE = 30000;
        private int[] buf = new int[BUFSIZE];
        private int ptr { get; set; }
        private bool echo { get; set; }

        public BrainFuckInterpreter()
        {
            this.ptr = 0;
            this.Reset();
        }

        public void Reset()
        {
            Array.Clear(this.buf, 0, this.buf.Length);
        }

        public void Interpret(string s)
        {
            int i = 0;
            int right = s.Length;
            while (i < right)
            {
                switch (s[i])
                {
                    case '>':
                        {
                            this.ptr++;
                            if (this.ptr >= BUFSIZE)
                            {
                                this.ptr = 0;
                            }
                            break;
                        }
                    case '<':
                        {
                            this.ptr--;
                            if (this.ptr < 0)
                            {
                                this.ptr = BUFSIZE - 1;
                            }
                            break;
                        }
                    case '.':
                        {
                            Console.Write((char)this.buf[this.ptr]);
                            break;
                        }
                    case '+':
                        {
                            this.buf[this.ptr]++;
                            break;
                        }
                    case '-':
                        {
                            this.buf[this.ptr]--;
                            break;
                        }
                    case '[':
                        {
                            if (this.buf[this.ptr] == 0)
                            {
                                int loop = 1;
                                while (loop > 0)
                                {
                                    i++;
                                    char c = s[i];
                                    if (c == '[')
                                    {
                                        loop++;
                                    }
                                    else
                                        if (c == ']')
                                        {
                                            loop--;
                                        }
                                }
                            }
                            break;
                        }
                    case ']':
                        {
                            int loop = 1;
                            while (loop > 0)
                            {
                                i--;
                                char c = s[i];
                                if (c == '[')
                                {
                                    loop--;
                                }
                                else
                                    if (c == ']')
                                    {
                                        loop++;
                                    }
                            }
                            i--;
                            break;
                        }
                    case ',':
                        {
                            // read a key
                            ConsoleKeyInfo key = Console.ReadKey(this.echo);
                            this.buf[this.ptr] = (int)key.KeyChar;
                            break;
                        }
                }
                i++;
            }
        }

        static void Main(string[] args)
        {
            string str = ">++++[>++++++<-]>-[[<+++++>>+<-]>-]<<[<]>>>>--.<<<-.>>>-.<.<.>---.<<+++.>>>++.<<---.[>]<<.";
            BrainFuckInterpreter bf = new BrainFuckInterpreter();
            bf.Interpret(str);
        }
    }
}