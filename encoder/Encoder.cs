using System;
using System.IO;

namespace encoder
{
    internal class Encoder
    {

        private string encodeType;
        private string clearTextFilePath;

        public Encoder(string encodeType, string clearTextFilePath)
        {
            this.encodeType = encodeType;
            this.clearTextFilePath = clearTextFilePath;
        }

        public void Encode()
        {
            // Verify that clear text file exists
            if (!File.Exists(clearTextFilePath))
            {
                throw new FileNotFoundException("\n ERROR: File does not exist. Check that the path you insert is correct \n");
            }
            string clearText = File.ReadAllText(clearTextFilePath);
            string cipherText;
            string filepath = Directory.GetCurrentDirectory() + @"\cipherText.txt";

            // Use the encode type entered by user. Toupper() for accepting case insensitive input
            switch (encodeType.ToUpper())
            {
                case "ATBASH":
                    cipherText = EncodeAtbash(clearText);
                    break;
                case "PROGRESSION":
                    cipherText = EncodeProgression(clearText);
                    break;
                default: throw new ArgumentException("\n ERROR: Invalid encode type. You can choose: Atbash/Progression \n" );

            }
            
            // Write cipher text to file
            File.WriteAllText(filepath, cipherText + "\n");

            // Calculate letters frequency and append to the file
            FrequencyOfLetters(clearText, filepath);

        }

        public string EncodeAtbash(string clearText)
        {
            char[] cipherText = new char[clearText.Length];
            for (int i = 0; i < clearText.Length; i++)
            {
                if(clearText[i]>= 'a' && clearText[i] <= 'z')
                {
                    cipherText[i] = (char)('a' + ('z' - clearText[i]));
                }

                else if(clearText[i] >= 'A' && clearText[i] <= 'Z')
                {
                    cipherText[i] = (char)('A' + ('Z' - clearText[i]));
                }
                else
                {
                    throw new ArgumentException("\n ERROR: Your Text Contain Special Charecters! Only letters please. \n");
                }
            }

            string decodeOutput = new string(cipherText);
            return decodeOutput; 

        }

        public string EncodeProgression(string clearText)
        {
            char[] cipherText = new char[clearText.Length];
            for (int i = 0; i < clearText.Length; i++)
            {
                if (clearText[i] >= 'a' && clearText[i] <= 'z')
                {
                    cipherText[i] = (char)((clearText[i] - 'a' + 1) % 26 + 'a');

                }
                else if (clearText[i] >= 'A' && clearText[i] <= 'Z')
                {
                    cipherText[i] = (char)((clearText[i] - 'A' + 1) % 26 + 'A');
                }
                else
                {
                    throw new ArgumentException("\n ERROR: Your Text Contain Special Charecters! Only letters please. \n");
                }
            }

            string decodeOutput = new string(cipherText);
            return decodeOutput;
        }

        public void FrequencyOfLetters(string clearText, string filepath)
        {
            int[] upperCase = new int[26];
            int[] lowerCase = new int[26];
            
            foreach (char c in clearText)
            {
                if (c >= 'a' && c <= 'z')
                {
                    lowerCase[c - 'a']++;
                }

                if (c >= 'A' && c <= 'Z')
                {
                    upperCase[c - 'A']++;
                }
            }

            for (int i = 0; i < 26; i++)
            {
                if (upperCase[i] != 0)
                    File.AppendAllText(filepath, (char)(i + 'A') + "=" + upperCase[i]+ " ");

                if (lowerCase[i] != 0)
                    File.AppendAllText(filepath, (char)(i + 'a') + "=" + lowerCase[i]+ " ");
            } 
        }
    }  
}
