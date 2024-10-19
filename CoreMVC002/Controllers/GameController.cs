using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreMVC002.Models;
using Microsoft.AspNetCore.Mvc;


namespace CoreMVC002.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // 初始化秘密數字
            string secretNumber = GenerateRandomNumberString();

            // 傳遞到 View 內部後再回到 Controller
            TempData["secretNumber"] = secretNumber;

            // 初始化猜測記錄（如果不存在）
            if (TempData["GuessHistory"] == null)
            {
                TempData["GuessHistory"] = new List<string>();
            }
            //初始化猜測次數
            if (TempData["GuessCount"] == null)
            {
                TempData["GuessCount"] = 0;
            }
            var model = new XAXBEngine(secretNumber);

            return View(model);
        }

        [HttpPost]
        public ActionResult Guess(XAXBEngine model)
        {
            string secretNumber = TempData["secretNumber"] as string;
            model.Secret = secretNumber;
            // 檢查猜測結果
            int Acount = model.numOfA(model.Guess);
            int Bcount = model.numOfB(model.Guess);
            model.Result = $"{Acount}A{Bcount}B";

            //紀錄每次猜測次數和結果
            string guessHistory = TempData["GuessHistory"] as string ?? "";
            string guessRecord = $"{model.Guess}:{model.Result}";
            guessHistory += guessRecord + "\n";
            TempData["GuessHistory"] = guessHistory;

            //讀猜測次數
            int Count = TempData["GuessCount"] != null ? (int)TempData["GuessCount"] : 0;


            Count++;
            TempData["GuessCount"] = Count;

            //更新模型中的GuessCount
            model.GuessCount = Count;

            //將猜測記錄加入模型中
            model.Store = guessHistory;

            //判斷是否猜對
            model.IsCorrect = model.IsGameOver(model.Guess);

            TempData.Keep("secretNumber");
            return View("Index", model);
        }

        // 遊戲邏輯
        private string GenerateRandomNumberString()
        {
            Random random = new Random();
            HashSet<int> digits = new HashSet<int>();
            // 生成 4 個不重複的隨機數字
            while (digits.Count < 4)
            {
                int digit = random.Next(0, 10); 
                digits.Add(digit); 
            }
            // 將 HashSet 中的數字轉換為字符串並返回
            return string.Join("", digits);
        }
        [HttpPost]
        public ActionResult Restart()
        {
            // 重新初始化遊戲
            TempData.Remove("secretNumber");
            TempData.Remove("GuessCount");
            TempData.Remove("GuessHistory");

            //重新開始遊戲
            return RedirectToAction("Index");
        }
        private string GetGuessResult(string guess)
        {
            // 檢查猜的結果，返回結果字符串
            string secretNumber = TempData["secretNumber"] as string;
            // 利用Keep(...) 方法, or 再次回存！
            TempData.Keep("SecretNumber");

            // 你可以根據遊戲規則自定義檢查邏輯
            if (secretNumber.Equals(guess))
                return "4A0B";
            else
                return "?A?B";
        }
    }
}

