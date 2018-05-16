namespace practicas
{
    partial class inicioform
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.salirbtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.iniciarbtn = new System.Windows.Forms.Button();
            this.usuariotxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordtxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // salirbtn
            // 
            this.salirbtn.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salirbtn.Location = new System.Drawing.Point(202, 355);
            this.salirbtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.salirbtn.Name = "salirbtn";
            this.salirbtn.Size = new System.Drawing.Size(113, 32);
            this.salirbtn.TabIndex = 3;
            this.salirbtn.Text = "Salir";
            this.salirbtn.UseVisualStyleBackColor = true;
            this.salirbtn.Click += new System.EventHandler(this.salirbtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::practicas.Properties.Resources.user1;
            this.pictureBox1.Location = new System.Drawing.Point(211, 25);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // iniciarbtn
            // 
            this.iniciarbtn.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iniciarbtn.Location = new System.Drawing.Point(178, 285);
            this.iniciarbtn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.iniciarbtn.Name = "iniciarbtn";
            this.iniciarbtn.Size = new System.Drawing.Size(185, 32);
            this.iniciarbtn.TabIndex = 2;
            this.iniciarbtn.Text = "Iniciar Sesión";
            this.iniciarbtn.UseVisualStyleBackColor = true;
            this.iniciarbtn.Click += new System.EventHandler(this.iniciarbtn_Click);
            // 
            // usuariotxt
            // 
            this.usuariotxt.Location = new System.Drawing.Point(253, 171);
            this.usuariotxt.Name = "usuariotxt";
            this.usuariotxt.Size = new System.Drawing.Size(191, 26);
            this.usuariotxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Contraseña";
            // 
            // passwordtxt
            // 
            this.passwordtxt.Location = new System.Drawing.Point(253, 228);
            this.passwordtxt.Name = "passwordtxt";
            this.passwordtxt.PasswordChar = '*';
            this.passwordtxt.Size = new System.Drawing.Size(191, 26);
            this.passwordtxt.TabIndex = 1;
            this.passwordtxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordtxt_KeyPress);
            // 
            // inicioform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(555, 394);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.passwordtxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usuariotxt);
            this.Controls.Add(this.iniciarbtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.salirbtn);
            this.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "inicioform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            //this.Load += new System.EventHandler(this.inicioform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button salirbtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button iniciarbtn;
        private System.Windows.Forms.TextBox usuariotxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordtxt;
    }
}

