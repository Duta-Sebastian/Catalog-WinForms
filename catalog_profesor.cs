﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catalog
{
    public partial class catalog_profesor : Form
    {
        public catalog_profesor()
        {
            InitializeComponent();
            Server.get_clase();
            Server.get_materie_for_prof();
            CB_clase.Items.AddRange(Server.CB_clase.Items.Cast<Object>().ToArray());
        }

        private void inapoi_Click(object sender, EventArgs e)
        {
            global.F_curent = new ter_profesor();
            this.Hide();
            global.F_curent.ShowDialog();
            this.Close();
        }

        private void CB_clase_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_absente.Rows.Clear();
            dgv_note.Rows.Clear();
            global.clasa_input = CB_clase.Text;
            Server.get_elevi_from_clase();
            for (int i = 0; i < Server.get_elvcls.RowCount - 1; i++)
            {
                DataGridViewRow row = Server.get_elvcls.Rows[i];
                dgv_elevi.Rows.Add();
                DataGridViewRow row1 = dgv_elevi.Rows[i];
                row1.Cells["nr_elev"].Value = Convert.ToString(i + 1);
                row1.Cells["nume_elev"].Value = row.Cells["nume_elev"].Value;
            }
            dgv_elevi.CurrentCell = null;
        }

        private void dgv_elevi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            global.elev_selectat = dgv_elevi.CurrentCell.Value.ToString();

            Server.afis_absente_profesori_();
            Server.afis_note_profesori_();

            dgv_absente.Rows.Clear();
            dgv_note.Rows.Clear();

            for (int i = 0; i < Server.dataGridView1.RowCount - 1; i++)
            {
                DataGridViewRow row = Server.dataGridView1.Rows[i];
                dgv_note.Rows.Add();
                DataGridViewRow row1 = dgv_note.Rows[i];
                row1.Cells["data_note"].Value = row.Cells["data_note"].Value;
                row1.Cells["nota_note"].Value = row.Cells["nota_note"].Value;
            }

            for (int i = 0; i < Server.dataGridView2.RowCount - 1; i++)
            {
                DataGridViewRow row = Server.dataGridView2.Rows[i];
                dgv_absente.Rows.Add();
                DataGridViewRow row1 = dgv_absente.Rows[i];
                row1.Cells["data_absente"].Value = row.Cells["data_absente"].Value;
                row1.Cells["absenta_absente"].Value = row.Cells["absenta_absente"].Value;
                row1.Cells["motivat_absente"].Value = row.Cells["motivat_absente"].Value;
            }
        }

        private void dgv_note_SelectionChanged(object sender, EventArgs e)
        {
            dgv_note.CurrentCell = null;
        }

        private void dgv_absente_SelectionChanged(object sender, EventArgs e)
        {
            dgv_absente.CurrentCell = null;
        }

        private void b_no_Click(object sender, EventArgs e)
        {

        }

        private void b_ab_Click(object sender, EventArgs e)
        {

        }
    }
}
