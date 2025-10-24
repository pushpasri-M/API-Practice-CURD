using APIBase.CURDop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIBase
{
    public partial class Form3 : Form
    {
        private string recordId = null; // store ID for update
        private bool isEditMode = false;
        public Form3()
        {
            InitializeComponent();
            APIHelper.InitializeClient();
            this.StartPosition = FormStartPosition.CenterScreen;


        }
        public Form3(BookModel book, string Id)
        {
            InitializeComponent();
            APIHelper.InitializeClient();
            this.StartPosition = FormStartPosition.CenterScreen;
            recordId = Id;
            isEditMode = true;

            // Pre-fill fields
            textBox2.Text = book.StudentID.ToString();
            textBox1.Text = book.StudentName;
            textBox3.Text = book.BookISBN.ToString();

            button2.Text = "UPDATE";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private async void Create(object sender, EventArgs e)
        { 
            if (!ValidateForm())
            {
                return; // Stop if validation fails
            }

            BookModel book = new BookModel
            {
                StudentID = int.Parse(textBox2.Text),
                StudentName = textBox1.Text,
                BookISBN = int.Parse(textBox3.Text)
            };

            if (isEditMode)
            {
                // 🔹 Update existing record
                var (updatedBook, status, message) = await CURDop.BookLoad.UpdateBookAsync(recordId, book);
                MessageBox.Show($"Updated Successfully with response code:{status}-->{message}", "Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                // 🔹 Create new record
                var (addedBook, status, message) = await BookLoad.AddBookAsync(book);
                MessageBox.Show($"Updated Successfully with response code:{status}-->{message}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Create(sender, e);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 r = new Form4();
            this.Hide();
            r.Show();
        }
        private bool ValidateForm()
        {
            bool isValid = true;
            errorProvider1.Clear(); // Clear previous errors

            // StudentID
            if (string.IsNullOrWhiteSpace(textBox2.Text) || !int.TryParse(textBox2.Text, out _))
            {
                errorProvider1.SetError(textBox2, "Enter a valid Student ID");
                isValid = false;
            }

            // StudentName
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Enter Student Name");
                isValid = false;
            }

            // BookISBN
            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text, out _))
            {
                errorProvider1.SetError(textBox3, "Enter a valid Book ISBN");
                isValid = false;
            }

            return isValid;
        }
       

    }
}
