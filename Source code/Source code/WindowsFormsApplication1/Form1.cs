using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int Phi, Num;       // khai báo Biến Num là số người dùng nhập
        int n;
        string nl = Environment.NewLine;    // tạo biến new line để xuống dòng


        public Form1()
        {

            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
        private void getPhi() /*Hàm này dùng để tính Phi*/
        {
            if (CheckTextBox() == true)
            {
                Num = Convert.ToInt32(tbNum.Text);          // chuyển giá trị trong textbox  thành kiểu Int
                Num = int.Parse(tbNum.Text);                //


                Phi = Num + 1;
            }
        }
        private void tbNum_KeyPress(object sender, KeyPressEventArgs e)   // Ngăn người dùng nhập ký tự ở TextBox
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    
        private string ShowQi(int i)
        { // hiển thị Qi  
            return "(" + (Num - i) + "/" + Num + "," + i + "/" + Num + " ) ";

        }
        private int getBNN(int Num, int b)
        {  // TÌM số Biến  Ngẫu nhiên : giá trị của cột 1

            return Combine(Num, b);  
        }

        private int Combine(int a, int b)
        { //  tổ hợp chỉnh b của a or aCb
            if (b == 0 || b == a)
            {
                return 1;
            }
            else
            {
                return (Combine(a - 1, b) + Combine(a - 1, b - 1));  //đệ quy
            }
        }
        private string InitTuma(int h)
        { // khởi tạo từ mã đầu tiên để đảo BIT
            /*{
             *      trong quá trình tính toán kết quả thường xuyên có dạng   111 hay 1111 hay 111111
             *      hàm này chèn số 0 vào trước nó để đủ bit  =>            0111  hay 0001111 hay 00111111
             * 
             * } 
             */
            string A = "";
            string strAdd = "";
            if (h == 0)
            {
                for (int i = 0; i < Num; i++)
                {
                    A += '1';

                }
                return A;
            }
            else if (h == Num)
            {
                for (int i = 0; i < Num; i++)
                {
                    A += '0';

                }
                return A;
            }
            else
            {
                for (int i = 0; i < Num - h; i++)
                {
                    strAdd += '0';

                }
                A = strAdd.PadRight(h + (Num - h), '1');
                return replaceBit(ReverseString(A));

            }

            // add 0.... vào bên trái

        }
        private string GotoTail(int i)
            /*      Yêu cầu : nếu mã là 000111111 thì làm sao dịch chuyển mọi số 0 về đuôi của mã (gotoTail), mỗi lần mã thay đổi thì lưu lại 1 lần
             *      Thuật Toán : xét từ bên phải qua nếu bên phải > bên trái thì đổi chổ , lưu kết quả lại , ngay sau đó quay lại vị trí ban đầu và chạy tiếp
             *                      cứ như vậy cho đến khi dừng là khi mọi số 0 đều qua bên phải.
             */
        { // hàm  0 chạy tới đuôi 

            string S = "";
            string strA = InitTuma(i);  // tạo mã gốc (mã đầu tiên của 1 hàng)
            char[] strC;
            char[] arrB = strA.ToCharArray();   // Copy strA qua cho arrB sau khi đã chuyển từ string qua array

            reGo:   // xét lại mã 
            for (int j = 1; j < Num; j++)
            {

                if (arrB[Num - j - 1] < arrB[Num - j])
                {

                    //swap(arrB[Num - j - 1], arrB[Num - j ]);  // swap gián tiếp ko chạy dc !!
                    char temp;
                    temp = arrB[Num - j - 1];               // swap trực tiếp
                    arrB[Num - j - 1] = arrB[Num - j];      // 
                    arrB[Num - j] = temp;                   //
                    strC = arrB;
                    S += new string(strC) + nl;   // chuyển array qua string rồi + dồn vào S có nl(newline)
                    goto reGo;  // quay lại vị trí ban đầu
                }
            }

            return S;

        }

        public static string ReverseString(string s)   // đảo mã vidu : 00011111 => 11111000
        {
            char[] arr = s.ToCharArray();       //  khởi tạo 1 array mới có giá trị bằng array mà string s đã chuyển đổi qua
            Array.Reverse(arr);                 //  Đảo mã
            return new string(arr);             //  chuyển array về string 
        }
        public static string replaceBit(string A) /*Nếu bit là 0 thì thay = 1 nếu 1 thì thay = 0*/
        {   // thay thế Bit
            A = A.Replace('1', '2');
            A = A.Replace('0', '1');
            A = A.Replace('2', '0');
            return A;
        }
        private double XacSuat(int k, int h)
        {  //  tính xác suất 
            double K = (double)k;
            double H = (double)h;
            if (k == 0)
            {
                return (double)0.0000000000000;
            }
            else if (h == 0)
            {
                return (double)1.00000000000000;
            }
            else
            {
                return (-(K / Num) * Math.Log(K / Num, 2) - (H / Num) * Math.Log(H / Num, 2));
            }
        }
        private string ResTuma(int rev) /*Chyển đổi 1 số nguyên sang Nhị phân*/
        { // trả về của từ mã cột cuối cùng  , chuyển đổi từ int qua binary
            string tranBin;

            tranBin = (Convert.ToString(rev, 2));  // rev : số nguyên cần chuyển
            tranBin.PadRight(3, '0');

            return tranBin;
        }
        private void btnQuit_Click(object sender, EventArgs e)  // Actions Exit
        {
            this.Close();

        }
        private bool CheckTextBox()
        {  // kiểm tra người nhập 
            if (tbNum.Text == "")
            {
                MessageBox.Show("Text Rỗng ", "Lỗi Gía trị ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnStart_Click(object sender, EventArgs e)  // Actions Start
        {
            try
            {
                CheckTextBox();
                getPhi();
                string A = "";
                string B = "";
                A = A.PadLeft(ResTuma(Num).Length, '0');
                B = ResTuma(Num);
                for (int i = Num; i >= 0; i--)                                                              // add data lên từng Cells
                {                                                                                           //        
                    dataGridView1.Rows.Add(1);                                                              //
                    dataGridView1.Rows[Num - i].Cells[0].Value = ShowQi(i);                                 //            
                    dataGridView1.Rows[Num - i].Cells[1].Value = getBNN(Num, Num - i);                      //        
                    dataGridView1.Rows[Num - i].Cells[3].Value = XacSuat(Num - i, i).ToString();
                    dataGridView1.Rows[Num - i].Cells[4].Value = TumaSau(Num - i + 1);
                    dataGridView1.Rows[0].Cells[4].Value = A;

                }
                dataGridView1.Rows.Add();
                dataGridView1.Rows[Num].Cells[4].Value = B;  // vì nếu add cùng với vòng for có i (values = 0) => error (div 0)nên đặt ngoài

                for (int i = 0; i <= Num; i++)
                {  // Add data lên cột 2

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[2].Value = InitTuma(i) + nl + GotoTail(i);
                }

                //------------------------------------END MAIN-----------------------------------------

            }
            catch (Exception ex)
            {  // bắt Error
                MessageBox.Show("Lỗi : " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private string TumaSau(int i) 
        { // từ mã cột 4 
            string A = "";
            string B = "";
            string C = "";

            B = ResTuma(i - 1); 

            if (B.Length < ResTuma(Num).Length)
            {
                B = B.PadLeft(B.Length + (ResTuma(Num).Length - B.Length), '0');
            }

            for (int j = 0; j < getBNN(Num, i - 1); j++)
            {


                C = ResTuma(j).PadLeft(ResTuma(j).Length + (ResTuma(getBNN(Num, i - 1)).Length - ResTuma(j).Length), '0');  // add '0' vào đầu mỗi từ mã 



                A += B + "  " + C + nl;                             // add dồn lại lưu vào A

            }
            return A;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }


        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void lbGV_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // Hiển thị values lên messageBox thay vì tooltip => get all values
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), "Cells Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Đừng nhấn bậy bạ !!","Cảnh Báo" + ex.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)   // reset ALL và làm lại từ đầu, 
        {
            dataGridView1.Rows.Clear();
        }


       private void getData(DataGridView dGV, string filename) /*gan gia tri vao bien de chuan bi Print*/
        {
            string stOutput = "\n\nVi so luong day nguon va Tu Ma nhieu nen duoc ghi o duoi !!! \n\n\n";
            // Export titles:
            string sHeaders = "";   
            string temp = "";
           string temp4 = "";
            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t"; // tab title qua
            stOutput += sHeaders + "\n";
            // Export data. Vì cot 2 va 4 co so luong tu ma lon nen phai format lai bang sau khi Export
            for (int i = 0; i <= Num; i++ ) {
                string strDataToEx = "";
                for (int j = 0; j <= 4; j++ ) {
                    if(dataGridView1.Rows[i].Cells[j].Value == null){  // kiem tra Neu Null thi khong lam gi
                        // do nothing =))
                    }
                    else if( j == 2 ){  
                        temp = "Si" + "\n";

                        for (int k = 0; k <= Num; k++)
                        {
                            temp += Convert.ToString(dataGridView1.Rows[k].Cells[0].Value) + "\n";
                            temp += Convert.ToString(dataGridView1.Rows[k].Cells[2].Value) + "\n";
                        }
                        }else if( j == 1 ){
                        strDataToEx += Convert.ToString(dataGridView1.Rows[i].Cells[j].Value) + "\t\t";

                    }
                    else if (j == 4)
                    {
                        temp4 = "wi" + "\n";

                        for (int k = 0; k <= Num; k++)
                        {
                            temp4 += Convert.ToString(dataGridView1.Rows[k].Cells[0].Value) + "\n";
                            temp4 += Convert.ToString(dataGridView1.Rows[k].Cells[4].Value) + "\n";
                        }
                        } else{
                        strDataToEx += Convert.ToString(dataGridView1.Rows[i].Cells[j].Value) + "\t";
                    }
                   
                }
                stOutput += strDataToEx + "\n" ;
            }
            stOutput += "\n\n\n\n" + temp + "\n\n\n\n" + temp4;
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);  // su dung Filestream 
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); 
            bw.Flush();
            bw.Close();
            fs.Close();
        } 
       
        private void btnExport_Click(object sender, System.EventArgs e) // actions Export ra file Excell
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "BảngMãHóaNguồnPhổQuátm=" + Num;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                
                getData(dataGridView1, sfd.FileName); 
              
            }  
        }  

       
    }
    
       
  }

