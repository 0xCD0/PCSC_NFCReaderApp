using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCSC;

namespace pcsc {
    public partial class MainForm : Form {
        // NFC Utility Class
        NFCUtility nfc;

        public MainForm() {
            InitializeComponent();

            // 크로스 쓰레드 위반 오류를 해제해주기 위해 사용되었습니다.
            CheckForIllegalCrossThreadCalls = false;

            try {
                // NFC Utility 클래스를 사용하기 위해 초기화합니다.
                // ConnectReader()를 사용하여 초기화, SCardMonitorInit()를 사용하여 삽입, 제거 이벤트를 설정합니다.
                // 아래 두 함수는 반드시 호출하셔야 합니다.
                nfc = new NFCUtility();
                Text_DeviceName.Text = nfc.ConnectReader();
                nfc.SCardMonitorInit(card_Inserted, card_Removed);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 카드가 삽입되었을때에 대한 CallBack 이벤트입니다.
        /// </summary>
        private void card_Inserted(object sender, CardStatusEventArgs e) {
            label1.Text = "Card Inserted";
        }

        /// <summary>
        /// 카드가 제거되었을때에 대한 CallBack 이벤트입니다.
        /// </summary>
        private void card_Removed(object sender, CardStatusEventArgs e) {
            label1.Text = "Card Removed";
        }

        /// <summary>
        /// 카드를 리더기에 인식시킨 상태에서 호출해야 하는 버튼 이벤트 함수입니다.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_read_Click(object sender, EventArgs e) {
            try {
                // GetBlockData는 블록에 쓰여진 데이터를 읽어오는 열거형입니다.
                // 열거형 리스트중에 카드의 고유 UID를 가져오는 열거형도 존재합니다.
                Text_ReadResult.Text = nfc.SendMessageToReader(NFCUtility.MessageList.GetBlockData);
            }
            catch (Exception ex) {
                MessageBox.Show("Card has been not taged");
            }
        }

        /// <summary>
        /// 입력한 string을 카드 데이터에 씁니다. 현재는 4글자까지밖에 지원을 안하고 한글도 지원하지 않습니다.
        /// </summary>
        private void Btn_WriteToTag_Click(object sender, EventArgs e) {
            try {

                if(Text_WriteToTag.Text == string.Empty || Text_WriteToTag.Text == "") {
                    MessageBox.Show("Write message is EMPTY. Please write to text on message");
                    return;
                }

                if (nfc.WriteToTag(Text_WriteToTag.Text)) {
                    label1.Text = "Write Success";
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}