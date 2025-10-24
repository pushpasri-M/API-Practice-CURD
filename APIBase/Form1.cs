using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace APIBase
{
    public partial class Form1 : Form
    {
        private int MaxComNum;  
        private int CurrentComNum;
        public Form1()
        {
            InitializeComponent();
            APIHelper.InitializeClient();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
        private async Task LoadImage(int image=0)
        {
          var comic = await APILoad.LoadCom(image);
            if (image == 0)
            {
                MaxComNum = comic.Num;

            }
            CurrentComNum = comic.Num;
            pictureBox1.Load(comic.Img);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 rr=new Form2();
            rr.Show();
        }
        

        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadImage();

        }

        private async void button2_Click(object sender, EventArgs e)
        {
           if(CurrentComNum<MaxComNum)
            {
                CurrentComNum++;
                await LoadImage(CurrentComNum);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(CurrentComNum>1)
            {
                CurrentComNum--;
                await LoadImage(CurrentComNum);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
