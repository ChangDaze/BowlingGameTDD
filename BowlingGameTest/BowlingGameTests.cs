using BowlingGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BowlingGameTest
{
    [TestClass]
    public class BowlingGameTests
    {
        [TestMethod]
        public void TestGutterGame()
        {
            var game = SetupGame();

            RollPins(game, 20, 0);            

            Assert.AreEqual(0, game.Score);
        }

        [TestMethod]
        public void TestHittingOnePinPerRoll()
        {
            var game = SetupGame();

            RollPins(game, 20, 1);

            Assert.AreEqual(20, game.Score);
        }

        [TestMethod]
        public void TestOneSpare()
        {
            var game = SetupGame();

            //RollPins(game, 18, 0);//可以另做尾局為spare情況的test
            RollSpare(game);
            game.Roll(3);
            RollPins(game, 17, 0);//補上剩餘球數，沒補上會錯誤(list會少)

            Assert.AreEqual(16, game.Score);
        }

        [TestMethod]
        public void TestOneStrike()
        {
            var game = SetupGame();

            RollStrike(game);
            game.Roll(3);
            game.Roll(4);
            RollPins(game, 16, 0);

            Assert.AreEqual(24, game.Score);
        }

        [TestMethod]//沒加不會加入test
        public void TestPerfectGame()
        {
            var game = SetupGame();

            RollPins(game, 12, 10);

            Assert.AreEqual(300, game.Score);
        }

        #region Private Methods
        private Game SetupGame()
        {
            return new Game();
        }

        private void RollPins(Game game, int numberOfRolls, int pinsHitPerRoll)
        {
            for (int i = 0; i < numberOfRolls; i++)
            {
                game.Roll(pinsHitPerRoll);
            }
        }

        private void RollSpare(Game game)
        {
            game.Roll(5);//這其實應該把每個球擊到數記下來，不過這個測試只計分用所以隨便寫
            game.Roll(5);
        }

        private void RollStrike(Game game)
        {
            game.Roll(10);
        }
        #endregion
    }
}
