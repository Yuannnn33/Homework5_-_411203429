using System;
using System.Reflection;

namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        public string Secret { get; set; }
        public string Guess { get; set; }
        public string Result { get; set; }
        public string Store { get; set; }
        public int GuessCount { get; set; }
        public bool IsCorrect { get; set; }
        public string GameOverMessage { get; set; }
        public XAXBEngine()
        {
            // TODO 0 - randomly 
            string randomSecret = "1234";
            Secret = randomSecret;
            Guess = null;
            Result = null;
        }


        public XAXBEngine(string secretNumber)
        {
            Secret = secretNumber;
            Guess = null;
            Result = null;

        }
        //
        public int numOfA(string guessNumber)
        {
            int Acount = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Secret[i] == guessNumber[i])
                {
                    Acount++;
                }
            }
            return Acount;
        }
        //  
        public int numOfB(string guessNumber)
        {
            int Bcount = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Secret[i] != guessNumber[i] && Secret.Contains(guessNumber[i]))
                {
                    Bcount++;
                }
            }
            return Bcount;
        }
        public bool IsGameOver(string guessNumber)
        {
            // TODO 3
            if (Secret.Equals(guessNumber))
            {
                GameOverMessage = "Bingo！答對啦～要不要重新開始遊戲？";
                return true;

            }
            return false;
        }

    }

}
