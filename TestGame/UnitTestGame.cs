using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP_Kursovaya;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace TestGame
{
    [TestClass]
    public class UnitTestGame
    {
        private GameField gameField;
        private ViewPort viewPort;
        private GameController controller;
        [TestInitialize]
        public void Init()
        {
            Size size = new Size(5, 5);
            gameField = new GameField(size, 30);
            viewPort = new ViewPort(null, gameField);
            controller = new GameController(viewPort, gameField);

        }
        [TestMethod]
        public void TheSimpleSceneGame()
        {
            Point headPos = controller.GetGameElems()[0].Pos;
            Point Dir = controller.GetDir();
            Point headPosLast = new Point(headPos.X, headPos.Y);
            for(int i = 0; i < 5; i++)
            {
                headPosLast.Offset(Dir);
                if (headPosLast.X < 0) { headPosLast.X = 4; }
                else if (headPosLast.X >= 5) { headPosLast.X = 0; }
                if (headPosLast.Y < 0) { headPosLast.Y = 4; }
                else if (headPosLast.Y >= 5) { headPosLast.Y = 0; }
            }
            for(int i = 0; i < 5; i++)
            {
                controller.Update(100);

            }
           Point last = controller.GetGameElems()[0].Pos;
            Assert.AreEqual(last, headPosLast);
        }
    }
}
