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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        PerDbEntities db = new PerDbEntities();
        Personel secPersonel;
        Repository<Personel> repPersonel = new Repository<Personel>();
        Repository<Unvan> repUnvan = new Repository<Unvan>();

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            Doldur();
            cbYonet();
            cbUnvan();

        }

        private void cbUnvan()
        {

            cbUnvan1.DisplayMember = "UnvanAd";
            cbUnvan1.ValueMember = "UnvanId";
            cbUnvan1.DataSource = repUnvan.set().
               Select(x => new
               {
                   x.UnvanId,
                   x.UnvanAd

               }).ToList();

        }

        private void cbYonet()
        {
            cbYonetici.DisplayMember = "Ad";
            cbYonetici.ValueMember = "PersonelId";
            cbYonetici.DataSource = repPersonel.set().
                Where(x=> x.PersonelId<4).Select(x => new
                {
                    x.Ad,
                    x.PersonelId
                    

                }).ToList();



        }

        private void Doldur()
        {
            dataGridView1.DataSource = repPersonel.set().Select(x => new
            {
                x.PersonelId,
                x.Ad,
                x.Soyad,
                x.Maas,
                x.Unvan.UnvanAd,
                Yonetici = x.Personel2.Ad,


            }).ToList();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            secPersonel = repPersonel.Bul((int)dataGridView1.CurrentRow.Cells[0].Value);
            txAd.Text = secPersonel.Ad;
            txSoyad.Text = secPersonel.Soyad;
            txMaas.Text = secPersonel.Maas.ToString();
            cbYonetici.SelectedValue = secPersonel.YoneticiId;
            cbUnvan1.SelectedValue = secPersonel.UnvanId;
            //txUnvan.Text = secPersonel.Unvan.UnvanAd;      
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Personel p = new Personel();
            p.Ad = txAd.Text;
            p.Soyad = txSoyad.Text;
            p.Maas = Convert.ToInt32(txMaas.Text);
            p.UnvanId = Convert.ToInt32(cbUnvan1.SelectedValue);
            p.YoneticiId = Convert.ToInt32(cbYonetici.SelectedValue);
            repPersonel.Ekle(p);
            repPersonel.Kaydet();
            Doldur();


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            repPersonel.Sil(secPersonel);
            repPersonel.Kaydet();
            Doldur();
        }

        private void btnGuncel_Click(object sender, EventArgs e)
        {
            secPersonel.Ad = txAd.Text;
            secPersonel.Soyad = txSoyad.Text;
            secPersonel.Maas = Convert.ToInt32(txMaas.Text);
            secPersonel.UnvanId= Convert.ToInt32(cbUnvan1.SelectedValue);
            secPersonel.YoneticiId= Convert.ToInt32(cbYonetici.SelectedValue);
            repPersonel.Guncelle();
            Doldur();

        }
    }
}
