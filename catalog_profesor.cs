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
        public int ctrl = 0;
        public catalog_profesor()
        {
            InitializeComponent();
            this.CenterToScreen();
            Server.get_materie_for_prof();
            Server.get_disc_id_prof();
            CB_disciplina.Items.AddRange(Server.CB_discipline.Items.Cast<Object>().ToArray());
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
            ctrl = 1;
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
            try
            {
                if (ctrl == 1&& dgv_elevi.CurrentCell.Value.ToString()!=null)
                {
                    global.elev_selectat = dgv_elevi.CurrentCell.Value.ToString();
                    global.medie = -1;
                    lb_med.Text = "Nu se poate calcula media";
                    Server.afis_absente_profesori_();
                    Server.afis_note_profesori_();
                    Server.nume_to_id();
                    Server.afis_media();
                    lb_medie.Visible = true;
                    lb_med.Visible = true;
                    if (global.medie != -1)
                    {
                        lb_med.Text = global.medie.ToString("0.00");
                    }

                    dgv_absente.Rows.Clear();
                    dgv_note.Rows.Clear();

                    for (int i = 0; i < Server.dataGridView1.RowCount - 1; i++)
                    {
                        DataGridViewRow row = Server.dataGridView1.Rows[i];
                        dgv_note.Rows.Add();
                        DataGridViewRow row1 = dgv_note.Rows[i];
                        row1.Cells["data_note"].Value = row.Cells["data_note"].Value;
                        row1.Cells["nota_note"].Value = row.Cells["nota_note"].Value;
                        row1.Cells["id_note"].Value = row.Cells["id_note"].Value.ToString();
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
                    dgv_note.CurrentCell = null;
                    dgv_absente.CurrentCell = null;
                }
                else MessageBox.Show("Selectati o clasa!", "Eroare!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { };
        }

        private void dgv_note_SelectionChanged(object sender, EventArgs e)
        {
            //dgv_note.CurrentCell = null;
        }

        private void dgv_absente_SelectionChanged(object sender, EventArgs e)
        {
            //dgv_absente.CurrentCell = null;
        }

        private void b_no_Click(object sender, EventArgs e)
        {
            if (String.Equals(lb_data.Text, "") || String.Equals(lb_noab.Text, "")) MessageBox.Show("Introduceti o data sau o nota/absenta valida", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                global.data_selectat = Convert.ToDateTime(lb_data.Text);
                global.noab = Convert.ToInt32(lb_noab.Text);

                Server.no_incarc();
                Server.afis_note_profesori_();
                Server.afis_media();

                lb_med.Text = global.medie.ToString("0.00");
                dgv_note.Rows.Clear();

                for (int i = 0; i < Server.dataGridView1.RowCount - 1; i++)
                {
                    DataGridViewRow row = Server.dataGridView1.Rows[i];
                    dgv_note.Rows.Add();
                    DataGridViewRow row1 = dgv_note.Rows[i];
                    row1.Cells["data_note"].Value = row.Cells["data_note"].Value;
                    row1.Cells["nota_note"].Value = row.Cells["nota_note"].Value;
                    row1.Cells["id_note"].Value = row.Cells["id_note"].Value.ToString();
                }
                lb_data.Text = "";
                lb_noab.Text = "";
                dateTimePicker1.Text = "";
                CB_noab.Text = "";
            }
        }

        private void b_ab_Click(object sender, EventArgs e)
        {
            if (String.Equals(lb_data.Text, "") || String.Equals(lb_noab.Text, "")) MessageBox.Show("Introduceti o data sau o nota/absenta valida", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                global.data_selectat = Convert.ToDateTime(lb_data.Text);
                global.noab = Convert.ToInt32(lb_noab.Text);

                Server.ab_incarc();
                Server.afis_absente_profesori_();

                dgv_absente.Rows.Clear();

                for (int i = 0; i < Server.dataGridView2.RowCount - 1; i++)
                {
                    DataGridViewRow row = Server.dataGridView2.Rows[i];
                    dgv_absente.Rows.Add();
                    DataGridViewRow row1 = dgv_absente.Rows[i];
                    row1.Cells["data_absente"].Value = row.Cells["data_absente"].Value;
                    row1.Cells["absenta_absente"].Value = row.Cells["absenta_absente"].Value;
                    row1.Cells["motivat_absente"].Value = row.Cells["motivat_absente"].Value;
                }

                lb_data.Text = "";
                lb_noab.Text = "";
                dateTimePicker1.Text = "";
                CB_noab.Text = "";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            lb_data.Text = dateTimePicker1.Text;
            global.data_selectat = Convert.ToDateTime(lb_data.Text);
        }

        private void CB_noab_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_noab.Text = CB_noab.Text;
            global.noab = Convert.ToInt32(lb_noab.Text);
        }

        private void sterg_Click(object sender, EventArgs e)
        {
            try
            {
                global.data_de_sters = Convert.ToInt32(dgv_note.SelectedCells[2].Value);
                Server.nota_delete();
                Server.afis_note_profesori_();
                Server.afis_media();

                lb_med.Text = global.medie.ToString("0.00");

                dgv_note.Rows.Clear();

                for (int i = 0; i < Server.dataGridView1.RowCount - 1; i++)
                {
                    DataGridViewRow row = Server.dataGridView1.Rows[i];
                    dgv_note.Rows.Add();
                    DataGridViewRow row1 = dgv_note.Rows[i];
                    row1.Cells["data_note"].Value = row.Cells["data_note"].Value;
                    row1.Cells["nota_note"].Value = row.Cells["nota_note"].Value;
                    row1.Cells["id_note"].Value = row.Cells["id_note"].Value;
                }
            }
            catch { };

        }

        private void reset_Click(object sender, EventArgs e)
        {
            dgv_note.CurrentCell = null;
            dgv_absente.CurrentCell = null;
        }

        private void sterg_ab_Click(object sender, EventArgs e)
        {
            try
            {
                global.data_de_sters = Convert.ToInt32(dgv_absente.SelectedCells[3].Value);
                Server.absenta_delete();
                Server.afis_absente_profesori_();

                dgv_absente.Rows.Clear();

                for (int i = 0; i < Server.dataGridView2.RowCount - 1; i++)
                {
                    DataGridViewRow row = Server.dataGridView2.Rows[i];
                    dgv_absente.Rows.Add();
                    DataGridViewRow row1 = dgv_absente.Rows[i];
                    row1.Cells["data_absente"].Value = row.Cells["data_absente"].Value;
                    row1.Cells["absenta_absente"].Value = row.Cells["absenta_absente"].Value;
                    row1.Cells["id_absente"].Value = row.Cells["id_absente"].Value;
                    row1.Cells["motivat_absente"].Value = row.Cells["motivat_absente"].Value;
                }
            }
            catch { };
        }

        private void dgv_absente_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                global.motivat = Convert.ToBoolean(dgv_absente.SelectedCells[2].Value);
                global.data_de_sters = Convert.ToInt32(dgv_absente.SelectedCells[3].Value);
                Server.absenta_motivat();
                Server.afis_absente_profesori_();

                dgv_absente.Rows.Clear();

                for (int i = 0; i < Server.dataGridView2.RowCount - 1; i++)
                {
                    DataGridViewRow row = Server.dataGridView2.Rows[i];
                    dgv_absente.Rows.Add();
                    DataGridViewRow row1 = dgv_absente.Rows[i];
                    row1.Cells["data_absente"].Value = row.Cells["data_absente"].Value;
                    row1.Cells["absenta_absente"].Value = row.Cells["absenta_absente"].Value;
                    row1.Cells["motivat_absente"].Value = row.Cells["motivat_absente"].Value;
                    row1.Cells["id_absente"].Value = row.Cells["id_absente"].Value;
                }
            }
            catch { }
        }

        private void CB_disciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            global.disciplina_input = CB_disciplina.Text;
            global.materie_selectata = CB_disciplina.Text;
            Server.get_clase();
            Server.disciplina_to_id();
            CB_clase.Items.Clear();
            CB_clase.Items.AddRange(Server.CB_clase.Items.Cast<Object>().ToArray());
        }
    }
}
