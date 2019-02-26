using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace AddCopyright2Image
{
    class Program
    {
        static string bitmapPath; // -i
        static string text; // -c
        static int xPosition; // -x
        static int yPosition; // -y
        static string fontName; // -n
        static float fontSize; // -s
        static string fontStyle; // -t
        static string colorName1; // -c1
        static string colorName2; // -c2
        static string flgBackup; // -b

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Dictionary<string, string> clArgValues = new Dictionary<string, string>();
                clArgValues = parseArgs(args);
                validateArgs(clArgValues);

                foreach (var pair in clArgValues)
                {
                    Console.WriteLine("Key: {0}; Val: {1};", pair.Key, pair.Value);
                }
            }
            else
            {
                Dictionary<string, string> clArgValues = new Dictionary<string, string>();
                clArgValues = parseArgs(args);
                validateArgs(clArgValues);
            }

            string curPath = Directory.GetCurrentDirectory();
            //Console.WriteLine(curPath); // + "\\" + bitmapPath);
            //Console.WriteLine(bitmapPath);
            //Console.WriteLine(bitmapPath.Contains(@"*").ToString());

            if (!bitmapPath.Contains(@"*"))
            {
                Bitmap temp = (Bitmap)Bitmap.FromFile(bitmapPath);
                Bitmap bmap = (Bitmap)temp.Clone();
                InsertText(bmap);
                SaveBitmap((Bitmap)bmap.Clone(), bitmapPath);
            }
            else
            {
                //string curPath = Directory.GetCurrentDirectory();
                //Console.WriteLine(curPath + "\\" + bitmapPath);

                string[] files = Directory.GetFiles(curPath, bitmapPath);
                foreach (string file in files)
                {
                    Console.WriteLine("Working file: " + file);
                    Bitmap temp = (Bitmap)Bitmap.FromFile(file);
                    Bitmap bmap = (Bitmap)temp.Clone();
                    InsertText(bmap);
                    SaveBitmap((Bitmap)bmap.Clone(), file);
                }
            }

        }

        static Dictionary<string, string> parseArgs(string[] clArgs)
        {
            Dictionary<string, string> clArgValues = new Dictionary<string, string>();

            for (int i = 0; i < clArgs.Length; i += 2)
            {
                clArgValues.Add(clArgs[i], clArgs[i + 1]);
            }

            return clArgValues;
        }

        static void validateArgs(Dictionary<string, string> argDict)
        {
            //static string bitmapPath; // -i
            if (!argDict.ContainsKey("-i"))
            {
                Console.WriteLine("No image (-i) argument passed in to the application... exiting!");
                ShowHelp();
            }
            else
            {
                bitmapPath = argDict["-i"];
                Console.WriteLine("Image to add text to: " + bitmapPath);
            }

            //static string text; // -c
            if (!argDict.ContainsKey("-c"))
            {
                Console.WriteLine("No copywrite text (-c) argument passed in to the application... exiting!");
                ShowHelp();
            }
            else
            {
                text = argDict["-c"];
                Console.WriteLine("Copywrite text: " + text);
            }
            //static int xPosition; // -x
            if (!argDict.ContainsKey("-x"))
            {
                Console.WriteLine("No position x (-x) argument passed in to the application. Setting to zero (0),");
                xPosition = 0;
            }
            else
            {
                xPosition = int.Parse(argDict["-x"]);
                Console.WriteLine("x Position: " + xPosition.ToString());
            }
            //static int yPosition; // -y
            if (!argDict.ContainsKey("-y"))
            {
                Console.WriteLine("No position y (-y) argument passed in to the application. Setting to zero (0),");
                yPosition = 0;
            }
            else
            {
                yPosition = int.Parse(argDict["-y"]);
                Console.WriteLine("y Position: " + yPosition.ToString());
            }
            //static string fontName; // -n
            if (!argDict.ContainsKey("-n"))
            {
                Console.WriteLine("No font name (-n) argument passed in to the application. Defaulting to Times New Roman.");
            }
            else
            {
                fontName = argDict["-n"];
                Console.WriteLine("Font Name: " + fontName);
            }
            //static float fontSize; // -s
            if (!argDict.ContainsKey("-s"))
            {
                Console.WriteLine("No font size (-s) argument passed in to the application. Setting to ten (10),");
                fontSize = 10.0F;
            }
            else
            {
                fontSize = float.Parse(argDict["-s"]);
                Console.WriteLine("Font size: " + fontSize.ToString());
            }
            //static string fontStyle; // -t
            if (!argDict.ContainsKey("-t"))
            {
                Console.WriteLine("No font style (-t) argument passed in to the application. Defaulting to regular.");
                fontStyle = "regular";
            }
            else
            {
                fontStyle = argDict["-t"];
                Console.WriteLine("Font Style: " + fontStyle);
            }
            //static string colorName1; // -c1
            if (!argDict.ContainsKey("-c1"))
            {
                Console.WriteLine("No font color 1 (-c1) argument passed in to the application. Defaulting to black.");
            }
            else
            {
                colorName1 = argDict["-c1"];
                Console.WriteLine("Font Color 1: " + colorName1);
            }
            //static string colorName2; // -c2
            if (!argDict.ContainsKey("-c2"))
            {
                Console.WriteLine("No font color 2 (-c2) argument passed in to the application. Defaulting to black.");
            }
            else
            {
                colorName2 = argDict["-c2"];
                Console.WriteLine("Font Color 2: " + colorName1);
            }
            //static string flgBackup; // -b
            if (!argDict.ContainsKey("-b"))
            {
                Console.WriteLine("No backup flag (-b) argument passed in to the application. Defaulting to Y (make backup before saving).");
                flgBackup = "Y";
            }
            else
            {
                flgBackup = argDict["-b"];
                Console.WriteLine("Create backup of image:: " + colorName2);
            }
        }

        private static void ShowHelp()
        {
            //static string bitmapPath; // -i
            //static string text; // -c
            //static int xPosition; // -x
            //static int yPosition; // -y
            //static string fontName; // -n
            //static float fontSize; // -s
            //static string fontStyle; // -t
            //static string colorName1; // -c1
            //static string colorName2; // -c2

            Console.WriteLine("AddCopyright2Image");
            Console.WriteLine("Valid arguments are:");
            Console.WriteLine("     -i - image path");
            Console.WriteLine("     -c - copyright text");
            Console.WriteLine("     -x - position x of the text (defaults to 0)");
            Console.WriteLine("     -y - position y of the text (defaults to 0)");
            Console.WriteLine("     -n - font name (defaults to Time New Roman)");
            Console.WriteLine("     -s - font size (defaults to 10)");
            Console.WriteLine("     -t - font style (defaults to regular)");
            Console.WriteLine("     -c1 - font color 1 (defaults to black)");
            Console.WriteLine("     -c1 - font color 2 (defaults to black)");
            Console.WriteLine("     -b - backup flag (defaults to Y, backup; do not overwrite current image)");
            Environment.Exit(1);
        }

        private static void InsertText(Bitmap bmap)
        {
            Graphics gr = Graphics.FromImage(bmap);

            if (string.IsNullOrEmpty(fontName))
                fontName = "Times New Roman";

            if (fontSize.Equals(null))
                fontSize = 10.0F;

            Font font = new Font(fontName, fontSize);

            if (!string.IsNullOrEmpty(fontStyle))
            {
                FontStyle fStyle = FontStyle.Regular;
                switch (fontStyle.ToLower())
                {
                    case "bold":
                        fStyle = FontStyle.Bold;
                        break;
                    case "italic":
                        fStyle = FontStyle.Italic;
                        break;
                    case "underline":
                        fStyle = FontStyle.Underline;
                        break;
                    case "strikeout":
                        fStyle = FontStyle.Strikeout;
                        break;

                }
                font = new Font(fontName, fontSize, fStyle);
            }

            if (string.IsNullOrEmpty(colorName1))
                colorName1 = "Black";

            if (string.IsNullOrEmpty(colorName2))
                colorName2 = colorName1;

            Color color1 = Color.FromName(colorName1);
            Color color2 = Color.FromName(colorName2);

            int gW = (int)(text.Length * fontSize);
            gW = gW == 0 ? 10 : gW;

            LinearGradientBrush LGBrush = new LinearGradientBrush(new Rectangle(0, 0, gW, (int)fontSize), color1, color2, LinearGradientMode.Vertical);
            gr.DrawString(text, font, LGBrush, xPosition, yPosition);
        }

        private static void SaveBitmap(Bitmap bmpBitmap, string saveFilePath)
        {
            bitmapPath = saveFilePath;
            if (flgBackup == "N")
            {
                if (System.IO.File.Exists(saveFilePath))
                    System.IO.File.Delete(saveFilePath);
            }
            else
            {
                string curPath = Directory.GetCurrentDirectory();
                string buPath = "Copyrighted";
                int pathIndex = saveFilePath.LastIndexOf("\\");

                //Console.WriteLine(saveFilePath);
                //Console.WriteLine(pathIndex);

                if (!Directory.Exists(curPath + "\\" + buPath))
                    Directory.CreateDirectory(curPath + "\\" + buPath);

                if (pathIndex > 0)
                {
                    saveFilePath = curPath + "\\" + buPath + saveFilePath.Substring(pathIndex);
                }
                else
                {
                    saveFilePath = curPath + "\\" + buPath + "\\" + saveFilePath;
                }
                
                //saveFilePath.Substring(0, saveFilePath.Length - 4) + "_Copyright" + saveFilePath.Substring(saveFilePath.Length - 4, 4);
                Console.WriteLine("Saving udpated file to: " + saveFilePath);
                if (System.IO.File.Exists(saveFilePath))
                {
                    //System.IO.File.Copy(saveFilePath, saveFilePath);
                    System.IO.File.Delete(saveFilePath);
                }
            }
            bmpBitmap.Save(saveFilePath);
        }
    }
}
