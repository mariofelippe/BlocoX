# Bloco X
O Bloco X Consiste no envio diário de informações de venda por estabelecimentos que utilizam equipamentos ECF para Secretaria Estadual de Fazenda.<br>
Os estatabelecimentos ficam obrigados a transmitir os arquivos de reducação Z assinados digitalmente.

## Objetivos:
O objetivo principal do desenvolvimento dessa aplicação é aprimorar de forma geral o conhecimento nos pontos abaixo:.<br>

- Linguagem C#.
- Orientação a Objetos.
- Manipulação de arquivos XMLs -> (Leitura e escrita).
- Teste unitários.
 
## Funcionalidades:
Para essa aplicação, algumas funcionalidades serão desenvolvidadas para apoiar no envio das pendências para Sefaz.<br>

- [X] Processar XMLs Reduções Z e atribuir os valores as classes.
- [X] Gerar XMLs Reduções Z.
- [X] Corrigir Problemas de valores nas Reduções Z.
- [X] Assinar XMLs.
- [X] Consultar Status de Recibos.
- [X] Enviar XMLs.
- [X] Cancelar XMLs.
- [X] Compactar XML.
- [X] Gerar Relatório de Arquivos Processados.
- [X] Baixar XMLs Reducação Z.

### Problemas corrigidos pela ferramenta:

- Correção da venda bruta diária.
- Ajuste de credenciamento.
- Correção dos valores dos totalizadores parcial.


### Utilização da Ferramenta

A ferramenta possui as opões abaixo para a utilização de suas funcionalidades:

~~~
*****************************************************************
                       Apoio Bloco X
*****************************************************************

Opções:
1 - Enviar XMLs Redução Z.
2 - Consultar Recibos.
3 - Cancelar XMLs Redução Z.
4 - Baixar XMLs Redução Z.
99 - Sair.

Opção:
~~~

#### Parâmetros de configuração

O App.config possui alguns valores para parametrização e utilização da ferramenta.

Parâmetro| Valor | Descrição
:--------- | :------ | :-------
ArquivoCertificado |  | Caminho do arquivo .pfx.
SenhaCertificado|  | Senha do certificado digital.
CredenciamentoPaf|  | Numero do credenciamento PAF.
ListaCancelamento| ListaCancelamento.txt | Arquivo com a lista de recibos a serem cancelados.
MotivoCancelamento| Cancelamento para ajuste da Venda Bruta diaria. | Movito enviado para cancelamento de XMLs.
ListaXML|ListaXML.txt  | Lista com os arquivos que serão processados e enviados para Sefaz.
TempoEsperaEnvio| 0 | Tempo de espera entre os envios. 
ListaConsulta| ListaConsulta.txt | Lista com os recibos para consultar o status do processamento.
TempoEsperaConsulta| 0 | Tempo de espera entre as consultas.
ListaDownloadArquivo| ListaDownloadArquivo.txt | Lista de recibos para download dos XMLs.
TempoEsperaDownload| 0 | Tempo de espera entre os downloads. 
PathXML| XMLs | Diretório com os XMLs a serem processados e enviados. 
PathLogs| Logs | Diretório onde são armazenados os relatórios e Logs da ferramenta. 
AjustaCredencimanetoPaf| false | Parâmetro para ajuste do credenciamento PAF. Se true, ajustará o credencimaneto caso seja diferente do parametrizado. 
AjustaVendaBrutaDiaria| false | Parâmetro para ajuste da venda bruta. Se true, a ferramenta calculará o valor e ajustará caso esteja diferente do arquivo processado.
AjustaValorTotalizador|  false | Parâmetro para ajuste do valor dos totalizadores. Se true, a ferramente calculará e ajustará o valor de cada totalizador caso esteja diferente.
