using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace Link2link
{
    // Classe Página, que contém página de origem e lista de links contidos nela
    // A variável root é a página aonde foi primeiro encontrada a página atual, servindo como uma lista encadeada para definir o caminho entra a inicial e a final
    // A função SetLinks busca na url as links da página e os armazena em uma lista
    // A função pode ser chamada mais de uma vez, mas só executa a busca se a lista for nula
    class Page
    {
        private string url;
        private Page root;
        private List<Page> links;

        public Page(string _url, Page _root)
        {
            this.url = _url;
            this.root = _root;
        }

        public string Url { get => url; }
        public Page Root { get => root; }
        public List<Page> Links { get => links; }

        public void SetLinks()
        {
            if (this.links == null)
            {
                WebClient w = new WebClient();
                string html = w.DownloadString(url);
                links = new List<Page>();

                foreach (Match match in Regex.Matches(html, "href=\"/wiki([^\"]*)\""))
                {
                    if (!match.Groups[1].Value.Contains(".") && !match.Groups[1].Value.Contains("#"))
                        links.Add(new Page("https://en.wikipedia.org/wiki" + match.Groups[1].Value, this));
                }
            }
        }

    }
}
