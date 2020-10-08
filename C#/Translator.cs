/**

    * Copyright 2020 William Farrow

    * Permission to use, copy, modify, and/or distribute this software 
    * for any purpose with or without fee is hereby granted, provided 
    * that the above copyright notice and this permission notice appear 
    * in all copies.

    * THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL 
    * WARRANTIES WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED 
    * WARRANTIES OF MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE 
    * AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR 
    * CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM 
    * LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, 
    * NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN 
    * CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Translator
{
    /// <summary>
    /// Translator app class.
    /// Has the ability to translate Morse code into text and vice-versa.
    /// </summary>
    class Translator
    {
        /// <summary>
        /// Readonly Dictionary<char, string> containing the translations.
        /// </summary>
        static readonly Dictionary<char, string> dictTranslations = new Dictionary<char, string>()
        {
            {'A', ".-"},{'B', "-..."},{'C', "-.-."},{'D', "-.."},
            {'E', "."},{'F', "..-."},{'G', "--."},{'H', "...."},
            {'I', ".."},{'J', ".---"},{'K', "-.-"},{'L', ".-.."},
            {'M', "--"},{'N', "-."},{'O', "---"},{'P', ".--."},
            {'Q', "--.-"},{'R', ".-."},{'S', "..."},{'T', "-"},
            {'U', "..-"},{'V', "...-"},{'W', ".--"},{'X', "-..-"},
            {'Y', "-.--"},{'Z', "--.."},{'1', ".----"},{'2', "..---"},
            {'3', "...--"},{'4', "....-"},{'5', "....."},{'6', "-...."},
            {'7', "--..."},{'8', "---.."},{'9', "----."},{'0', "-----"},
            {'.', ".-.-.-"},{',', "--..--"},{'?', "..--.."}
        };
        /// <summary>
        /// Main Method, runs upon executing the application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // check if the end user has provided only 1 command line argument, exit if not.
            if (args.Length != 1)
            {
                Console.WriteLine($"You must specify {( args.Length > 1 ? "only one" : "an" )} argument.\n" +
                    $"\"m2t\" - Morse code to text.\n" +
                    $"\"t2m\" - Text to Morse code.\n" +
                    $"\"help\" - How to use the application.\n" +
                    $"\"translations\" - List of translatable characters." );
                Environment.Exit(1);
            }

            // check if the provided command line argument is "m2t", "t2m", "help", or "translations" and call related methods.
            if(args[0] == "m2t")
            {
                MorseToText();
            }
            else if (args[0] == "t2m")
            {
                TextToMorse();
            }
            else if (args[0] == "help")
            {
                HelpText();
            }
            else if (args[0] == "translations")
            {
                Translations();
            }
            else
            {
                Console.WriteLine($"You may only specify the following arguments.\n" +
                    $"\"m2t\" - Morse code to text.\n" +
                    $"\"t2m\" - Text to Morse code.\n" +
                    $"\"help\" - How to use the application.\n" +
                    $"\"translations\" - List of translatable characters.");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Loop through the <c>dictTranslations</c> dictionary and show the Morse code per character.
        /// </summary>
        static void Translations()
        {
            foreach(KeyValuePair<char, string> t in dictTranslations)
            {
                Console.WriteLine($"{t.Key} | {t.Value}\n");
            }
        }

        /// <summary>
        /// Show help text to the end user.
        /// </summary>
        static void HelpText()
        {
            Console.WriteLine("\n");
            Console.WriteLine(">    Morse code groups should be separated by a space.\n\n" +
                ">    Short should be the period key (.) and long should be a hyphen (-).\n\n" +
                ">    / - Forward slash should be used to separate words.\n\n" +
                ">    Example: \".... . .-.. .-.. --- / .-- --- .-. .-.. -..\" is \"HELLO WORLD\".");
            Console.WriteLine("\n");
            Environment.Exit(0);
        }

        /// <summary>
        /// Translate Morse code to text.
        /// Asks the end user for a string, splits it by ascii decimal 32 (char space), loops through it and translates by the <c>dictTranslations</c>.
        /// </summary>
        static void MorseToText()
        {
            Console.WriteLine("Enter the Morse code to be translated.");
            Console.Write("> ");

            string morse = Console.ReadLine();

            string[] arrMorse = morse.Split(' ');

            string translation = "";

            for (int i = 0; i < arrMorse.Length; i++)
            {
                if (arrMorse[i] == "/") 
                {
                    translation += " ";
                }
                else if (dictTranslations.ContainsValue(arrMorse[i]))
                {
                    translation += dictTranslations.FirstOrDefault(x => x.Value == arrMorse[i]).Key;
                }
                else
                {
                    translation += "#";
                }
            }

            Console.WriteLine($"Morse code: {morse}\n" +
                $"Translation: {translation}");
        }

        /// <summary>
        /// Translate text to Morse code.
        /// Asks the end user for a string, converts it to uppercase, loops through it and translates by the <c>dictTranslations</c>.
        /// </summary>
        static void TextToMorse()
        {
            Console.WriteLine("Enter the text to be translated.");
            Console.Write("> ");

            string text = Console.ReadLine();

            string utext = text.ToUpper();

            string translation = "";

            for (int i = 0; i < utext.Length; i++)
            {
                if(text[i] == 32)
                {
                    translation += " / ";
                }
                else if (dictTranslations.ContainsKey(utext[i]))
                {
                    translation += (dictTranslations[utext[i]] + " ");
                }
                else
                {
                    translation += utext[i];
                }
            }

            Console.WriteLine($"Text: {text}\n" +
                $"Morse code: {translation}");
        }
    }
}
