using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedTec_assignment_library_managament
{
    public partial class BooksForm : Form
    {
        BookDAO _BookDAO = new BookDAO();
        Book _ActiveBook { get; set; }
        public BooksForm()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dtBookData.DataSource = _BookDAO.GetBookList();
                this.dtBookData.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAuthor.Text))
                {
                    MessageBox.Show("Author is requried!", "Invalid data");
                    return;
                }
                Book book = new Book();
                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.TotalPages = int.Parse(txtPageCount.Text);
                book.Notes = txtNotes.Text;

                _BookDAO.Save(book);

                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadGrid();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close application?", "Action confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GetActiveBook();
                if (_ActiveBook != null)
                {
                    if (string.IsNullOrWhiteSpace(_ActiveBook.Author))
                    {
                        MessageBox.Show("Author is requried!", "Invalid data");
                        return;
                    }

                    _BookDAO.Update(_ActiveBook, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadGrid();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                GetActiveBook();
                if (_ActiveBook != null)
                {
                    if (string.IsNullOrWhiteSpace(_ActiveBook.Author))
                    {
                        MessageBox.Show("Author is requried!", "Invalid data");
                        return;
                    }

                    _BookDAO.Update(_ActiveBook);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadGrid();
            }
        }

        private void GetActiveBook()
        {
            _ActiveBook = new Book();
            _ActiveBook.Id = int.Parse(dtBookData.CurrentRow.Cells["Id"].Value.ToString());
            _ActiveBook.Title = dtBookData.CurrentRow.Cells["Title"].Value.ToString();
            _ActiveBook.Author = dtBookData.CurrentRow.Cells["Author"].Value.ToString();
            _ActiveBook.TotalPages = int.Parse(dtBookData.CurrentRow.Cells["NrPages"].Value.ToString());
            _ActiveBook.Notes = dtBookData.CurrentRow.Cells["Notes"].Value.ToString();
        }

        private void txtPageCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void ClearControls()
        {
            txtAuthor.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtPageCount.Text = string.Empty;
            txtNotes.Text = string.Empty;
        }
    }
}
