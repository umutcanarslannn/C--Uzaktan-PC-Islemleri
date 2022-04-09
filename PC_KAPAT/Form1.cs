using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Net;
using System.Threading;

namespace PC_KAPAT
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
        }
        SqlCommand cmd,cmd2;
        SqlConnection con;
        SqlCommand komut,komut2;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
       
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Server=192.168.0.2;Database= B2;User Id=veritabani;Password=vt123;");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update kapat set kapat2=@kapat";
            cmd.Parameters.AddWithValue("@kapat", 1);
            cmd.ExecuteNonQuery();
        //  Thread.Sleep(3000);
            
        }

        void pclistesi()
        {
          
            con = new SqlConnection("Server=192.168.0.2;Database=B2;User Id=veritabani;Password=vt123");
            da = new SqlDataAdapter("Select pc_ad from pc_kapat", con);
            con.Open();
            da.Fill(ds);
            con.Close();
            dataGridView1.DataSource = ds.Tables[0];
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            acilacakpclistesi();
            button3.Visible = false;
            pclistesi();
            dataGridView2.AllowUserToAddRows = false;
            timer1.Enabled = true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Server=192.168.0.2; Database=B2;User Id=veritabani;Password=vt123;");
           komut.Connection = con;           
            con.Open(); 
            string bilgisayaradi = Dns.GetHostName();
            komut.CommandText = "insert into pc_kapat (pc_ad) values (@pc_ad)";
            komut.Parameters.AddWithValue("@pc_ad", bilgisayaradi.ToString());

            komut.ExecuteNonQuery();
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==0)
            {               
             
                if (MessageBox.Show("Kapatmak istiyormusunuz","Uyarı",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    con = new SqlConnection("Server=192.168.0.2;Database=B2;User Id=veritabani;Password=vt123");
                    komut = new SqlCommand();
                    //komut2 = new SqlCommand();
                    komut.Connection = con;
                    con.Open();
                    komut.CommandText = "update pc_kapat set durum=@durum where pc_ad='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
                    komut.Parameters.AddWithValue("@durum", 1);
                    komut.ExecuteNonQuery();
                    con.Close();           
                }
            }
        }


        public static bool WakeOnLan(string MacAddress)
        {
            try
            {
                MacAddress = MacAddress.Replace("-", "");
                MacAddress = MacAddress.Replace(":", "");
                MacAddress = MacAddress.Replace(".", "");
                MacAddress = MacAddress.Replace("_", "");
                MacAddress = MacAddress.Replace(" ", "");
                MacAddress = MacAddress.Replace(",", "");
                MacAddress = MacAddress.Replace(";", "");
                if (MacAddress.Length != 12)
                {
                    //return ;  
                }

                byte[] mac = new byte[6];


                for (int k = 0; k < 6; k++)
                {
                    mac[k] = Byte.Parse(MacAddress.Substring(k * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }

                System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient();
                client.Connect(System.Net.IPAddress.Broadcast, 5004);


                byte[] packet = new byte[17 * 6];

                for (int i = 0; i < 6; i++)
                    packet[i] = 0xFF;

                for (int i = 1; i <= 16; i++)
                    for (int j = 0; j < 6; j++)
                        packet[i * 6 + j] = mac[j];

                client.Send(packet, packet.Length);
                return true;
            }
            catch
            {
                MessageBox.Show("HATA");
                return false;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
           // ds.Tables.Clear();
           // pclistesi();
      
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ds.Tables.Clear();
            pclistesi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*komut = new SqlCommand();
            con = new SqlConnection("Server=192.168.0.2; Database=B2;User Id=veritabani;Password=vt123;");
            komut.Connection = con;
            komut.CommandText = "Delete from pc_kapat where pc_ad='" + Dns.GetHostName().ToString() + "'";
            con.Open();
            komut.ExecuteNonQuery();
            con.Close();*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Server=192.168.0.2; Database=B2;User Id=veritabani;Password=vt123;");
            con.Open();
            cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "update kapat set kapat2=@kapat";
            cmd2.Parameters.AddWithValue("@kapat", 0);
            cmd2.ExecuteNonQuery();
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Kapatmak istiyormusunuz", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                con = new SqlConnection("Server=192.168.0.2;Database=B2;User Id=veritabani;Password=vt123");
                komut = new SqlCommand();
                //komut2 = new SqlCommand();
                komut.Connection = con;
                con.Open();
                
                komut.CommandText = "update pc_kapat set durum=@durum where pc_ad='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
                komut.Parameters.AddWithValue("@durum", 1);
                komut.ExecuteNonQuery();
                con.Close();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==0)
            {
                WakeOnLan(dataGridView2.CurrentRow.Cells[2].Value.ToString());
            }
        }
        
        void acilacakpclistesi()
        {
            
            con = new SqlConnection("Server=192.168.0.2;Database=B2;User Id=veritabani;Password=vt123");
            da = new SqlDataAdapter("Select Pc_ad,adres from mac_adresleri", con);
            con.Open();
            da.Fill(ds2);
            con.Close();
            dataGridView2.DataSource = ds2.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int a = dataGridView2.RowCount;
            for(int i =0 ;i<a;i++)
            {
                WakeOnLan(dataGridView2.Rows[i].Cells[2].Value.ToString());
            }
        }
    }
}
