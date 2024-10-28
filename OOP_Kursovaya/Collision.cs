using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Kursovaya
{
    public class Collision
    {
        private Snake _snake;
        private GameField _gameField;
        public Collision(Snake snake, GameField gameField)
        {
            _snake = snake;
            _gameField = gameField;
        }

        public bool IsFree(Point pos)
        {
            foreach(GameElem gE in _snake.GetSnakeElems())
            {
                if(pos == gE.Pos)
                {
                    return false;
                }
               
            }
            foreach (GameElem aP in _gameField.GetApples())
            {
                if(pos == aP.Pos)
                {
                    return false;
                }
            }
            return true;
        }
        public GameElem IsCollide(Point pos)
        {
            foreach( GameElem gE in _snake.GetSnakeElems())
            {
                if (pos == gE.Pos)
                {
                    return gE;
                }
            }
            foreach ( GameElem aP in _gameField.GetApples())
            {
                if(pos == aP.Pos)
                {
                    return aP;
                }
            }
            //snake.Find()
            return null;
        }
    }
}
