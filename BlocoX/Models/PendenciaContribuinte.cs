using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class PendenciaContribuinte
    {
        private Estabelecimento estabelecimento;
        private DateTime dataInicioObrigacao;
        private List<PendenciaEcf> pendenciasEcfs;

        public Estabelecimento Estabelecimento { get => estabelecimento; set => estabelecimento = value; }
        public DateTime DataInicioObrigacao { get => dataInicioObrigacao; set => dataInicioObrigacao = value; }
        public List<PendenciaEcf> PendenciasEcfs 
        {
            get
            { 
                if(pendenciasEcfs == null)
                {
                    pendenciasEcfs = new List<PendenciaEcf>();
                }
                return pendenciasEcfs;
            }
            set => pendenciasEcfs = value; }

        public void AdicionarPendenciaEcf(PendenciaEcf pendenciaEcf)
        {
            if (PendenciasEcfs == null)
            {
                PendenciasEcfs = new List<PendenciaEcf>();
            }
            PendenciasEcfs.Add(pendenciaEcf);
        }
    }
}
