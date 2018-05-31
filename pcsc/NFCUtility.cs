using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCSC;

namespace pcsc {
    /// <summary>
    /// NFC를 사용하기 위한 유틸리티 클래스입니다.
    /// 사용하실 때 ConnectReader() 함수와 SCardMonitorInit() 함수를 반드시 차례로 실행시켜주어야 합니다.
    /// </summary>
    public class NFCUtility {
        #region 열거형
        // 전송 메세지 리스트
        public enum MessageList {
            GetBlockData,
            GetCardUID
        }
        #endregion

        #region 로컬 변수
        SCardContext SContext;
        string[] readers;

        // 이벤트
        SCardMonitor monitor;
        #endregion

        #region 초기화 함수
        /// <summary>
        /// 리더기에 Connect 합니다. 한 리더기에 여러 방식이 존재할 경우 0번째 방식을 자동으로 선택합니다.
        /// 리더기에 연결하려면 반드시 실행하셔야 하는 함수입니다.
        /// </summary>
        /// <returns>연결된 리더기의 이름을 string으로 리턴합니다.</returns>
        public string ConnectReader() {
            SContext = new SCardContext();
            SContext.Establish(SCardScope.System);

            readers = SContext.GetReaders();
            if (readers.Length <= 0) {
                throw new Exception("Reader not found");
            }
            return readers[0].ToString();
        }

        /// <summary>
        /// 리더기를 모니터링합니다. 카드가 인식되었는지, 또는 카드가 떨어졌는지에 대한 이벤트를 연결할 수 있습니다.
        /// 이벤트에 연결하려면 반드시 실행하셔야 하는 함수입니다.
        /// </summary>
        /// <param name="CardInserted">카드가 인식되었을 때 작동하는 Callback 메서드입니다. CardInsertedEvent 델리게이트 형태의 함수를 인자로 입력하십시오.</param>
        /// <param name="CardRemoved">카드가 떼어졌을 때 작동하는 Callback 메서드입니다. CardIRemovedEvent 델리게이트 형태의 함수를 인자로 입력하십시오.</param>
        public void SCardMonitorInit(CardInsertedEvent CardInserted, CardRemovedEvent CardRemoved) {
            monitor = new SCardMonitor(new SCardContext(), SCardScope.System);
            monitor.CardInserted += new CardInsertedEvent(CardInserted);
            monitor.CardRemoved += new CardRemovedEvent(CardRemoved);
            monitor.Start(readers[0].ToString());
        }
        #endregion

        #region 메세지 전송 함수
        /// <summary>
        /// NFC 리더기로 Message를 보내는 함수입니다. MessageList 열거형으로 보낼 메세지를 선택하시고 실행하시면 APDU 통신을 통해 바로 결과를 string으로 리턴합니다.
        /// </summary>
        /// <param name="message">NFC 리더기로 보낼 MessageList를 선택하여 입력합니다.</param>
        /// <returns>APDU 통신 결과를 리턴합니다.</returns>
        public string SendMessageToReader(MessageList message) {
            SCardReader reader = new SCardReader(SContext);

            // 카드에 연결합니다.
            SCardError err = reader.Connect(readers[0], SCardShareMode.Shared, SCardProtocol.T0 | SCardProtocol.T1);

            long SendPCILength = 0;
            switch (reader.ActiveProtocol) {
                case SCardProtocol.T0:
                    SendPCILength = (long)SCardPCI.T0;
                    break;
                case SCardProtocol.T1:
                    SendPCILength = (long)SCardPCI.T1;
                    break;
                default:
                    throw new Exception("Protocol not support,");
            }

            byte[] receiveBuffer = new byte[256];
            byte[] sendMessage = null;

            switch (message) {
                case MessageList.GetBlockData:
                    sendMessage = new byte[] { 0xFF, 0xB0, 0x00, 0x04, 0x04 };
                    break;

                case MessageList.GetCardUID:
                    sendMessage = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 };
                    break;
            }

            err = reader.Transmit(sendMessage, ref receiveBuffer);
            CheckError(err);

            return ByteToString(receiveBuffer);
        }

        /// <summary>
        /// NFC 리더기로 Message를 보내는 함수입니다. 보낼 명령을 Byte 배열로 입력한 후 송신할 수 있습니다.
        /// </summary>
        /// <param name="message">보낼 메세지를 Byte 배열로 입력합니다.</param>
        /// <returns>APDU 통신 결과를 리턴합니다.</returns>
        public string SendMessageToReader(byte[] message) {
            SCardReader reader = new SCardReader(SContext);

            // 카드에 연결합니다.
            SCardError err = reader.Connect(readers[0], SCardShareMode.Shared, SCardProtocol.T0 | SCardProtocol.T1);

            long SendPCILength = 0;
            switch (reader.ActiveProtocol) {
                case SCardProtocol.T0:
                    SendPCILength = (long)SCardPCI.T0;
                    break;
                case SCardProtocol.T1:
                    SendPCILength = (long)SCardPCI.T1;
                    break;
                default:
                    throw new Exception("Protocol not support,");
            }

            byte[] receiveBuffer = new byte[256];

            err = reader.Transmit(message, ref receiveBuffer);
            CheckError(err);

            return ByteToString(receiveBuffer);
        }

        /// <summary>
        /// NFC 리더기로 Message를 보내는 함수입니다. 보낼 명령어를 string 형태로 입력하면 Byte 배열로 자동 변환됩니다.
        /// </summary>
        /// <param name="message">보낼 메세지를 string 형태로 입력합니다.</param>
        /// <returns>APDU 통신 결과를 리턴합니다.</returns>
        public string SendMessageToReader(string message) {
            SCardReader reader = new SCardReader(SContext);

            // 카드에 연결합니다.
            SCardError err = reader.Connect(readers[0], SCardShareMode.Shared, SCardProtocol.T0 | SCardProtocol.T1);

            long SendPCILength = 0;
            switch (reader.ActiveProtocol) {
                case SCardProtocol.T0:
                    SendPCILength = (long)SCardPCI.T0;
                    break;
                case SCardProtocol.T1:
                    SendPCILength = (long)SCardPCI.T1;
                    break;
                default:
                    throw new Exception("Protocol not support,");
            }

            byte[] receiveBuffer = new byte[256];

            err = reader.Transmit(StringToByte(message), ref receiveBuffer);
            CheckError(err);

            return ByteToString(receiveBuffer);
        }
        #endregion

        #region NFC 태그 쓰기 함수
        /// <summary>
        /// NFC Tag에 값을 쓰는 함수입니다. 현재는 ASCII 코드를 지원하고 4글자만 사용할 수 있습니다.
        /// </summary>
        /// <param name="message">4글자의 작성 메세지를 입력합니다.</param>
        /// <returns>쓰기의 성공 여부를 확인합니다.</returns>
        public bool WriteToTag(string message) {
            if (message.Length > 4) {
                throw new Exception("Only 4 letters of the message. Sorry.");
            }
            SCardReader reader = new SCardReader(SContext);

            // 카드에 연결합니다.
            SCardError err = reader.Connect(readers[0], SCardShareMode.Shared, SCardProtocol.T0 | SCardProtocol.T1);

            long SendPCILength = 0;
            switch (reader.ActiveProtocol) {
                case SCardProtocol.T0:
                    SendPCILength = (long)SCardPCI.T0;
                    break;
                case SCardProtocol.T1:
                    SendPCILength = (long)SCardPCI.T1;
                    break;
                default:
                    throw new Exception("Protocol not support,");
            }

            byte[] receiveBuffer = new byte[256];

            byte[] header = new byte[] { 0xFF, 0xD6, 0x00, 0x04, 0x04 };
            byte[] Message = StringToByte(message);


            byte[] sendMessage;

            sendMessage = new byte[header.Length + Message.Length];

            Array.Copy(header, 0, sendMessage, 0, header.Length);
            Array.Copy(Message, 0, sendMessage, 5, Message.Length);

            err = reader.Transmit(sendMessage, ref receiveBuffer);
            CheckError(err);

            return true;
        }
        #endregion

        #region 유틸리티 함수
        /// <summary>
        /// PC/SC Library에서 정의된 에러 코드를 사람이 읽을 수 있는 코드로 변환하여 에러가 발생했다면 에러 코드를 throw합니다.
        /// </summary>
        /// <param name="err">PC/SC에 정의된 SCardError 열거형을 입력합니다.</param>
        public void CheckError(SCardError err) {
            if (err != SCardError.Success)
                throw new PCSCException(err, SCardHelper.StringifyError(err));
        }

        /// <summary>
        /// 바이트 배열을 string으로 변환합니다. 문자열의 기본 4자리까지만 표시됩니다.
        /// </summary>
        /// <param name="strByte">Bytge 배열에서 변환된 string으로 리턴됩니다.</param>
        /// <returns></returns>
        private string ByteToString(byte[] strByte) {
            string str = Encoding.Default.GetString(strByte);
            try {
                str = str.Substring(0, 4);
            }
            catch (Exception ex) {
                throw new Exception("Not Supported CommandLine");
            }

            return str;
        }

        /// <summary>
        /// 바이트 배열을 string으로 변환합니다.
        /// </summary>
        /// <param name="strByte">변환할 Byte 배열을 입력합니다.</param>
        /// <param name="length">자리수를 입력하여 몇 째자리까지 자를건지 입력합니다.</param>
        /// <returns>Byte 배열에서 변환된 string으로 리턴됩니다.</returns>
        private string ByteToString(byte[] strByte, int length) {
            string str = Encoding.Default.GetString(strByte);
            str = str.Substring(0, length);
            return str;
        }

        /// <summary>
        /// string을 Byte배열로 변환합니다.
        /// </summary>
        /// <param name="str">string 형태의 문자열을 입력합니다.</param>
        /// <returns>string 형태에서 변환된 byte 배열을 리턴합니다.</returns>
        private byte[] StringToByte(string str) {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }
        #endregion
    }
}
