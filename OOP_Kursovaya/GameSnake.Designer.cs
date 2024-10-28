namespace OOP_Kursovaya
{
    partial class Game_Snake
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            //this.button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button
            // 
            //this.button.Location = new System.Drawing.Point(286, 21);
            //this.button.Name = "button";
            //this.button.Size = new System.Drawing.Size(68, 24);
            //this.button.TabIndex = 0;
            //this.button.Text = "button";
            //this.button.UseVisualStyleBackColor = true;
            // 
            // Game_Snake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 268);
            //this.Controls.Add(this.button);
            this.Name = "Game_Snake";
            this.Text = "Змейка";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Game_Snake_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_Snake_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer;
       // private System.Windows.Forms.Button button;
    }
}

