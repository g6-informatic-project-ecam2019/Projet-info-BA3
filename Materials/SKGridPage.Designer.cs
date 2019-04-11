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
            this.Client = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Search = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientcommandBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pricesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mykitboxDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piececommandBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Client
            // 
            this.Client.Location = new System.Drawing.Point(770, 80);
            this.Client.Name = "Client";
            this.Client.Size = new System.Drawing.Size(82, 40);
            this.Client.TabIndex = 2;
            this.Client.Text = "Client";
            this.Client.UseVisualStyleBackColor = true;
            this.Client.Click += new System.EventHandler(this.Client_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(725, 273);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(761, 332);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 4;
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(761, 361);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(100, 33);
            this.Search.TabIndex = 5;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // ClientCommand
            // 
            this.ClientCommand.Location = new System.Drawing.Point(770, 12);
            this.ClientCommand.Name = "ClientCommand";
            this.ClientCommand.Size = new System.Drawing.Size(82, 45);
            this.ClientCommand.TabIndex = 6;
            this.ClientCommand.Text = "Client Command";
            this.ClientCommand.UseVisualStyleBackColor = true;
            this.ClientCommand.Click += new System.EventHandler(this.ClientCommand_Click);
            // 
            // Prices
            // 
            this.Prices.Location = new System.Drawing.Point(770, 148);
            this.Prices.Name = "Prices";
            this.Prices.Size = new System.Drawing.Size(82, 40);
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
            this.PieceCommand.Location = new System.Drawing.Point(770, 218);
            this.PieceCommand.Name = "PieceCommand";
            this.PieceCommand.Size = new System.Drawing.Size(82, 48);
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
            // SKGridPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 451);
            this.Controls.Add(this.PieceCommand);
            this.Controls.Add(this.Prices);
            this.Controls.Add(this.ClientCommand);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Client);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SKGridPage";
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Client;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Search;
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
    }
}