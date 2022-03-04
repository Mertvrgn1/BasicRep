using BasicRep.Context;
using BasicRep.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicRep
{
    public partial class FrmUnvan : Form
    {
        public FrmUnvan()
        {
            InitializeComponent();
        }
        Repository<Unvan> repUnvan = new Repository<Unvan>();
        Unvan secUnvan;
        private void FrmUnvan_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = repUnvan.set().Select(x => new
            {
                x.UnvanId,
                x.UnvanAd
            }).ToList();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            secUnvan = repUnvan.Bul((int)dataGridView1.CurrentRow.Cells[0].Value);
            //secUnvan = (Unvan)dataGridView1.CurrentRow.DataBoundItem; Diğer yolu
            txUnvan.Text = secUnvan.UnvanAd;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Unvan unvan = new Unvan();
            unvan.UnvanAd = txUnvan.Text;
            repUnvan.Ekle(unvan);
            repUnvan.Kaydet();
            FrmUnvan_Load(null, null);
            
        }

        private void btnGucel_Click(object sender, EventArgs e)
        {
            secUnvan.UnvanAd = txUnvan.Text;
            repUnvan.Guncelle();
            FrmUnvan_Load(null, null);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Unvan unvan = new Unvan();
            unvan.UnvanAd = txUnvan.Text;
            repUnvan.Sil(unvan);
            repUnvan.Kaydet();
            FrmUnvan_Load(null, null);
        }

        private void txAra_TextChanged(object sender, EventArgs e)
        {
            if (txAra.Text != "")
            {
                dataGridView1.DataSource = repUnvan.set().Where(x => x.UnvanAd.ToUpper().
           Contains(txAra.Text.ToUpper())).Select(x => new
           {
               x.UnvanId,
               x.UnvanAd
           }).ToList();
            }
            else 
                FrmUnvan_Load(null, null);
           
        }
    }
}
