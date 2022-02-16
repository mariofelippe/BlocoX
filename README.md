# Bloco X
O Bloco X Consiste no envio di�rio de informa��es de venda por estabelecimentos que utilizam equipamentos ECF para Secretaria Estadual de Fazenda.<br>
Os estatabelecimentos ficam obrigados a transmitir os arquivos de reduca��o Z assinados digitalmente.

## Objetivos:
O objetivo principal do desenvolvimento dessa aplica��o � aprimorar de forma geral o conhecimento nos pontos abaixo:.<br>

- Linguagem C#.
- Orienta��o a Objetos.
- Manipula��o de arquivos XMLs -> (Leitura e escrita).
- Teste unit�rios.
 
## Funcionalidades:
Para essa aplica��o, algumas funcionalidades ser�o desenvolvidadas para apoiar no envio das pend�ncias para Sefaz.<br>

- [X] Processar XMLs Redu��es Z e atribuir os valores as classes.
- [X] Gerar XMLs Redu��es Z.
- [X] Corrigir Problemas de valores nas Redu��es Z.
- [X] Assinar XMLs.
- [X] Consultar Status de Recibos.
- [X] Enviar XMLs.
- [X] Cancelar XMLs.
- [X] Compactar XML.
- [X] Gerar Relat�rio de Arquivos Processados.
- [X] Baixar XMLs Reduca��o Z.

### Problemas corrigidos pela ferramenta:

- Corre��o da venda bruta di�ria.
- Ajuste de credenciamento.
- Corre��o dos valores dos totalizadores parcial.


### Utiliza��o da Ferramenta

A ferramenta possui as op�es abaixo para a utiliza��o de suas funcionalidades:

~~~
*****************************************************************
                       Apoio Bloco X
*****************************************************************

Op��es:
1 - Enviar XMLs Redu��o Z.
2 - Consultar Recibos.
3 - Cancelar XMLs Redu��o Z.
4 - Baixar XMLs Redu��o Z.
99 - Sair.

Op��o:
~~~

#### Par�metros de configura��o

O App.config possui alguns valores para parametriza��o e utiliza��o da ferramenta.

Par�metro| Valor | Descri��o
:--------- | :------ | :-------
ArquivoCertificado |  | Caminho do arquivo .pfx.
SenhaCertificado|  | Senha do certificado digital.
CredenciamentoPaf|  | Numero do credenciamento PAF.
ListaCancelamento| ListaCancelamento.txt | Arquivo com a lista de recibos a serem cancelados.
MotivoCancelamento| Cancelamento para ajuste da Venda Bruta diaria. | Movito enviado para cancelamento de XMLs.
ListaXML|ListaXML.txt  | Lista com os arquivos que ser�o processados e enviados para Sefaz.
TempoEsperaEnvio| 0 | Tempo de espera entre os envios. 
ListaConsulta| ListaConsulta.txt | Lista com os recibos para consultar o status do processamento.
TempoEsperaConsulta| 0 | Tempo de espera entre as consultas.
ListaDownloadArquivo| ListaDownloadArquivo.txt | Lista de recibos para download dos XMLs.
TempoEsperaDownload| 0 | Tempo de espera entre os downloads. 
PathXML| XMLs | Diret�rio com os XMLs a serem processados e enviados. 
PathLogs| Logs | Diret�rio onde s�o armazenados os relat�rios e Logs da ferramenta. 
AjustaCredencimanetoPaf| false | Par�metro para ajuste do credenciamento PAF. Se true, ajustar� o credencimaneto caso seja diferente do parametrizado. 
AjustaVendaBrutaDiaria| false | Par�metro para ajuste da venda bruta. Se true, a ferramenta calcular� o valor e ajustar� caso esteja diferente do arquivo processado.
AjustaValorTotalizador|  false | Par�metro para ajuste do valor dos totalizadores. Se true, a ferramente calcular� e ajustar� o valor de cada totalizador caso esteja diferente.
