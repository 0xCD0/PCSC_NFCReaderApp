namespace pcsc
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text_DeviceName = new System.Windows.Forms.TextBox();
            this.Text_ReadResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_read = new System.Windows.Forms.Button();
            this.Text_WriteToTag = new System.Windows.Forms.TextBox();
            this.Btn_WriteToTag = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Text_DeviceName
            // 
            this.Text_DeviceName.Location = new System.Drawing.Point(12, 27);
            this.Text_DeviceName.Name = "Text_DeviceName";
            this.Text_DeviceName.ReadOnly = true;
            this.Text_DeviceName.Size = new System.Drawing.Size(374, 21);
            this.Text_DeviceName.TabIndex = 1;
            // 
            // Text_ReadResult
            // 
            this.Text_ReadResult.Location = new System.Drawing.Point(12, 88);
            this.Text_ReadResult.Name = "Text_ReadResult";
            this.Text_ReadResult.Size = new System.Drawing.Size(374, 21);
            this.Text_ReadResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(11, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 3;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Write to Tag";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Device Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Read Result";
            // 
            // btn_read
            // 
            this.btn_read.Location = new System.Drawing.Point(12, 115);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(374, 41);
            this.btn_read.TabIndex = 13;
            this.btn_read.Text = "Read from Tag";
            this.btn_read.UseVisualStyleBackColor = true;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // Text_WriteToTag
            // 
            this.Text_WriteToTag.Location = new System.Drawing.Point(13, 194);
            this.Text_WriteToTag.Name = "Text_WriteToTag";
            this.Text_WriteToTag.Size = new System.Drawing.Size(373, 21);
            this.Text_WriteToTag.TabIndex = 14;
            // 
            // Btn_WriteToTag
            // 
            this.Btn_WriteToTag.Location = new System.Drawing.Point(12, 222);
            this.Btn_WriteToTag.Name = "Btn_WriteToTag";
            this.Btn_WriteToTag.Size = new System.Drawing.Size(374, 41);
            this.Btn_WriteToTag.TabIndex = 15;
            this.Btn_WriteToTag.Text = "Read from Tag";
            this.Btn_WriteToTag.UseVisualStyleBackColor = true;
            this.Btn_WriteToTag.Click += new System.EventHandler(this.Btn_WriteToTag_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 297);
            this.Controls.Add(this.Btn_WriteToTag);
            this.Controls.Add(this.Text_WriteToTag);
            this.Controls.Add(this.btn_read);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Text_ReadResult);
            this.Controls.Add(this.Text_DeviceName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PC/SC NFC R/W";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Text_DeviceName;
        private System.Windows.Forms.TextBox Text_ReadResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_read;
        private System.Windows.Forms.TextBox Text_WriteToTag;
        private System.Windows.Forms.Button Btn_WriteToTag;
    }
}

