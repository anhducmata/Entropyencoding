using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int Phi,Num;
        int n;
        string nl = Environment.NewLine;
        
        
        public Form1()
        {
            InitializeComponent();
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }
        private void getPhi() {
            if (CheckTextBox() == true)
            {
                Num = Convert.ToInt32(tbNum.Text);  // nhìn là bt
                Num = int.Parse(tbNum.Text);


                Phi = Num + 1;
            }
        }
        private void tbNum_KeyPress(object sender, KeyPressEventArgs e)   // chống n dùng nhập ký tự ở TextBox
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')){
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        //private int Gt(int n) { // hàm giai thừa
        //    if (n >= 0 && n < 62)

        //        return n * Gt(n - 1);
        //    else
        //        return -1;
        //}
        private string ShowQi(int i)
        { // hiển thị Q  
            return "(" + (Num -  i) + "/" + Num + "," + i + "/" + Num + " ) ";
            
        }
        private int getBNN(int Num, int b) {  // TÌM SỐ bIẾN Ngẫu nhiên
           
            return Combine(Num, b);
        }
   
        private int Combine(int a , int b) { //  tổ hợp chỉnh b của a or aCb
            if (b == 0 || b == a)
            {
                return 1;
            }
            else
            {
                return (Combine(a - 1, b) + Combine(a - 1, b - 1)); // Binary Recursion
            }
        }
        private string InitTuma(int h) { // khởi tạo từ mã đầu tiên để đảo BIT
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
            else if (h == Num) {
                for (int i = 0; i < Num; i++)
                {
                    A += '0';
                    
                }
                return A;
            }
            else
            {
                for (int i = 0 ; i < Num - h; i++)
                {
                    strAdd += '0';
                   
                }
                A = strAdd.PadRight(h + (Num - h), '1');
                return replaceBit(ReverseString(A));
                
            }
            
                // add 0.... vào bên trái
            
        }
        private string GotoTail(int i) { // hàm  0 chạy tới đuôi 

            string S = "";
            string strA = InitTuma(i);  // tạo mã gốc
            char[] strC;                        
            char[] arrB = strA.ToCharArray();   // Copy strA qua cho arrB sau khi đã chuyển từ string qua array
            
            reGo :   // xét lại mã 
            for(int j = 1; j <Num ; j++){
                
                if (arrB[Num - j -1] < arrB[Num - j ])
                {

                    //swap(arrB[Num - j - 1], arrB[Num - j ]);  // swap ko chạy dc !!
                    char temp;
                    temp = arrB[Num - j - 1];               // swap trực tiếp
                    arrB[Num - j - 1] = arrB[Num - j];      // 
                    arrB[Num - j] = temp;                   //
                    strC = arrB;
                    S += new string(strC) + nl;   // chuyển array qua string rồi + dồn vào S có nl
                    goto reGo; 
                } 
            }

            return S;
            
        }

        public static string ReverseString(string s)   // đảo mã
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string replaceBit(string A) {   // thay thế Bit
            A = A.Replace('1','2');
            A = A.Replace('0', '1');
            A = A.Replace('2', '0');
            return A;
        }
        private double XacSuat(int k, int h) {  //  tính xác suất 
            double K = (double)k;
            double H = (double)h;
           if(k == 0){
               return (double)0.0000000000000;
           }
           else if (h == 0)
           {
               return (double)1.00000000000000;
           }
           else {
               return (-(K / Num) * Math.Log(K / Num, 2) -(H / Num) * Math.Log(H / Num, 2));
           }
        }
        private string ResTuma(int rev) { // trả về của từ mã cột cuối cùng  , chuyển đổi từ int qua binary
            string tranBin;
            
                tranBin = (Convert.ToString(rev , 2));
                tranBin.PadRight(3,'0');

            return tranBin;
        }
       private void btnQuit_Click(object sender, EventArgs e)  // Exit
       {
           this.Close();
           
       }
       private bool CheckTextBox() {  // kiểm tra n nhập 
            if(tbNum.Text == ""){
                MessageBox.Show("Text Rỗng ", "Lỗi Gía trị ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           return true;
       }
       private void btnStart_Click(object sender, EventArgs e)  //---------------START------------->>>
       {
           try
           {
               CheckTextBox();
               getPhi();
//--------------------------------------MAIN------------------------------------------
               string A = "";
               string B = "";
               A = A.PadLeft(ResTuma(Num).Length, '0');
               B = ResTuma(Num);
               for (int i = Num; i >= 0; i--) // add data lên từng Cells
               {
                   dataGridView1.Rows.Add(1);
                   dataGridView1.Rows[Num - i].Cells[0].Value = ShowQi(i);
                   dataGridView1.Rows[Num - i].Cells[1].Value = getBNN(Num, Num - i);
                   dataGridView1.Rows[Num - i].Cells[3].Value = XacSuat(Num - i, i).ToString();
                   dataGridView1.Rows[Num - i].Cells[4].Value = TumaSau(Num - i + 1);
                   dataGridView1.Rows[0].Cells[4].Value = A;
                   
               }
               dataGridView1.Rows.Add();
               dataGridView1.Rows[Num].Cells[4].Value = B;  // vì nếu add cùng với vòng for có i (values = 0) => error (div 0)nên đặt ngoài
               
               for (int i = 0; i <= Num; i++) {  // Add data lên cột 2
                   
                   dataGridView1.Rows.Add();
                   dataGridView1.Rows[i].Cells[2].Value = InitTuma(i) + nl + GotoTail(i) ;
               }
                
//------------------------------------END MAIN-----------------------------------------

           }
           catch (Exception ex) {  // bắt Error
                MessageBox.Show("Lỗi : " + ex.ToString(),"Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
           }
       }

      
      
       private string TumaSau(int i) { // từ mã cột 4
           string A = "";
           string B = "";
           string C = "";
               
               B = ResTuma(i-1);
                
               if (B.Length < ResTuma(Num).Length) {
                   B = B.PadLeft(B.Length + (ResTuma(Num).Length - B.Length), '0');
               }
          
               for (int j = 0; j < getBNN(Num, i -1) ; j++)
               {
                   

                       C = ResTuma(j).PadLeft(ResTuma(j).Length + (ResTuma(getBNN(Num, i - 1)).Length - ResTuma(j).Length), '0');


                       
                   A += B + "  " + C + nl;
                   
               }
           return A;
       }

       private void groupBox4_Enter(object sender, EventArgs e)
       {

       }

       private void tbNum_TextChanged(object sender, EventArgs e)
       {
           tbNum.Text = "";
       }

       private void tbNum_MouseClick(object sender, MouseEventArgs e)
       {
           tbNum.Text = "";
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
           catch (Exception ex) {
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
    }
}
