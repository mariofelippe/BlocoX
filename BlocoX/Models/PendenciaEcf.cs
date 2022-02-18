using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocoX.Models
{
    public class PendenciaEcf
    {
        private Ecf ecf;
        private int situacaoPafEcfCodigo;
        private string situacaoPafEcfDescricao;
        private int quantidadePendencias;
        private int quantidadeAviso;
        private List<Pendencia> pendencias;
        private List<Aviso> avisos;

        public PendenciaEcf(Ecf ecf, int situacaoPafEcfCofigo, string situacaoPafEcfDescricao, int qtdPendencia, int qtdAviso)
        {
            Ecf = ecf;
            SituacaoPafEcfCodigo = situacaoPafEcfCodigo;
            SituacaoPafEcfDescricao = situacaoPafEcfDescricao;
            QuantidadePendencias = qtdPendencia;
            QuantidadeAviso = qtdAviso;
        }

        public Ecf Ecf { get => ecf; set => ecf = value; }
        public int SituacaoPafEcfCodigo { get => situacaoPafEcfCodigo; set => situacaoPafEcfCodigo = value; }
        public string SituacaoPafEcfDescricao { get => situacaoPafEcfDescricao; set => situacaoPafEcfDescricao = value; }
        public int QuantidadePendencias { get => quantidadePendencias; set => quantidadePendencias = value; }
        public int QuantidadeAviso { get => quantidadeAviso; set => quantidadeAviso = value; }
        public List<Pendencia> Pendencias { get => pendencias; set => pendencias = value; }
        public List<Aviso> Avisos { get => avisos; set => avisos = value; }

        public void AdicionarPendencia (Pendencia pendencia)
        {
            if (Pendencias == null)
            {
                Pendencias = new List<Pendencia>();
            }
            Pendencias.Add(pendencia);
        }
        public void AdicionarAviso(Aviso aviso)
        {
            if (Avisos == null)
            {
                Avisos = new List<Aviso>();
            }

            Avisos.Add(aviso);
        }
    }
}
