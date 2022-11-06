using Demos;
using Microsoft.AspNetCore.Mvc;

namespace WebCliente.Controllers
{
    public class EjercitoController : Controller
    {
        public IActionResult Index()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            IEnumerable<Ejercito> eje= context.Ejercitosos.Execute();
            return View(eje);
        }

        public IActionResult PotEjercito()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            IEnumerable<Ejercito> ejercitosos = context.Ejercitosos.Execute();
            Ejercito? ejercit = ejercitosos.OrderByDescending(x => x.FuerzaCombate).FirstOrDefault();
            return View(ejercit);
        }

        public IActionResult Resumen()
        {
            var serviceRoot = "http://localhost:55093/";
            var context = new DefaultContainer(new Uri(serviceRoot));
            IEnumerable<Ejercito> eje = context.Ejercitosos.Execute();
            List<string> list = new List<string>();
            list.Add(NumEjer());
            list.Add(Fcombat());
            list.Add(FMcombat());
            list.Add(AExp());
            list.Add(AMExp());
            return View(list);
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
            return ejercitosos.Sum(x => x.FuerzaCombate).ToString();
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
            return Math.Round(date.Sum() / 8760, 2).ToString();
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
            return Math.Round(date.Average() / 8760, 2).ToString();
        }
    }
}
