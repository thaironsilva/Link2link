using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Link2link
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
            // Páginas teste
            pFrom.Text = "https://en.wikipedia.org/wiki/Pressure_suit";
            pTo.Text = "https://en.wikipedia.org/wiki/Evenk_Autonomous_Okrug";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WebClient w = new WebClient();
            Boolean found = false;      // Determina se a página final já foi encontrada ou não
            string html = "";
            int index = 0;      // Indice do hashmap

            try
            {
                html = w.DownloadString(pFrom.Text);        // Testa se o link na página inicial é válido
                if (!pFrom.Text.StartsWith("https://en.wikipedia.org/"))        // Testa se o link é da wikipedia em ingles
                    throw new NotWikiException("Não é uma página wiki em Inglês.");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Link inicial inválido");
                return;
            }

            try
            {
                w.DownloadString(pTo.Text);        // Testa se o link na página final é válido
                if (!pTo.Text.StartsWith("https://en.wikipedia.org/"))      // Testa se o link é da wikipedia em ingles
                    throw new NotWikiException("Não é uma página wiki em Inglês.");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Link final inválido");
                return;
            }

            result_label.Text = "calculando...";
            path_list.Items.Clear();
            label3.Text = "";
            Refresh();

            Page initial = new Page(pFrom.Text, null);      // Cria página inicial
            initial.SetLinks();
            Page final = new Page(pTo.Text, null);          // Cria página final

            Dictionary<string, int> hash_map = new Dictionary<string, int>();       // Cria hashmap que serve pra verificar se página já foi descoberta e/ou visitada antes
            hash_map.Add(pFrom.Text, index++);        // Adiciona a página inicial no hashmap
            //hash_map.Add(pTo.Text, index++);      // Adiciona página final no hashmap

            int value;
            List<Page> current_pages = new List<Page>();
            List<Page> next_pages = new List<Page>();

            foreach (Page page in initial.Links)
            {
                if (page.Url == pTo.Text)
                {
                    found = true;
                    final = page;
                }
                current_pages.Add(page);
            }

            // Busca em Largura
            while(!found)
            {
                next_pages = new List<Page>();
                foreach (Page page in current_pages)
                {
                    if (!hash_map.TryGetValue(page.Url, out value))
                    {
                        page.SetLinks();
                        hash_map.Add(page.Url, index++);
                        foreach (Page next in page.Links)
                        {
                            if (next.Url == pTo.Text)
                            {
                                found = true;
                                final = next;
                                break;
                            }
                            next_pages.Add(next);
                        }
                    }
                    if (found)
                        break;
                }
                current_pages = next_pages;
            }

            label3.Text = "Caminho:";
            //string msg = final.Url;
            path_list.Items.Add(final.Url);
            int count = 0;

            while (final.Root != null)
            {
                //msg += final.Root.Url + "\n";
                path_list.Items.Add(final.Root.Url);
                final = final.Root;
                count++;
            }

            //label_path.Text = msg;
            result_label.Text = count.ToString() + "    CLIQUES \nentre os dois links \né o menor caminho.";
        }

        // Excessão 
        public class NotWikiException : Exception
        {
            public NotWikiException()
            {

            }
            public NotWikiException(string _msg) : base(_msg)
            {

            }
        }
    }
}
