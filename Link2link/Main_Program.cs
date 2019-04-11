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
        // Objetivo: Calcular o menor número de cliques para chegar de uma página inicial à uma final
        // Fazer uma busca em Largura dos links da página inicial até encontrar a final
        //      Definir Estruturas de dados principais
        //      Carregar página e recuperar links
        //      Armazenar links, verificar se a página final já foi encontrada e, se não, procurar links em cada nova páginas encontrada e repetir o processo, como uma busca em largura
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Form());
        }
    }
}
