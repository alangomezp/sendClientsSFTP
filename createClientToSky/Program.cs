using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using createClientToSky.Models;

namespace createClientToSky
{
    class Program
    {
        static void Main(string[] args)
        {
            Funciones func = new Funciones();

            func.createCSVClientes();
        }
    }
}
