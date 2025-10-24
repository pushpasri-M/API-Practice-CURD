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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            APIHelper.InitializeClient();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(GridView);
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }
        private BindingList<BookModel> bookBindingList;


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private async void GridView(object sender, EventArgs e)
        {
            var (book,statusCode,Response) = await CURDop.BookLoad.LoadBook();
            try
            {

                bookBindingList = new BindingList<BookModel>(book);
                dataGridView1.DataSource = bookBindingList;
                dataGridView1.AllowUserToAddRows = false;

                // Hide Id column
                if (dataGridView1.Columns.Contains("Id"))
                    dataGridView1.Columns["Id"].Visible = false;
                if (!dataGridView1.Columns.Contains("Delete"))
                {
                    DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                    btnColumn.HeaderText = "Remove";
                    btnColumn.Name = "Delete";
                    btnColumn.Text = "Delete";
                    btnColumn.UseColumnTextForButtonValue = true;

                    // ✨ Optional styling for a flat, colored button
                    btnColumn.FlatStyle = FlatStyle.Flat;
                    btnColumn.DefaultCellStyle.BackColor = Color.IndianRed;
                    btnColumn.DefaultCellStyle.ForeColor = Color.White;
                    btnColumn.DefaultCellStyle.SelectionBackColor = Color.Firebrick;
                    btnColumn.DefaultCellStyle.SelectionForeColor = Color.White;
                    btnColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns.Add(btnColumn);
                }
                if (!dataGridView1.Columns.Contains("Edit"))
                {
                    DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                    editColumn.HeaderText = "Update";
                    editColumn.Name = "Edit";
                    editColumn.Text = "Edit";
                    editColumn.UseColumnTextForButtonValue = true;

                    // Optional styling for flat, colored button
                    editColumn.FlatStyle = FlatStyle.Flat;
                    editColumn.DefaultCellStyle.BackColor = Color.SteelBlue;
                    editColumn.DefaultCellStyle.ForeColor = Color.White;
                    editColumn.DefaultCellStyle.SelectionBackColor = Color.DodgerBlue;
                    editColumn.DefaultCellStyle.SelectionForeColor = Color.White;
                    editColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridView1.Columns.Add(editColumn);
                }

                MessageBox.Show($"Data Loaded Successfully with Status Code:{statusCode} --> {Response}","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
            }
            catch (Exception)
            {
                MessageBox.Show($"Error Occured: {statusCode} ---> {Response}","API Response",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                string bookId = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                var result = await BookLoad.DeleteBook(bookId);

                if (result.success)
                {
                    MessageBox.Show("Book deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var (books, _, _) = await BookLoad.LoadBook();
                    dataGridView1.DataSource = books;
                }
                else
                {
                    MessageBox.Show($"Failed to delete. Reason: {result.reason}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                BookModel book = new BookModel
                {
                    StudentID = (int)dataGridView1.Rows[e.RowIndex].Cells["StudentID"].Value,
                    StudentName = dataGridView1.Rows[e.RowIndex].Cells["StudentName"].Value.ToString(),
                    BookISBN = (int)dataGridView1.Rows[e.RowIndex].Cells["BookISBN"].Value
                };

                Form3 editForm = new Form3(book, id);
                editForm.Show();
                this.Hide();

                // Reload grid after closing edit form
                await CURDop.BookLoad.LoadBook();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            Form3 r = new Form3();
            this.Hide();
            r.Show();
           
        }
    }
}
