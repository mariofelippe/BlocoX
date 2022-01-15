using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlocoX.Models;
using System.Xml;


namespace BlocoX.Utils
{
    public class Xml
    {
        public static string XmlReducaoZ(Estabelecimento estabelecimento, PafEcf paf, ReducaoZ reducaoZ)
        {
            string xml = $@"<?xml version='1.0' encoding='UTF-8'?>
<ReducaoZ Versao='1.0'>
    <Mensagem>
        <Estabelecimento>
            <Ie>{estabelecimento.Ie}</Ie>
        </Estabelecimento>
        <PafEcf>
            <NumeroCredenciamento>{Util.AdiconaZeroEsqueda(15, paf.NumeroCredenciamento)}</NumeroCredenciamento>
        </PafEcf>
        <Ecf>
            <NumeroFabricacao>{reducaoZ.ECF.NumeroFabricacao}</NumeroFabricacao>
            <DadosReducaoZ>
                <DataReferencia>{reducaoZ.DadosReducao.DataReferencia.ToString("yyyy-MM-dd")}</DataReferencia>
                <DataHoraEmissao>{reducaoZ.DadosReducao.DataHoraEmissao.ToString("yyyy-MM-ddTHH:mm:ss")}</DataHoraEmissao>
                <CRZ>{Util.AdiconaZeroEsqueda(4, reducaoZ.DadosReducao.CRZ.ToString())}</CRZ>
                <COO>{Util.AdiconaZeroEsqueda(9, reducaoZ.DadosReducao.COO.ToString())}</COO>
                <CRO>{Util.AdiconaZeroEsqueda(3, reducaoZ.DadosReducao.CRO.ToString())}</CRO>
                <VendaBrutaDiaria>{Util.AdiconaZeroEsqueda(14, Util.RemoverPontoVirgula(reducaoZ.DadosReducao.VendaBrutaDiaria.ToString("N2")))}</VendaBrutaDiaria>
                <GT>{Util.AdiconaZeroEsqueda(18,Util.RemoverPontoVirgula(reducaoZ.DadosReducao.GT.ToString("N2")))}</GT>
                <TotalizadoresParciais>
                   {TotalizadorParcialTags(reducaoZ.Totalizadores)}
                </TotalizadoresParciais>
            </DadosReducaoZ>
        </Ecf>
    </Mensagem>
</ReducaoZ>";

           
            return xml.Replace("\'","\"");
        }


        private static string TotalizadorParcialTags(List<TotalizadorParcial> totalizadores)
        {
            string tagsTotalizadorParcial = String.Empty;

            foreach (TotalizadorParcial totalizador in totalizadores)
            {
                tagsTotalizadorParcial += $@"<TotalizadorParcial>
    <Nome>{totalizador.Nome}</Nome>
    <Valor>{totalizador.Valor.ToString("N2").Replace(".","")}</Valor>
    <ProdutosServicos>
        {ProdutosServicosTags(totalizador.Produtos)}
    </ProdutosServicos>
</TotalizadorParcial>";

            }
            
            return tagsTotalizadorParcial;
        }

        private static string ProdutosServicosTags(List<Produto> produtos)
        {
            string tagsProdutosServicos = String.Empty;

            for (int i =0; i < produtos.Count; i++)
            {
                tagsProdutosServicos += $@"<Produto>
            <Descricao>{produtos[i].Descricao.Replace("&", @"&amp;")}</Descricao>
            <CodigoGTIN>{produtos[i].CodigoGTIN}</CodigoGTIN>
            <CodigoCEST>{produtos[i].CodigoCEST}</CodigoCEST>
            <CodigoNCMSH>{produtos[i].CodigoNCM}</CodigoNCMSH>
            <CodigoProprio>{produtos[i].CodigoProprio}</CodigoProprio>
            <Quantidade>{produtos[i].Quantidade.ToString("N2").Replace(".", "")}</Quantidade>
            <Unidade>{produtos[i].Unidade}</Unidade>
            <ValorDesconto>{produtos[i].ValorDesconto.ToString("N2").Replace(".", "")}</ValorDesconto>
            <ValorAcrescimo>{produtos[i].ValorAcrescimo.ToString("N2").Replace(".", "")}</ValorAcrescimo>
            <ValorCancelamento>{produtos[i].ValorCancelamento.ToString("N2").Replace(".", "")}</ValorCancelamento>
            <ValorTotalLiquido>{produtos[i].ValorLiquido.ToString("N2").Replace(".", "")}</ValorTotalLiquido>
        </Produto>";
            }

            return tagsProdutosServicos;
        }
    }
}
