using Demos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormCliente
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TodoEjercitos ejer = new TodoEjercitos();
            //ejer.Show();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn columnaId = new DataGridViewTextBoxColumn();
            columnaId.HeaderText = "Id";
            columnaId.Width = 50;
            dataGridView1.Columns.Add(columnaId);


            DataGridViewTextBoxColumn columnaName = new DataGridViewTextBoxColumn();
            columnaName.HeaderText = "Nombre";
            dataGridView1.Columns.Add(columnaName);

            DataGridViewTextBoxColumn columnaFnac = new DataGridViewTextBoxColumn();
            columnaFnac.HeaderText = "Fecha Nacimiento";
            columnaFnac.Width = 200;
            dataGridView1.Columns.Add(columnaFnac);

            DataGridViewTextBoxColumn columnaFuerza = new DataGridViewTextBoxColumn();
            columnaFuerza.HeaderText = "Fuerza Combate";
            columnaFuerza.Width = 80;
            dataGridView1.Columns.Add(columnaFuerza);

            DataGridViewTextBoxColumn columnaUbicacion = new DataGridViewTextBoxColumn();
            columnaUbicacion.HeaderText = "Ubicación";
            dataGridView1.Columns.Add(columnaUbicacion);


            IEnumerable <Ejercito> Ejercitosos = ListarEjercitos();
            foreach(Ejercito eje in Ejercitosos)
            {
                dataGridView1.Rows.Add(eje.Id.ToString(), eje.Name, eje.FechNac.ToString(), eje.FuerzaCombate.ToString(), eje.Ubicacion);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Poderoso pod = new Poderoso();
            //pod.Show();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn columnaId = new DataGridViewTextBoxColumn();
            columnaId.HeaderText = "Id";
            columnaId.Width = 50;
            dataGridView1.Columns.Add(columnaId);


            DataGridViewTextBoxColumn columnaName = new DataGridViewTextBoxColumn();
            columnaName.HeaderText = "Nombre";
            dataGridView1.Columns.Add(columnaName);

            DataGridViewTextBoxColumn columnaFnac = new DataGridViewTextBoxColumn();
            columnaFnac.HeaderText = "Fecha Nacimiento";
            columnaFnac.Width = 200;
            dataGridView1.Columns.Add(columnaFnac);

            DataGridViewTextBoxColumn columnaFuerza = new DataGridViewTextBoxColumn();
            columnaFuerza.HeaderText = "Fuerza Combate";
            columnaFuerza.Width = 80;
            dataGridView1.Columns.Add(columnaFuerza);

            DataGridViewTextBoxColumn columnaUbicacion = new DataGridViewTextBoxColumn();
            columnaUbicacion.HeaderText = "Ubicación";
            dataGridView1.Columns.Add(columnaUbicacion);

            Ejercito? eje = EjercitoMasPoderoso();

            if (eje != null)
            {
                dataGridView1.Rows.Add(eje.Id.ToString(), eje.Name, eje.FechNac.ToString(), eje.FuerzaCombate.ToString(), eje.Ubicacion);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TodoEjercitos todo = new TodoEjercitos();
            //todo.Show();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn columnaNum = new DataGridViewTextBoxColumn();
            columnaNum.HeaderText = "Numero Ejercitos";
            dataGridView1.Columns.Add(columnaNum);

            DataGridViewTextBoxColumn columnaFTotal = new DataGridViewTextBoxColumn();
            columnaFTotal.HeaderText = "Fuerza Total";
            dataGridView1.Columns.Add(columnaFTotal);

            DataGridViewTextBoxColumn columnaFMedia = new DataGridViewTextBoxColumn();
            columnaFMedia.HeaderText = "Fuerza Media";
            dataGridView1.Columns.Add(columnaFMedia);

            DataGridViewTextBoxColumn columnaSExp = new DataGridViewTextBoxColumn();
            columnaSExp.HeaderText = "Suma Años Experiencia";
            dataGridView1.Columns.Add(columnaSExp);

            DataGridViewTextBoxColumn columnaMExp = new DataGridViewTextBoxColumn();
            columnaMExp.HeaderText = "Media Años Experiencia";
            dataGridView1.Columns.Add(columnaMExp);

            dataGridView1.Rows.Add(NumEjer(), Fcombat(), FMcombat(), AExp(), AMExp());
        }

        private IEnumerable<Ejercito> ListarEjercitos()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            return context.Ejercitosos.Execute();
        }
        private Ejercito? EjercitoMasPoderoso()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            var ejercitosos = context.Ejercitosos.Execute();

            var ejercit = ejercitosos.OrderByDescending(x => x.FuerzaCombate).FirstOrDefault(); 
            return ejercit;
        }

        private string NumEjer() 
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            var ejercitosos = context.Ejercitosos.Execute();
            return ejercitosos.Count().ToString();
        }

        private string Fcombat()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            var ejercitosos = context.Ejercitosos.Execute();
            return ejercitosos.Sum(x=>x.FuerzaCombate).ToString();
        }

        private string FMcombat()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            var ejercitosos = context.Ejercitosos.Execute();
            return ejercitosos.Average(x => x.FuerzaCombate).ToString();
        }

        private string AExp()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            var ejercitosos = context.Ejercitosos.Execute();
            List<Double> date = new List<Double>();
            foreach (Ejercito eje in ejercitosos)
            {
                var a = eje.FechNac;
                DateTime utc = a.UtcDateTime;
                var horas = (DateTime.Now - utc).TotalHours;
                date.Add(horas);
            }
            return Math.Round(date.Sum()/8760,2).ToString();
        }

        private string AMExp()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            var ejercitosos = context.Ejercitosos.Execute();
            List<Double> date = new List<Double>();
            foreach (Ejercito eje in ejercitosos)
            {
                var a = eje.FechNac;
                DateTime utc = a.UtcDateTime;
                var horas = (DateTime.Now - utc).TotalHours;
                date.Add(horas);
            }
            return Math.Round(date.Average()/8760, 2).ToString();
        }
    }
}
