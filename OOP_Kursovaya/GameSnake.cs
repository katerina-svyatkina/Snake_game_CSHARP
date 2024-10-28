using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace OOP_Kursovaya
{
    public partial class Game_Snake : Form
    {
        private int speed = 400; // in milliseconds
        private Size _size = new Size (20,12); // размер поля
        private int _sizeOfElem = 30; // размер элемента в пикселях
        private GameField _gameField; // поле
        private ViewPort _viewPort; // порт отображения объектов
        //private Snake _snake;       // змея
        private GameController _controller; // управление
        //private Collision _collision;   // столкновения
        private Button buttonStart;
        private Button buttonLoad;
        private Button buttonSave;

        public Game_Snake()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(_update);
            timer.Interval = speed;
            GameMenu();

        }
        private void InitGame()
        {
            _gameField = new GameField(_size, _sizeOfElem);
            if (_viewPort != null)
            {
                _viewPort.ClearView();
                _viewPort.ClearInfo();
            }
            _viewPort = new ViewPort(this.Controls, _gameField);
            //_snake = new Snake(_sizeOfElem, _gameField); // moved to GameController
            _controller = new GameController(_viewPort, _gameField);
            //_collision = new Collision(); // moved to GameController
            this.Width = _gameField.CalcWidthPix();//(_size.Width + 1) * _sizeOfElem + _sizeOfElem/2;
            this.Height = _gameField.CalcHeigthPix();//(_size.Height + 1) * _sizeOfElem + _sizeOfElem/2 + 50;
            this.MaximumSize = this.MinimumSize = new Size(this.Width, this.Height);
            //timer.Tick += new EventHandler(_update);
           // timer.Interval = speed;
        }
        private void _update(object myObject, EventArgs eventArgs)
        {
            if (_controller.Update(timer.Interval)) // логика перенесена внутрь контроллера
            {
                // игровой цикл прошел нормально
            }
            else
            {
                timer.Stop();
                //GameMenu();
                MessageBox.Show("Вы проиграли!");
                GameMenu();
                // End game
            }
        }
 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            MessageBox.Show("Bye!"); 
        }

        private void Game_Snake_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                timer.Stop();
                DestroyMenu();
                GameMenu();
            }
            _controller.SetKey(e.KeyCode);
        }

        private void Game_Snake_Load(object sender, EventArgs e)
        {
            
            //gameField = new GameField(new Size(20,20));
            //snake = new Snake(new Point(10,10));
            //controller = new GameController();
            //collision = new Collision();
        }
        private void GameMenu() {
            this.buttonStart = new Button();
            int sizeX = 68;
            int sizeY = 24;
            int step = 50;
            int posX = (this.Width - sizeX - 16)/2;
            int posY = (this.Height - sizeY )/2 - step;

            this.buttonStart.Location = new Point(posX, posY);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new Size(sizeX, sizeY);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += new EventHandler(buttonStart_click);
            //buttonStart.BringToFront(); 
            this.Controls.Add(this.buttonStart);
            Controls[Controls.IndexOf(buttonStart)].BringToFront();
            this.buttonSave = new Button();
            posY += step;
            //сохранить
            this.buttonSave.Location = new Point(posX, posY);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new Size(sizeX, sizeY);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += new EventHandler(buttonSave_click);
            //buttonSave.BringToFront();
            this.Controls.Add(this.buttonSave);
            Controls[Controls.IndexOf(buttonSave)].BringToFront();
            //загрузить
            this.buttonLoad = new Button();
            posY += step;

            this.buttonLoad.Location = new Point(posX, posY);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new Size(sizeX, sizeY);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Загрузить";
            this.buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += new EventHandler(buttonLoad_click);
            //buttonLoad.BringToFront();
            this.Controls.Add(this.buttonLoad);
            Controls[Controls.IndexOf(buttonLoad)].BringToFront();
        }

        private void buttonStart_click(object myObject, EventArgs eventArgs)
        {
            //buttonLoad.Visible = false;
            //buttonSave.Visible = false;
            //buttonStart.Visible = false;
            DestroyMenu();
            InitGame();
            this.Focus();
            timer.Start();
        }
        private void DestroyMenu()
        {
            Controls.Remove(buttonSave);
            Controls.Remove(buttonLoad);
            Controls.Remove(buttonStart);
            buttonSave.Dispose();
            buttonLoad.Dispose();
            buttonStart.Dispose();  
        }
        private void buttonSave_click(object myObject, EventArgs eventArgs)
        {
             StreamWriter sW = new StreamWriter("Save.txt");
            sW.WriteLine("[Apples] : ");
            sW.WriteLine(_gameField.GetApples().Count);
            foreach (GameElem ge in _gameField.GetApples())
            {
                sW.Write(ge.Pos.X + " ");
                sW.Write(ge.Pos.Y + " ");
                sW.WriteLine(ge.Val);
            }
            sW.WriteLine("[Snake] : ");
            List<GameElem> temp = _controller.GetGameElems();
            sW.WriteLine(temp.Count);
            foreach (GameElem ge in temp)
            {
                sW.Write(ge.Pos.X + " ");
                sW.Write(ge.Pos.Y + " ");
                sW.WriteLine(ge.Type);
            }
            sW.WriteLine("[Dir] : ");
            sW.Write(_controller.GetDir().X + " ");
            sW.WriteLine(_controller.GetDir().Y);
            sW.WriteLine("[Info] : ");
            sW.Write(_gameField.Info.Level + " ");
            sW.WriteLine(_gameField.Info.Score);
            sW.Close();
            MessageBox.Show("Данные успешно сохранены!");
        }
        private void buttonLoad_click(object myObject, EventArgs eventArgs)
        {
            StreamReader sr = new StreamReader("Save.txt");
            string temp = sr.ReadLine();  //заголовок apples
            temp = sr.ReadLine();        //количество яблок
            int count = int.Parse(temp);
            //MessageBox.Show(temp);
            //temp = sr.ReadLine();
            List<GameElem> Apples = new List<GameElem>();
            for(int i = 0; i < count; i++)
            {
                temp = sr.ReadLine();
                string[] a = temp.Split(' ');
                //for(int j = 0; j < a.Length; j++)
                //{
                    GameElem elem = new Apple(new Point(int.Parse(a[0]), int.Parse(a[1])),
                        _sizeOfElem, int.Parse(a[2]));
                    Apples.Add(elem);
                //}
            }
            temp = sr.ReadLine();  //[snake]
            count = int.Parse(sr.ReadLine());
           // temp = sr.ReadLine();
            List<GameElem> Snake = new List<GameElem>();
            for (int i = 0; i < count; i++)
            {
                temp = sr.ReadLine();
                string[] s = temp.Split(' ');   
                //for(int j=0; j <s.Length; j++)
                //{
                    GameElem elem;
                    if (s[2] == "Head")
                    {
                        elem = new SnakeHead(new Point(int.Parse(s[0]), int.Parse(s[1])), 
                            _sizeOfElem);
                    }
                    else
                    {
                        elem = new SnakeBody(new Point(int.Parse(s[0]), int.Parse(s[1])),
                            _sizeOfElem);
                    }
                    Snake.Add(elem);    
                //}
            }
            temp = sr.ReadLine();  //[dir]
            temp = sr.ReadLine();
            string[] d = temp.Split(' ');
            Point dir = new Point(int.Parse(d[0]), int.Parse(d[1]));
            //MessageBox.Show(temp1[2].ToString());
            temp = sr.ReadLine();  //[info]
            temp = sr.ReadLine();
            string[] inf = temp.Split(' ');
            int level = int.Parse(inf[0]);
            int score = int.Parse(inf[1]);
            sr.Close();
            // _controller = null;
            InitGame();
            _gameField.Load(Apples, level, score);
            _controller.Load(Snake, dir);
            _viewPort.AddViewElements(Apples, Snake);
            // _controller.Load(Snake, dir);

            //buttonLoad.Visible = false;
            //buttonSave.Visible = false;
            //buttonStart.Visible = false;
            //InitGame();
            DestroyMenu();
            this.Focus();
            timer.Start();
        }
    }
}
