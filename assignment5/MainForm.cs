using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personDBDataSet.Person' table. You can move, or remove it, as needed.
            this.personTableAdapter.Fill(this.personDBDataSet.Person);

        }

        // Navigator is gone but this is still here
        private void personBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.personBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.personDBDataSet);

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.personBindingSource.EndEdit();

            this.tableAdapterManager.UpdateAll(this.personDBDataSet);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            personBindingSource.RemoveCurrent(); // removes current row

            this.Validate();
            this.personBindingSource.EndEdit(); 

            this.tableAdapterManager.UpdateAll(this.personDBDataSet);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Create a new row with the data from the add form
                    var newRow = personDBDataSet.Person.NewPersonRow();
                    newRow.Name = addForm.PersonName;
                    newRow.Phone = addForm.Phone;

                    personDBDataSet.Person.Rows.Add(newRow);

                    this.Validate();
                    this.personBindingSource.EndEdit();
                    this.personTableAdapter.Update(this.personDBDataSet.Person);

                    // Refresh the data grid view
                    this.personTableAdapter.Fill(this.personDBDataSet.Person);
                }
            }
        }
    }
}
