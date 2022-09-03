using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    //計分規則
    //文字: https://www.tpenoc.net/sport/bowling/
    //影片: https://www.youtube.com/watch?v=Eu6i6hMv_A8
    public class Game
    {
        private List<int> _rolls = new List<int>(22);//21 for capacity應該是記憶體的，但list還是要用add的樣子，超過還是會做重分配的樣子
        //https://www.geeksforgeeks.org/c-sharp-capacity-of-a-list/
        private int _currentRoll = 0;

        public Game()
        {
            //一般perfectgame最大應該有22個計分位置，但下面將strike兩球和一計算perfectgame應該12個位置就夠了
            //尾局spare則要21個位置
            for (int i = 0; i < 21; i++)
            {
                _rolls.Add(0);
            }
        }

        public void Roll(int pinsHit)
        {
            _rolls[_currentRoll++] = pinsHit;
        }

        //這邊作者在寫時基本把能拆的function都拆出去了
        public int Score {
            //將Score隔離
            get
            {
                int score = 0;
                int frameIndex =0;//投擲次數含補投(可能輪空)
                for(int frame = 0; frame < 10; frame++)
                {
                    if (IsStrike(frameIndex))
                    {
                        //strike case 10(一球) + 下兩球獎勵分
                        score += 10 + StrikeBonus(frameIndex);
                        frameIndex ++;//strike時兩球空間合併為一球(這算法感覺不太正規)，但簡單情況下可通
                    }
                    else if(IsSpare(frameIndex))
                    {
                        //spare case 10(兩球) + 下一球獎勵分
                        score += 10 + SpareBonus(frameIndex);
                        frameIndex += 2;
                    }
                    else
                    {
                        // normal case 加補球共兩球
                        score += NormalFrameBonus(frameIndex);
                        frameIndex += 2;//進入下個記分局
                    }
                    
                }
                return score;
            }

        }

        private bool IsStrike(int frameIndex)
        {
            return _rolls[frameIndex] == 10;
        }

        private bool IsSpare(int frameIndex)
        {
            return _rolls[frameIndex] + _rolls[frameIndex + 1] == 10;
        }

        private int StrikeBonus(int frameIndex)
        {
            return _rolls[frameIndex + 1] + _rolls[frameIndex + 2];
        }

        private int SpareBonus(int frameIndex)
        {
            return _rolls[frameIndex + 2];
        }

        private int NormalFrameBonus(int frameIndex)
        {
            return _rolls[frameIndex] + _rolls[frameIndex + 1];
        }
    }
}
