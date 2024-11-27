using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRpgEtec.Models
{
    class Disputa
    {
        public int id {  get; set; }
        public DateTime? DataDisputa { get; set; }
        public int AtacanteId { get; set; }
        public int OponenteId { get; set; }
        public string Narracao  { get; set; }
        public int HabilidadeId { get; set; }
        public List<int> ListaIdPersonagens { get; set; }
        public List<string> Resultados  { get; set; }
    }
}
