using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            int N = 80;
            string vhod;
            string vihod;
            string text2="";

            if (args.Length == 2)
            {
                vhod = args[0].ToString();
                vihod = args[1].ToString();
            }
            else
            {
                return;
            }

           // string text = System.IO.File.ReadAllText(vhod);
            
            Encoding enc = Encoding.GetEncoding(1251);
            StreamReader sr = new StreamReader(vhod, enc);
            string text = sr.ReadToEnd();

          //  string[] lines = text.Split('\n');
          //  text = text.Replace("\r\n", "");

            //делим на абзацы

            while (text.Length != 0)
            {
                int k = text.IndexOf("\r\n");
                if (k == 0)
                {
                    text2 += "\r\n";
                    text = text.Remove(0, 2);
                }
                else
                {
                    if ((text.IndexOf("\r\n ") != -1) || (text[k + 2] >= 65 && text[k + 2] <= 90))
                    {
                        string l = text.Substring(0, k);
                        text2 += l + "\r\n";
                        text = text.Remove(0, k + 2);
                    }
                    else
                    {
                        if (k != -1)
                        {
                            string l = text.Substring(0, k);
                            text2 += l;
                            text = text.Remove(0, k + 2);
                        }
                        else
                        {
                            text2 += text;
                            break;
                        }
                    }
                }
            }

            text = text2;
            text2 = "";

            //добавляем пробелы в начале каждого абзаца
            text = "    " + text.Replace("\r\n", "\r\n    ");

            //делаем в абзаце строки равными N

            N = N - 1;

            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length < N / 2)
                {
                    text2 += lines[i] + "\n";
                }
                else
                {
                    while (lines[i].Length != 0)
                    {
                        if (lines[i].Length < N / 2)
                        {
                            text2 += lines[i] + "\n";
                            lines[i] = lines[i].Remove(0);
                        }
                        else
                        {
                            string l;
                            if (lines[i].Length < N + 1)
                            {
                                l = lines[i].Substring(0, lines[i].Length);
                                text2 += l + "\n";
                                lines[i] = lines[i].Remove(0, lines[i].Length);
                                break;
                            }
                            else
                            {
                                l = lines[i].Substring(0, N + 1);
                            }
                            if (l.Length > N / 2)
                            {
                                if (l[N] == 32)
                                {
                                    l = l.TrimEnd();
                                    text2 += l + "\r\n";
                                    lines[i] = lines[i].Remove(0, N+1);
                                }
                                else
                                {
                                    int h = l.LastIndexOf(" ");
                                    l = l.Remove(h, l.Length - h);

                                    lines[i] = lines[i].Remove(0, h + 1);

                                    //увеличение строки за счет пробелов

                                    string[] slov = l.Split(' ');
                                    int dl = l.Length;
                                    int kolpr = N - dl;//кол-во пробелов
                                    int kolsl = slov.Length;//кол-во слов

                                    if (kolpr > (kolsl-1))
                                    {
                                        int ost;
                                        if ((slov[0] != "") && (slov[1] != "") && (slov[2] != "") && (slov[3] != ""))
                                        {
                                            int cel = kolpr / (kolsl - 5);
                                            ost = kolpr % (kolsl - 5);

                                            for (int t = 0; t < cel; t++)
                                            {
                                                for (int u = 5; u < slov.Length; u++)
                                                {
                                                    slov[u] = " " + slov[u];
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int cel = kolpr / (kolsl - 1);
                                            ost = kolpr % (kolsl - 1);

                                            for (int t = 0; t < cel; t++)
                                            {
                                                for (int u = 1; u < slov.Length; u++)
                                                {
                                                    slov[u] = " " + slov[u];
                                                }
                                            }
                                        }

                                        l = String.Join(" ", slov);
                                    }

                                    string w = "";
                                    int lo = l.Length;

                                    for (int j = 0; j < (N - lo); j++)
                                    {
                                        l = l.Insert(l.LastIndexOf(" "), " ");
                                        w = w.Insert(0, l.Substring(l.LastIndexOf("  ")));
                                        l = l.Remove(l.LastIndexOf("  "));
                                    }
                                    w = w.Insert(0, l);
                                    text2 += w + "\r\n";
                                }
                            }
                            else
                            {
                                text2 += l + "\r\n";
                                lines[i] = lines[i].Remove(0, lines[i].Length);
                            }
                        }
                    }
                }
            }


            using (StreamWriter aa = new StreamWriter(vihod,false,System.Text.Encoding.GetEncoding(1251)))
               {
                       aa.Write(text2);
                   aa.Close();
            }



              //  System.IO.File.WriteAllText(vihod, text2);
        }
    }
}
