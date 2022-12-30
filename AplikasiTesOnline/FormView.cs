using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiTesOnline
{
    public partial class FormView : Form
    {
        _sql sql = new _sql();

        public FormView()
        {
            InitializeComponent();
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            DataTable data = sql.GetData("CALL get_user_all()");

            DataGridViewRow dataGrid = new DataGridViewRow();
            foreach(DataRow row in data.Rows)
            {
                string created = DateTime.Parse(row["created_at"].ToString()).ToString("yyyy-MM-dd");
                string updated = DateTime.Parse(row["updated_at"].ToString()).ToString("yyyy-MM-dd");
                gridview.Rows.Add(row["id"], row["username"], row["age"], created, updated);
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
