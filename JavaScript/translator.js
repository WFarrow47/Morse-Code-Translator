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

// require readline in order to read from console.
const readline = require("readline");

// create readline interface, input on standard input and output on standard output.
const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

// translations key-value.
const TRANSLATIONS = {
    'A': '.-', 'B': '-...', 'C': '-.-.', 'D': '-..', 'E': '.', 'F': '..-.', 'G': '--.', 'H': '....',
    'I': '..', 'J': '.---', 'K': '-.-', 'L': '.-..', 'M': '--', 'N': '-.', 'O': '---', 'P': '.--.',
    'Q': '--.-', 'R': '.-.', 'S': '...', 'T': '-', 'U': '..-', 'V': '...-', 'W': '.--', 'X': '-..-',
    'Y': '-.--', 'Z': '--..', '1': '.----', '2': '..---', '3': '...--', '4': '....-', '5': '.....',
    '6': '-....', '7': '--...', '8': '---..', '9': '----.', '0': '-----', '.': '.-.-.-', ',': '--..--',
    '?': '..--..'
};

/**
 * Helper Function (ask question in console)
 */
const askQuestion = async () => new Promise((resolve) => rl.question("> ", ans =>
{
    rl.close();
    resolve(ans);
}));

// self invoking function.
(() =>
{
    // check if the end user has specified only one argument, the length check is at 3 due to the first and second elems are a part of node.
    if (process.argv.length !== 3)
    {
        console.log("You must specify " + (process.argv.length > 3 ? "only one" : "an") + " argument.\n" +
            "\"m2t\" - Morse code to text.\n" +
            "\"t2m\" - Text to Morse code.\n" +
            "\"help\" - How to use the application.\n" +
            "\"translations\" - List of translatable characters.");
        process.exit(1);
    }

    // check if the provided command line argument is "m2t", "t2m", "help", or "translations" and call related methods.
    if (process.argv[2] == "m2t")
    {
        morseToText();
    }
    else if (process.argv[2] == "t2m")
    {
        textToMorse();
    }
    else if (process.argv[2] == "help")
    {
        helpText();
    }
    else if (process.argv[2] == "translations")
    {
        translations();
    }
    else
    {
        console.log("You may only specify the following arguments.\n" +
            "\"m2t\" - Morse code to text.\n" +
            "\"t2m\" - Text to Morse code.\n" +
            "\"help\" - How to use the application.\n" +
            "\"translations\" - List of translatable characters.");
        process.exit(1);
    }
})();

/**
 * Loop through TRANSLATIONS and show the Morse code per character.
 */
function translations()
{
    Object.keys(TRANSLATIONS).forEach((key) => console.log(`${key} | ${TRANSLATIONS[key]}\n`));
}

/**
 * Show help text to the end user.
 */
function helpText()
{
    console.log("\n");
    console.log(">    Morse code groups should be separated by a space.\n\n" +
        ">    Short should be the period key (.) and long should be a hyphen (-).\n\n" +
        ">    / - Forward slash should be used to separate words.\n\n" +
        ">    Example: \".... . .-.. .-.. --- / .-- --- .-. .-.. -..\" is \"HELLO WORLD\".");
    process.exit(0);
}

/**
 * Translate Morse code to text.
 * Asks the end user for a string, splits it by space, loops through it and translates by the TRANSLATIONS object.
 */
async function morseToText()
{
    let arrMorse, translation = "";
    console.log("Enter the Morse code to be translated.");
    const morse = await askQuestion();

    arrMorse = morse.split(' ');

    arrMorse.forEach(elem =>
    {
        if (elem === "/")
        {
            translation += ' ';
        }
        else if (Object.values(TRANSLATIONS).includes(elem))
        {
            translation += Object.keys(TRANSLATIONS).find(key => TRANSLATIONS[key] === elem);
        }
        else
        {
            translation += "#";
        }
    });
    console.log("Morse code: " + morse + "\n" +
        "Translation: " + translation);
}

/**
 * Translate text to Morse code.
 * Asks the end user for a string, converts it to uppercase, loops through it and translates by the TRANSLATIONS object.
 */
async function textToMorse()
{
    let utext, translation = "";
    console.log("Enter the Morse code to be translated.");
    const text = await askQuestion();

    utext = text.toUpperCase();

    utext.split("").forEach(elem =>
    {
        if (elem === " ")
        {
            translation += ' / ';
        }
        else if (Object.keys(TRANSLATIONS).includes(elem))
        {
            translation += TRANSLATIONS[elem] + " ";
        }
        else
        {
            translation += elem;
        }
    });
    console.log("Morse code: " + text + "\n" +
        "Translation: " + translation);
}