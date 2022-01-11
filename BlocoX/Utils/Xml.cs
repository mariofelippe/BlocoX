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
            string xml = $@"<?xml version='1.0' encoding='UTF‐8'?>
<ReducaoZ Versao='1.0'>
    <Mensagem>
        <Estabelecimento>
            <Ie>{estabelecimento.Ie}</Ie>
        </Estabelecimento>
        <PafEcf>
            <NumeroCredenciamento>{paf.NumeroCredenciamento}</NumeroCredenciamento>
        </PafEcf>
        <Ecf>
            <NumeroFabricacao>{reducaoZ.ECF.NumeroFabricacao}</NumeroFabricacao>
            <DadosReducaoZ>
                <DataReferencia>{reducaoZ.DadosReducao.DataReferencia.ToString("yyyy-MM-dd")}</DataReferencia>
                <DataHoraEmissao>{reducaoZ.DadosReducao.DataHoraEmissao.ToString("yyyy-MM-ddTHH:mm:ss")}</DataHoraEmissao>
                <CRZ>{reducaoZ.DadosReducao.CRO}</CRZ>
                <COO>{reducaoZ.DadosReducao.COO}</COO>
                <CRO>{reducaoZ.DadosReducao.CRO}</CRO>
                <VendaBrutaDiaria>{reducaoZ.DadosReducao.VendaBrutaDiaria}</VendaBrutaDiaria>
                <GT>{reducaoZ.DadosReducao.GT}</GT>
                <TotalizadoresParciais>
                   {TotalizadorParcialTags(reducaoZ.Totalizadores)}
                </TotalizadoresParciais>
            </DadosReducaoZ>
        </Ecf>
    </Mensagem>
</ReducaoZ>";


            XmlDocument xmlDco = new XmlDocument();
            xmlDco.LoadXml(xml.Replace("\'","\""));
            //Console.WriteLine(xmlDco.InnerXml);
            Console.WriteLine(xml);
            return xml;
        }


        private static string TotalizadorParcialTags(List<TotalizadorParcial> totalizadores)
        {
            string tagsTotalizadorParcial = String.Empty;

            foreach (TotalizadorParcial totalizador in totalizadores)
            {
                tagsTotalizadorParcial += $@"
<TotalizadorParcial>
    <Nome>{totalizador.Nome}</Nome>
    <Valor>{totalizador.Valor.ToString()}</Valor>
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
                tagsProdutosServicos += $@"
        <Produto>
            <Descricao>{produtos[i].Descricao.Replace("&", @"&amp;")}</Descricao>
            <CodigoGTIN>{produtos[i].CodigoGTIN}</CodigoGTIN>
            <CodigoCEST>{produtos[i].CodigoCEST}</CodigoCEST>
            <CodigoNCMSH>{produtos[i].CodigoNCM}</CodigoNCMSH>
            <CodigoProprio>{produtos[i].CodigoProprio}</CodigoProprio>
            <Quantidade>{produtos[i].Quantidade}</Quantidade>
            <Unidade>{produtos[i].Unidade}</Unidade>
            <ValorDesconto>{produtos[i].ValorDesconto}</ValorDesconto>
            <ValorAcrescimo>{produtos[i].ValorAcrescimo}</ValorAcrescimo>
            <ValorCancelamento>{produtos[i].ValorCancelamento}</ValorCancelamento>
            <ValorTotalLiquido>{produtos[i].ValorLiquido}</ValorTotalLiquido>
        </Produto>";
            }

            return tagsProdutosServicos;
        }
    }
}
