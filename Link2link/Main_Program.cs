using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Link2link
{
    class Main_Program
    {
        // Objetivo: Calcular o menor número de cliques entre duas páginas da Wikipedia sem sair da Wikipedia
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Form());
        }
    }
}
