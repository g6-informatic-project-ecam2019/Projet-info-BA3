namespace Materials
{
    partial class SKGridPage
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
            this.Client_Pieces = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ClientCommand = new System.Windows.Forms.Button();
            this.Prices = new System.Windows.Forms.Button();
            this.mykitboxDataSet = new Materials.mykitboxDataSet();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientTableAdapter = new Materials.mykitboxDataSetTableAdapters.clientTableAdapter();
            this.mykitboxDataSet1 = new Materials.mykitboxDataSet1();
            this.clientcommandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.client_commandTableAdapter = new Materials.mykitboxDataSet1TableAdapters.client_commandTableAdapter();
            this.mykitboxDataSet2 = new Materials.mykitboxDataSet2();
            this.pricesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pricesTableAdapter = new Materials.mykitboxDataSet2TableAdapters.pricesTableAdapter();
            this.PieceCommand = new System.Windows.Forms.Button();
            this.mykitboxDataSet3 = new Materials.mykitboxDataSet3();
            this.piececommandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.piececommandTableAdapter = new Materials.mykitboxDataSet3TableAdapters.piececommandTableAdapter();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.Prevbtn = new System.Windows.Forms.Button();
            this.mykitboxDataSet4 = new Materials.mykitboxDataSet4();
            this.clientpiecescommandBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.client_piecescommandTableAdapter = new Materials.mykitboxDataSet4TableAdapters.client_piecescommandTableAdapter();
            this.Modifie = new System.Windows.Forms.Button();
            this.ApplyMod = new System.Windows.Forms.Button();
            this.CancelMod = new System.Windows.Forms.Button();
            this.mykitboxDataSet5 = new Materials.mykitboxDataSet5();
            this.pieceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pieceTableAdapter = new Materials.mykitboxDataSet5TableAdapters.pieceTableAdapter();
            this.Pieces = new System.Windows.Forms.Button();
            this.RowAdd = new System.Windows.Forms.Button();
            this.RowDelete = new System.Windows.Forms.Button();
            this.textboxDel = new System.Windows.Forms.TextBox();
            this.labelDel = new System.Windows.Forms.Label();
            this.DeleteCommand = new System.Windows.Forms.Button();
            this.textBoxDel2 = new System.Windows.Forms.TextBox();
            this.labelDel2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientcommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pricesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piececommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientpiecescommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Client_Pieces
            // 
            this.Client_Pieces.Location = new System.Drawing.Point(771, 71);
            this.Client_Pieces.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Client_Pieces.Name = "Client_Pieces";
            this.Client_Pieces.Size = new System.Drawing.Size(83, 46);
            this.Client_Pieces.TabIndex = 2;
            this.Client_Pieces.Text = "Client Pieces";
            this.Client_Pieces.UseVisualStyleBackColor = true;
            this.Client_Pieces.Click += new System.EventHandler(this.Client_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(711, 290);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(729, 345);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 22);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ClientCommand
            // 
            this.ClientCommand.Location = new System.Drawing.Point(771, 12);
            this.ClientCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClientCommand.Name = "ClientCommand";
            this.ClientCommand.Size = new System.Drawing.Size(83, 46);
            this.ClientCommand.TabIndex = 6;
            this.ClientCommand.Text = "Client Command";
            this.ClientCommand.UseVisualStyleBackColor = true;
            this.ClientCommand.Click += new System.EventHandler(this.ClientCommand_Click);
            // 
            // Prices
            // 
            this.Prices.Location = new System.Drawing.Point(771, 135);
            this.Prices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Prices.Name = "Prices";
            this.Prices.Size = new System.Drawing.Size(83, 39);
            this.Prices.TabIndex = 7;
            this.Prices.Text = "Prices";
            this.Prices.UseVisualStyleBackColor = true;
            this.Prices.Click += new System.EventHandler(this.Prices_Click);
            // 
            // mykitboxDataSet
            // 
            this.mykitboxDataSet.DataSetName = "mykitboxDataSet";
            this.mykitboxDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataMember = "client";
            this.clientBindingSource.DataSource = this.mykitboxDataSet;
            // 
            // clientTableAdapter
            // 
            this.clientTableAdapter.ClearBeforeFill = true;
            // 
            // mykitboxDataSet1
            // 
            this.mykitboxDataSet1.DataSetName = "mykitboxDataSet1";
            this.mykitboxDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientcommandBindingSource
            // 
            this.clientcommandBindingSource.DataMember = "client_command";
            this.clientcommandBindingSource.DataSource = this.mykitboxDataSet1;
            // 
            // client_commandTableAdapter
            // 
            this.client_commandTableAdapter.ClearBeforeFill = true;
            // 
            // mykitboxDataSet2
            // 
            this.mykitboxDataSet2.DataSetName = "mykitboxDataSet2";
            this.mykitboxDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pricesBindingSource
            // 
            this.pricesBindingSource.DataMember = "prices";
            this.pricesBindingSource.DataSource = this.mykitboxDataSet2;
            // 
            // pricesTableAdapter
            // 
            this.pricesTableAdapter.ClearBeforeFill = true;
            // 
            // PieceCommand
            // 
            this.PieceCommand.Location = new System.Drawing.Point(771, 254);
            this.PieceCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PieceCommand.Name = "PieceCommand";
            this.PieceCommand.Size = new System.Drawing.Size(83, 48);
            this.PieceCommand.TabIndex = 8;
            this.PieceCommand.Text = "Piece Command";
            this.PieceCommand.UseVisualStyleBackColor = true;
            this.PieceCommand.Click += new System.EventHandler(this.PieceCommand_Click);
            // 
            // mykitboxDataSet3
            // 
            this.mykitboxDataSet3.DataSetName = "mykitboxDataSet3";
            this.mykitboxDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // piececommandBindingSource
            // 
            this.piececommandBindingSource.DataMember = "piececommand";
            this.piececommandBindingSource.DataSource = this.mykitboxDataSet3;
            // 
            // piececommandTableAdapter
            // 
            this.piececommandTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView2.Location = new System.Drawing.Point(12, 345);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(711, 228);
            this.dataGridView2.TabIndex = 9;
            this.dataGridView2.Visible = false;
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(741, 311);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(0, 17);
            this.SearchLabel.TabIndex = 10;
            // 
            // Prevbtn
            // 
            this.Prevbtn.Location = new System.Drawing.Point(771, 431);
            this.Prevbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Prevbtn.Name = "Prevbtn";
            this.Prevbtn.Size = new System.Drawing.Size(83, 48);
            this.Prevbtn.TabIndex = 11;
            this.Prevbtn.Text = "Previous";
            this.Prevbtn.UseVisualStyleBackColor = true;
            this.Prevbtn.Click += new System.EventHandler(this.Prevbtn_Click);
            // 
            // mykitboxDataSet4
            // 
            this.mykitboxDataSet4.DataSetName = "mykitboxDataSet4";
            this.mykitboxDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientpiecescommandBindingSource
            // 
            this.clientpiecescommandBindingSource.DataMember = "client_piecescommand";
            this.clientpiecescommandBindingSource.DataSource = this.mykitboxDataSet4;
            // 
            // client_piecescommandTableAdapter
            // 
            this.client_piecescommandTableAdapter.ClearBeforeFill = true;
            // 
            // Modifie
            // 
            this.Modifie.Location = new System.Drawing.Point(771, 377);
            this.Modifie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Modifie.Name = "Modifie";
            this.Modifie.Size = new System.Drawing.Size(83, 48);
            this.Modifie.TabIndex = 12;
            this.Modifie.Text = "Modify";
            this.Modifie.UseVisualStyleBackColor = true;
            this.Modifie.Click += new System.EventHandler(this.Modifie_Click);
            // 
            // ApplyMod
            // 
            this.ApplyMod.Location = new System.Drawing.Point(771, 377);
            this.ApplyMod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ApplyMod.Name = "ApplyMod";
            this.ApplyMod.Size = new System.Drawing.Size(83, 48);
            this.ApplyMod.TabIndex = 13;
            this.ApplyMod.Text = "Apply";
            this.ApplyMod.UseVisualStyleBackColor = true;
            this.ApplyMod.Visible = false;
            this.ApplyMod.Click += new System.EventHandler(this.ApplyMod_Click);
            // 
            // CancelMod
            // 
            this.CancelMod.Location = new System.Drawing.Point(771, 431);
            this.CancelMod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelMod.Name = "CancelMod";
            this.CancelMod.Size = new System.Drawing.Size(83, 48);
            this.CancelMod.TabIndex = 14;
            this.CancelMod.Text = "Cancel";
            this.CancelMod.UseVisualStyleBackColor = true;
            this.CancelMod.Visible = false;
            this.CancelMod.Click += new System.EventHandler(this.CancelMod_Click);
            // 
            // mykitboxDataSet5
            // 
            this.mykitboxDataSet5.DataSetName = "mykitboxDataSet5";
            this.mykitboxDataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pieceBindingSource
            // 
            this.pieceBindingSource.DataMember = "piece";
            this.pieceBindingSource.DataSource = this.mykitboxDataSet5;
            // 
            // pieceTableAdapter
            // 
            this.pieceTableAdapter.ClearBeforeFill = true;
            // 
            // Pieces
            // 
            this.Pieces.Location = new System.Drawing.Point(771, 189);
            this.Pieces.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Pieces.Name = "Pieces";
            this.Pieces.Size = new System.Drawing.Size(83, 48);
            this.Pieces.TabIndex = 15;
            this.Pieces.Text = "Pieces";
            this.Pieces.UseVisualStyleBackColor = true;
            this.Pieces.Click += new System.EventHandler(this.Pieces_Click);
            // 
            // RowAdd
            // 
            this.RowAdd.Location = new System.Drawing.Point(682, 493);
            this.RowAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RowAdd.Name = "RowAdd";
            this.RowAdd.Size = new System.Drawing.Size(83, 48);
            this.RowAdd.TabIndex = 16;
            this.RowAdd.Text = "Add a piece";
            this.RowAdd.UseVisualStyleBackColor = true;
            this.RowAdd.Visible = false;
            this.RowAdd.Click += new System.EventHandler(this.RowAdd_Click);
            // 
            // RowDelete
            // 
            this.RowDelete.Location = new System.Drawing.Point(771, 493);
            this.RowDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RowDelete.Name = "RowDelete";
            this.RowDelete.Size = new System.Drawing.Size(83, 48);
            this.RowDelete.TabIndex = 17;
            this.RowDelete.Text = "Delete a piece";
            this.RowDelete.UseVisualStyleBackColor = true;
            this.RowDelete.Visible = false;
            this.RowDelete.Click += new System.EventHandler(this.RowDelete_Click);
            // 
            // textboxDel
            // 
            this.textboxDel.Location = new System.Drawing.Point(771, 562);
            this.textboxDel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxDel.Name = "textboxDel";
            this.textboxDel.Size = new System.Drawing.Size(83, 22);
            this.textboxDel.TabIndex = 18;
            this.textboxDel.Visible = false;
            // 
            // labelDel
            // 
            this.labelDel.AutoSize = true;
            this.labelDel.Location = new System.Drawing.Point(768, 543);
            this.labelDel.Name = "labelDel";
            this.labelDel.Size = new System.Drawing.Size(97, 17);
            this.labelDel.TabIndex = 19;
            this.labelDel.Text = "Enter a code :";
            this.labelDel.Visible = false;
            // 
            // DeleteCommand
            // 
            this.DeleteCommand.Location = new System.Drawing.Point(771, 493);
            this.DeleteCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteCommand.Name = "DeleteCommand";
            this.DeleteCommand.Size = new System.Drawing.Size(83, 48);
            this.DeleteCommand.TabIndex = 20;
            this.DeleteCommand.Text = "Delete a command";
            this.DeleteCommand.UseVisualStyleBackColor = true;
            this.DeleteCommand.Visible = false;
            this.DeleteCommand.Click += new System.EventHandler(this.DeleteCommand_Click);
            // 
            // textBoxDel2
            // 
            this.textBoxDel2.Location = new System.Drawing.Point(771, 562);
            this.textBoxDel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxDel2.Name = "textBoxDel2";
            this.textBoxDel2.Size = new System.Drawing.Size(83, 22);
            this.textBoxDel2.TabIndex = 21;
            this.textBoxDel2.Visible = false;
            // 
            // labelDel2
            // 
            this.labelDel2.AutoSize = true;
            this.labelDel2.Location = new System.Drawing.Point(768, 543);
            this.labelDel2.Name = "labelDel2";
            this.labelDel2.Size = new System.Drawing.Size(103, 17);
            this.labelDel2.TabIndex = 22;
            this.labelDel2.Text = "Enter a idcom :";
            this.labelDel2.Visible = false;
            // 
            // SKGridPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(887, 595);
            this.Controls.Add(this.labelDel2);
            this.Controls.Add(this.textBoxDel2);
            this.Controls.Add(this.DeleteCommand);
            this.Controls.Add(this.labelDel);
            this.Controls.Add(this.textboxDel);
            this.Controls.Add(this.RowDelete);
            this.Controls.Add(this.RowAdd);
            this.Controls.Add(this.Pieces);
            this.Controls.Add(this.CancelMod);
            this.Controls.Add(this.ApplyMod);
            this.Controls.Add(this.Modifie);
            this.Controls.Add(this.Prevbtn);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.PieceCommand);
            this.Controls.Add(this.Prices);
            this.Controls.Add(this.ClientCommand);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Client_Pieces);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SKGridPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SKGridPage";
            this.Load += new System.EventHandler(this.SKGridPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientcommandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pricesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piececommandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientpiecescommandBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pieceBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Client_Pieces;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ClientCommand;
        private System.Windows.Forms.Button Prices;
        private mykitboxDataSet mykitboxDataSet;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private mykitboxDataSetTableAdapters.clientTableAdapter clientTableAdapter;
        private mykitboxDataSet1 mykitboxDataSet1;
        private System.Windows.Forms.BindingSource clientcommandBindingSource;
        private mykitboxDataSet1TableAdapters.client_commandTableAdapter client_commandTableAdapter;
        private mykitboxDataSet2 mykitboxDataSet2;
        private System.Windows.Forms.BindingSource pricesBindingSource;
        private mykitboxDataSet2TableAdapters.pricesTableAdapter pricesTableAdapter;
        private System.Windows.Forms.Button PieceCommand;
        private mykitboxDataSet3 mykitboxDataSet3;
        private System.Windows.Forms.BindingSource piececommandBindingSource;
        private mykitboxDataSet3TableAdapters.piececommandTableAdapter piececommandTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Button Prevbtn;
        private mykitboxDataSet4 mykitboxDataSet4;
        private System.Windows.Forms.BindingSource clientpiecescommandBindingSource;
        private mykitboxDataSet4TableAdapters.client_piecescommandTableAdapter client_piecescommandTableAdapter;
        private System.Windows.Forms.Button Modifie;
        private System.Windows.Forms.Button ApplyMod;
        private System.Windows.Forms.Button CancelMod;
        private mykitboxDataSet5 mykitboxDataSet5;
        private System.Windows.Forms.BindingSource pieceBindingSource;
        private mykitboxDataSet5TableAdapters.pieceTableAdapter pieceTableAdapter;
        private System.Windows.Forms.Button Pieces;
        private System.Windows.Forms.Button RowAdd;
        private System.Windows.Forms.Button RowDelete;
        private System.Windows.Forms.TextBox textboxDel;
        private System.Windows.Forms.Label labelDel;
        private System.Windows.Forms.Button DeleteCommand;
        private System.Windows.Forms.TextBox textBoxDel2;
        private System.Windows.Forms.Label labelDel2;
    }
}