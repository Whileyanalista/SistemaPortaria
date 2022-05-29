namespace SistemaPortaria
{
    partial class FormValidacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormValidacao));
            this.panelValidacao = new System.Windows.Forms.Panel();
            this.pictureBox32 = new System.Windows.Forms.PictureBox();
            this.labelValidar = new System.Windows.Forms.Label();
            this.labelSerie = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelNomeCondominio = new System.Windows.Forms.Label();
            this.buttonKeyHabilite = new System.Windows.Forms.Button();
            this.buttonHabilitarEmail = new System.Windows.Forms.Button();
            this.buttonCond = new System.Windows.Forms.Button();
            this.textBoxConominio = new System.Windows.Forms.TextBox();
            this.textBoxChave = new System.Windows.Forms.TextBox();
            this.textBoxEmailAtivar = new System.Windows.Forms.TextBox();
            this.pictureBoxConecxãoOk = new System.Windows.Forms.PictureBox();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.pictureBoxConecxão = new System.Windows.Forms.PictureBox();
            this.timerProgreceBar = new System.Windows.Forms.Timer(this.components);
            this.panelValidacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConecxãoOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConecxão)).BeginInit();
            this.SuspendLayout();
            // 
            // panelValidacao
            // 
            this.panelValidacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(73)))), ((int)(((byte)(121)))));
            this.panelValidacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelValidacao.Controls.Add(this.pictureBox32);
            this.panelValidacao.Controls.Add(this.labelValidar);
            this.panelValidacao.Controls.Add(this.labelSerie);
            this.panelValidacao.Controls.Add(this.labelEmail);
            this.panelValidacao.Controls.Add(this.labelNomeCondominio);
            this.panelValidacao.Controls.Add(this.buttonKeyHabilite);
            this.panelValidacao.Controls.Add(this.buttonHabilitarEmail);
            this.panelValidacao.Controls.Add(this.buttonCond);
            this.panelValidacao.Controls.Add(this.textBoxConominio);
            this.panelValidacao.Controls.Add(this.textBoxChave);
            this.panelValidacao.Controls.Add(this.textBoxEmailAtivar);
            this.panelValidacao.Controls.Add(this.pictureBoxConecxãoOk);
            this.panelValidacao.Controls.Add(this.buttonRegister);
            this.panelValidacao.Controls.Add(this.pictureBoxConecxão);
            this.panelValidacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelValidacao.Location = new System.Drawing.Point(0, 0);
            this.panelValidacao.Name = "panelValidacao";
            this.panelValidacao.Size = new System.Drawing.Size(422, 211);
            this.panelValidacao.TabIndex = 14;
            // 
            // pictureBox32
            // 
            this.pictureBox32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(73)))), ((int)(((byte)(121)))));
            this.pictureBox32.Image = global::SistemaPortaria.Properties.Resources.Icone;
            this.pictureBox32.Location = new System.Drawing.Point(24, 3);
            this.pictureBox32.Name = "pictureBox32";
            this.pictureBox32.Size = new System.Drawing.Size(58, 50);
            this.pictureBox32.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox32.TabIndex = 65;
            this.pictureBox32.TabStop = false;
            // 
            // labelValidar
            // 
            this.labelValidar.AutoSize = true;
            this.labelValidar.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValidar.ForeColor = System.Drawing.Color.White;
            this.labelValidar.Location = new System.Drawing.Point(202, 18);
            this.labelValidar.Name = "labelValidar";
            this.labelValidar.Size = new System.Drawing.Size(115, 26);
            this.labelValidar.TabIndex = 24;
            this.labelValidar.Text = "VALIDAÇÃO";
            // 
            // labelSerie
            // 
            this.labelSerie.AutoSize = true;
            this.labelSerie.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSerie.ForeColor = System.Drawing.Color.White;
            this.labelSerie.Location = new System.Drawing.Point(24, 131);
            this.labelSerie.Name = "labelSerie";
            this.labelSerie.Size = new System.Drawing.Size(49, 18);
            this.labelSerie.TabIndex = 23;
            this.labelSerie.Text = "SERIAL";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmail.ForeColor = System.Drawing.Color.White;
            this.labelEmail.Location = new System.Drawing.Point(24, 94);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(52, 18);
            this.labelEmail.TabIndex = 22;
            this.labelEmail.Text = "E-MAIL";
            // 
            // labelNomeCondominio
            // 
            this.labelNomeCondominio.AutoSize = true;
            this.labelNomeCondominio.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNomeCondominio.ForeColor = System.Drawing.Color.White;
            this.labelNomeCondominio.Location = new System.Drawing.Point(23, 61);
            this.labelNomeCondominio.Name = "labelNomeCondominio";
            this.labelNomeCondominio.Size = new System.Drawing.Size(47, 18);
            this.labelNomeCondominio.TabIndex = 21;
            this.labelNomeCondominio.Text = "HOME";
            // 
            // buttonKeyHabilite
            // 
            this.buttonKeyHabilite.BackColor = System.Drawing.SystemColors.Control;
            this.buttonKeyHabilite.Enabled = false;
            this.buttonKeyHabilite.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonKeyHabilite.FlatAppearance.BorderSize = 0;
            this.buttonKeyHabilite.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.buttonKeyHabilite.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonKeyHabilite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonKeyHabilite.ForeColor = System.Drawing.Color.White;
            this.buttonKeyHabilite.Image = global::SistemaPortaria.Properties.Resources.Key_2_16;
            this.buttonKeyHabilite.Location = new System.Drawing.Point(383, 130);
            this.buttonKeyHabilite.Name = "buttonKeyHabilite";
            this.buttonKeyHabilite.Size = new System.Drawing.Size(22, 20);
            this.buttonKeyHabilite.TabIndex = 0;
            this.buttonKeyHabilite.UseVisualStyleBackColor = false;
            // 
            // buttonHabilitarEmail
            // 
            this.buttonHabilitarEmail.AutoSize = true;
            this.buttonHabilitarEmail.BackColor = System.Drawing.SystemColors.Control;
            this.buttonHabilitarEmail.CausesValidation = false;
            this.buttonHabilitarEmail.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonHabilitarEmail.FlatAppearance.BorderSize = 0;
            this.buttonHabilitarEmail.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.buttonHabilitarEmail.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonHabilitarEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHabilitarEmail.ForeColor = System.Drawing.Color.White;
            this.buttonHabilitarEmail.Image = global::SistemaPortaria.Properties.Resources.Message_Filled_16;
            this.buttonHabilitarEmail.Location = new System.Drawing.Point(384, 92);
            this.buttonHabilitarEmail.Name = "buttonHabilitarEmail";
            this.buttonHabilitarEmail.Size = new System.Drawing.Size(22, 22);
            this.buttonHabilitarEmail.TabIndex = 0;
            this.buttonHabilitarEmail.UseVisualStyleBackColor = false;
            // 
            // buttonCond
            // 
            this.buttonCond.AutoSize = true;
            this.buttonCond.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCond.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCond.CausesValidation = false;
            this.buttonCond.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonCond.FlatAppearance.BorderSize = 0;
            this.buttonCond.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.buttonCond.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonCond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCond.ForeColor = System.Drawing.Color.White;
            this.buttonCond.Image = global::SistemaPortaria.Properties.Resources.Organization_16;
            this.buttonCond.Location = new System.Drawing.Point(383, 55);
            this.buttonCond.Name = "buttonCond";
            this.buttonCond.Size = new System.Drawing.Size(22, 22);
            this.buttonCond.TabIndex = 0;
            this.buttonCond.UseVisualStyleBackColor = false;
            // 
            // textBoxConominio
            // 
            this.textBoxConominio.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxConominio.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConominio.ForeColor = System.Drawing.Color.Gray;
            this.textBoxConominio.Location = new System.Drawing.Point(109, 53);
            this.textBoxConominio.MaxLength = 50;
            this.textBoxConominio.Name = "textBoxConominio";
            this.textBoxConominio.Size = new System.Drawing.Size(300, 26);
            this.textBoxConominio.TabIndex = 3;
            this.textBoxConominio.Click += new System.EventHandler(this.textBoxConominio_Click);
            this.textBoxConominio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxConominio_KeyPress);
            // 
            // textBoxChave
            // 
            this.textBoxChave.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxChave.Enabled = false;
            this.textBoxChave.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxChave.ForeColor = System.Drawing.Color.Gray;
            this.textBoxChave.Location = new System.Drawing.Point(109, 127);
            this.textBoxChave.MaxLength = 50;
            this.textBoxChave.Name = "textBoxChave";
            this.textBoxChave.Size = new System.Drawing.Size(300, 26);
            this.textBoxChave.TabIndex = 1;
            this.textBoxChave.Click += new System.EventHandler(this.textBoxChave_Click);
            this.textBoxChave.MouseEnter += new System.EventHandler(this.textBoxChave_MouseEnter);
            // 
            // textBoxEmailAtivar
            // 
            this.textBoxEmailAtivar.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxEmailAtivar.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEmailAtivar.ForeColor = System.Drawing.Color.Gray;
            this.textBoxEmailAtivar.Location = new System.Drawing.Point(109, 90);
            this.textBoxEmailAtivar.MaxLength = 50;
            this.textBoxEmailAtivar.Name = "textBoxEmailAtivar";
            this.textBoxEmailAtivar.Size = new System.Drawing.Size(300, 26);
            this.textBoxEmailAtivar.TabIndex = 2;
            this.textBoxEmailAtivar.Click += new System.EventHandler(this.textBoxEmailAtivar_Click);
            this.textBoxEmailAtivar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEmailAtivar_KeyPress);
            // 
            // pictureBoxConecxãoOk
            // 
            this.pictureBoxConecxãoOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxConecxãoOk.Image = global::SistemaPortaria.Properties.Resources.pictureBoxConecxãoOk3;
            this.pictureBoxConecxãoOk.Location = new System.Drawing.Point(381, 2);
            this.pictureBoxConecxãoOk.Name = "pictureBoxConecxãoOk";
            this.pictureBoxConecxãoOk.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxConecxãoOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxConecxãoOk.TabIndex = 12;
            this.pictureBoxConecxãoOk.TabStop = false;
            this.pictureBoxConecxãoOk.Visible = false;
            // 
            // buttonRegister
            // 
            this.buttonRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(117)))), ((int)(((byte)(0)))));
            this.buttonRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegister.ForeColor = System.Drawing.Color.White;
            this.buttonRegister.Location = new System.Drawing.Point(107, 164);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(305, 30);
            this.buttonRegister.TabIndex = 4;
            this.buttonRegister.Text = "REGISTRAR";
            this.buttonRegister.UseVisualStyleBackColor = false;
            this.buttonRegister.Click += new System.EventHandler(this.buttonAtivação_Click);
            // 
            // pictureBoxConecxão
            // 
            this.pictureBoxConecxão.Image = global::SistemaPortaria.Properties.Resources.sem_wi_fi2;
            this.pictureBoxConecxão.Location = new System.Drawing.Point(381, 1);
            this.pictureBoxConecxão.Name = "pictureBoxConecxão";
            this.pictureBoxConecxão.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxConecxão.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxConecxão.TabIndex = 10;
            this.pictureBoxConecxão.TabStop = false;
            this.pictureBoxConecxão.Visible = false;
            // 
            // timerProgreceBar
            // 
            this.timerProgreceBar.Interval = 1;
            // 
            // FormValidacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(422, 211);
            this.Controls.Add(this.panelValidacao);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "FormValidacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validacão";
            this.Load += new System.EventHandler(this.Validacao_Load);
            this.panelValidacao.ResumeLayout(false);
            this.panelValidacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConecxãoOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConecxão)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelValidacao;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.PictureBox pictureBoxConecxão;
        private System.Windows.Forms.Button buttonHabilitarEmail;
        private System.Windows.Forms.Button buttonKeyHabilite;
        private System.Windows.Forms.Button buttonCond;
        private System.Windows.Forms.TextBox textBoxChave;
        private System.Windows.Forms.TextBox textBoxEmailAtivar;
        private System.Windows.Forms.TextBox textBoxConominio;
        private System.Windows.Forms.Label labelSerie;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelNomeCondominio;
        private System.Windows.Forms.PictureBox pictureBoxConecxãoOk;
        private System.Windows.Forms.Timer timerProgreceBar;
        private System.Windows.Forms.Label labelValidar;
        private System.Windows.Forms.PictureBox pictureBox32;
    }
}