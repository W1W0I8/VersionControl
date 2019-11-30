using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();

            label1.Text = Resource1.FullName;            
            button1.Text = Resource1.Add;
            button2.Text = Resource1.SaveToFile;
            button3.Text = Resource1.DeleteSelectedItem; // újonnan került hozzáadásra (Resource1)

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text,
                
            };
            users.Add(u);
            textBox1.Text = ""; // a Textbox tartalmának kiürítése --> könnyebb újabb adatot "felvinni" ily módon
        }

        private void button2_Click(object sender, EventArgs e) // mentés fájlba
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() != DialogResult.OK) return;

            StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
            foreach (User item in users)
            {
                sw.WriteLine($"{item.ID};{item.FullName}");
            }
            sw.Close();
        }

        private void button3_Click(object sender, EventArgs e) // a kiválasztott listaelem törlése
        {
           
            try
            {
                var ID = listBox1.SelectedValue;
                var delete = from x in users
                             where x.ID.ToString() == ID.ToString()
                             select x;
                users.Remove(delete.FirstOrDefault());
            }
            catch (Exception)
            {
                MessageBox.Show("Nem sikerült törölni a kiválasztott elemet.");
            }
        }
    }
}
