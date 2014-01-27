IF OBJECT_ID('dataprep._TMPCABECALHO') IS NOT NULL
	DROP TABLE dataprep._TMPCABECALHO
	
CREATE TABLE dataprep._TMPCABECALHO
(
	ProcessingID uniqueidentifier,
	[ctGuiaID] [nvarchar](20) NOT NULL,
	[ctRotaID] [bigint] NOT NULL,
	[ctMotID] [int] NOT NULL,
	[ctMatricula] [nvarchar](20) NOT NULL,
	[ctClienteID] [bigint] NULL,
	[ctEmpTransp] bigint NULL,
	[ctEmpDest] bigint NULL,
	[ctLocDesc] [smallint] NOT NULL,
	[ctDataIni] [datetime] NULL,
	[ctDataFim] [datetime] NULL,
	[ctOtrID] [smallint] NULL,
	[ctObs] [nvarchar](255) NULL,
	[ctMAID] [smallint] NULL,
	[ctMAObs] [nvarchar](255) NULL,
	[ctAssCli] [image] NULL,
	[ctCliNome] [nvarchar](40) NULL,
	[ctDocImp] [tinyint] NULL,
	[ctDecExp] [nvarchar](100) NULL,
	[ctDecDest] [nvarchar](100) NULL,
	[ctDecTransp] [nvarchar](100) NULL,
	[UltAlteracao] [datetime] NOT NULL,
	[ctTransporte] [bit] NOT NULL,
	[ctPesoGIII] numeric(10,4) NOT NULL,
	[ctPesoGIV] numeric(10,4) NOT NULL,
	[ctNumContrato] [varchar](50) NULL,
	[ctFacturada] [tinyint] NULL,
	[ctDataReg] [datetime] NULL,
	[ctPeriodoFact] [int] NULL,
	[ctGuiaEliminada] [bit] NULL,
	[Facturada] [bit] NULL
)

IF OBJECT_ID('dataprep._TMPDetalhe') IS NOT NULL
	DROP TABLE dataprep._TMPDetalhe
	
CREATE TABLE dataprep._TMPDetalhe(
	ProcessingID uniqueidentifier,
	[dtGuiaID] [nvarchar](20) NOT NULL,
	[dtPrdID] [bigint] NOT NULL,
	[dtRecipienteID] [smallint] NOT NULL,
	[dtVolumeID] [smallint] NOT NULL,
	[dtQtdEnt] [smallint] NULL,
	[dtQtdRec] [smallint] NULL,
	[dtQtdDevS] [smallint] NULL,
	[dtQtdDevD] [smallint] NULL,
	[dtPesoEnt] numeric(10,4) NULL,
	[dtPesoRec] numeric(10,4) NULL,
	[dtPesoDevS] numeric(10,4) NULL,
	[dtPesoDevD] numeric(10,4) NULL,
	[dtMDID] [smallint] NULL,
	[dtMDObs] [nvarchar](255) NULL,
	[UltAlteracao] [datetime] NOT NULL,
	[dtFacturada] [tinyint] NULL,
	[dtDataReg] [datetime] NULL,
	[dtPeriodoFact] [int] NULL,
	[dtRefEncomenda] [varchar](500) NULL,
	[ctGuiaEliminada] [bit] NULL,
	[DTCONTRACTID] VARCHAR(20) NULL
)

IF OBJECT_ID('dataprep._ACONDICIONAMENTOS') IS NOT NULL
	DROP TABLE dataprep._ACONDICIONAMENTOS

PRINT 'CREATE TABLE dataprep._ACONDICIONAMENTOS'	
CREATE TABLE dataprep._ACONDICIONAMENTOS
(
	ProcessingID uniqueidentifier,
    Produto int,
	ItemContrato varchar(50),
	CLIENTE varchar(50),
	PERIODOFACTID INT
)


PRINT 'CREATE TABLE dataprep._CLIENTES'	
IF OBJECT_ID('dataprep._CLIENTES') IS NOT NULL
	DROP TABLE dataprep._CLIENTES
	
CREATE TABLE dataprep._Clientes
(
	ProcessingID uniqueidentifier,
	ClienteID varchar(50),
	PeriodoFacturacao INT,
	CTipoCliente VARCHAR(50),
	COrigem VARCHAR(50),
	UsufruiDescontoFerias bit
)


IF OBJECT_ID('dataprep._tblAuxFacturacao') IS NOT NULL
	DROP TABLE dataprep._tblAuxFacturacao
	
PRINT 'CREATE TABLE dataprep._TBLAUXFACTURACAO'	

 CREATE TABLE dataprep._tblAuxFacturacao 
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	PERIODOFACTURACAO INT,
	PRODUTOID varchar(50),
	PRDNOMEPHC VARCHAR(255),
	PRDCODPHC VARCHAR(50),
	PRDCODPHC_EXCESSO VARCHAR(50),
	PRDCODPHC_UTILIZACAO VARCHAR(50),
	PRDCODPHC_SERVICO VARCHAR(50),
	PRDCODPHC_TGR VARCHAR(50),
	TIPOFACTURACAO VARCHAR(50),
	TIPOFACTURACAO_UTILIZACAO VARCHAR(50),
	QUANTIDADE decimal(10,4),
	QUANTIDADE_UTILIZACAO decimal(10,4),
	QUANTIDADE_EXCESSO decimal(10,4),
	QUANTIDADE_OFERTA decimal(10,4),
	ITEMCONTRATO VARCHAR(50),
	ITEMCONTRATOAVENCA VARCHAR(50),
	QUANTIDADE_AVENCA decimal(10,4),
	PRECOUNITARIO decimal(10,4),
	PRECOEXCESSO decimal(10,4),
	PRECOUTILIZACAO decimal(10,4),
	IVA decimal(10,4),
	IVAUTILIZACAO decimal(10,4),
	DESCONTOPROTOCOLO decimal(10,4),
	DESCONTOCAMPANHA decimal(10,4),
	DESCONTOCOMERCIAL decimal(10,4),
	VALORMINIMOFACTURACAO decimal(10,4),
	CLIENTEFACTURACAO VARCHAR(50),
	ESTABELECIMENTO INT,
	EMPRESAINTERNA VARCHAR(50),
	CONSUMIVEL BIT,
	REGRA VARCHAR(500),
	ISEXCESSO BIT,
	GUIA varchar(50),
	ISUTILIZACAO BIT,
	ISEXCESSOTRANSPORTE BIT,
	DESTINATARIO VARCHAR(50)
)

IF OBJECT_ID('dataprep._tblDummyPeriodicidadeRecolha') IS NOT NULL
	DROP TABLE dataprep._tblDummyPeriodicidadeRecolha
create table dataprep._tblDummyPeriodicidadeRecolha 
(
	Periodicidade varchar(50),
	NumRecolhas int
)

--[DEF 2]
IF OBJECT_ID('dataprep._tblAuxTransaccoes') IS NOT NULL
	DROP TABLE dataprep._tblAuxTransaccoes
	
CREATE TABLE dataprep._tblAuxTransaccoes
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	NUMGUIA VARCHAR(50),
	TRANSACCAOID VARCHAR(50),
	PERIODOFACTURACAO INT,
	PRODUTOID INT,
	QUANTIDADERECOLHIDA INT,
	QUANTIDADEENTREGUE INT,
	PESORECOLHIDO numeric(10,4),
	PESOENTREGUE numeric(10,4),
	PESOGIII numeric(10,4),
	PESOGIV numeric(10,4),
	LITRO numeric(10,4),
	RECIPIENTE VARCHAR(50),
	VOLUME VARCHAR(10),
	DATAGUIA DATETIME,
	MOTORISTA INT,
	MATRICULA VARCHAR(50),
	NaoCobrarTransporte bit,
	DESTINATARIO VARCHAR(50)
)

IF OBJECT_ID('dataprep._tblAuxTransaccoesEspecial') IS NOT NULL
	DROP TABLE dataprep._tblAuxTransaccoesEspecial
	
CREATE TABLE dataprep._tblAuxTransaccoesEspecial
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	NUMGUIA VARCHAR(50),
	TRANSACCAOID VARCHAR(50),
	PERIODOFACTURACAO INT,
	PRODUTOID INT,
	QUANTIDADERECOLHIDA INT,
	QUANTIDADEENTREGUE INT,
	PESORECOLHIDO numeric(10,4),
	PESOENTREGUE numeric(10,4),
	PESOGIII numeric(10,4),
	PESOGIV numeric(10,4),
	LITRO numeric(10,4),
	RECIPIENTE VARCHAR(50),
	VOLUME VARCHAR(10),
	DATAGUIA DATETIME,
	MOTORISTA INT,
	MATRICULA VARCHAR(50),
	NaoCobrarTransporte bit,
	Periodicidade int,
	SERVICO VARCHAR(50)
)

IF OBJECT_ID('dataprep._tblAuxTransaccoesTransportesNaoMensais') IS NOT NULL
	DROP TABLE dataprep._tblAuxTransaccoesTransportesNaoMensais

CREATE TABLE dataprep._tblAuxTransaccoesTransportesNaoMensais
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	NUMGUIA VARCHAR(50),
	TRANSACCAOID VARCHAR(50),
	PERIODOFACTURACAO INT,
	PRODUTOID INT,
	DATAGUIA DATETIME,
	MOTORISTA INT,
	MATRICULA VARCHAR(50),
	NaoCobrarTransporte bit
)


-- [DEF 3]
IF OBJECT_ID('dataprep._tblAuxItemContrato') IS NOT NULL
	DROP TABLE dataprep._tblAuxItemContrato
	
CREATE TABLE dataprep._tblAuxItemContrato 
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	NUMCONTRATO VARCHAR(50),
	ITEMCONTRATO VARCHAR(50),
	PERIODOFACTURACAO INT,
	PRODUTOID INT,
	PRODUTONOME VARCHAR(255),
	PRODUTOFAMILIA VARCHAR(50),
	REFPHCPRODUTO VARCHAR(50),
	REFPHCSERVICO VARCHAR(255),
	REFPHCEXCESSO VARCHAR(50),
	REFPHCUTILIZACAO VARCHAR(255),
	DATAINICIO DATETIME,
	DATAFIM DATETIME,
	TIPOFACTURACAO VARCHAR(50),
	TIPOFACTURACAOUTILIZACAO VARCHAR(50),
	CLIENTEFACTURACAOID VARCHAR(50),
	EMPRESAFACTURACAOID VARCHAR(50),
	ACONDICIONAMENTOID	VARCHAR(1000),
	AVENCA VARCHAR(50),
	TRANSPORTE VARCHAR(50),
	IVA numeric(10,4),
	IVAUTILIZACAO numeric(10,4),
	CONSUMIVEL BIT,
	DATAINICIOOFERTA DATETIME,
	DATAFIMOFERTA DATETIME,
	DESCONTOPROTOCOLO numeric(10,4),
	DESCONTOCAMPANHA numeric(10,4),
	DESCONTOCOMERCIAL numeric(10,4),
	PRECOUNITARIO numeric(10,4),
	PRECOEXCESSO numeric(10,4),
	PRECOUTILIZACAO numeric(10,4),
	QUANTIDADE INT,
	QUANTIDADEOFERTA INT,
	TIPOITEM VARCHAR(50),
	PERIODICIDADESERVICO VARCHAR(50),
	TAXAGESTAORESIDUOS numeric(10,4),
	TAXAGESTAORESIDUOSCLIENTE numeric(10,4),
	ESTABELECIMENTO INT,
	ESTADO varchar(50),
	ISESCALAO BIT,
	SERVICO VARCHAR(100),
	CONTABILIZAR BIT,
	CREMACAOINDIVIDUAL BIT,
	PERIODICIDADEEXCESSO VARCHAR(50),
	FACTURARMENSAL BIT,
	APLICARTAXAGESTAORESIDUOS BIT,
	RACIO int,
	APLICARDESCONTOTGR BIT,
	APLICARDESCONTOPRECOS BIT
)

-- [DEF 4]
IF OBJECT_ID('dataprep._tblAuxAcondicionamentosTransaccoes') IS NOT NULL
	DROP TABLE dataprep._tblAuxAcondicionamentosTransaccoes
	
CREATE TABLE  dataprep._tblAuxAcondicionamentosTransaccoes
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	TRANSACCAOID VARCHAR(50),
	PRODUTOACONDICIONAMENTOID VARCHAR(50),
	PRODUTOID INT,
	NUMGUIA varchar(50)
)

IF OBJECT_ID('dataprep._tblAuxAcondicionamentosTransaccoesEspecial') IS NOT NULL
	DROP TABLE dataprep._tblAuxAcondicionamentosTransaccoesEspecial
	
CREATE TABLE  dataprep._tblAuxAcondicionamentosTransaccoesEspecial
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	TRANSACCAOID VARCHAR(50),
	PRODUTOACONDICIONAMENTOID VARCHAR(50),
	PRODUTOID INT,
	numguia varchar(50)
)

--[DEF 5]
IF OBJECT_ID('dataprep._tblAuxClientesComFerias') IS NOT NULL
	DROP TABLE dataprep._tblAuxClientesComFerias
	
CREATE TABLE dataprep._tblAuxClientesComFerias
(
	ProcessingID uniqueidentifier,
	CLIENTEID VARCHAR(50),
	PERIODOFACTURACAOID INT,
	QTDTRANSACCOES INT,
	RECOLHASCONTRATADAS INT
)

--[DEF 6]
IF OBJECT_ID('dataprep._Escaloes') IS NOT NULL
	DROP TABLE dataprep._Escaloes
	
CREATE TABLE dataprep._Escaloes
(
	ProcessingID uniqueidentifier,
	ClienteID varchar(50),
	PeriodoFacturacao INT,
	ProdutoID varchar(50),
	PrecoUnitario decimal(10,4),
	PrecoExcesso decimal(10,4),
	PrecoUtilizacao decimal(10,4),
	QuantidadeInicial decimal(10,4),
	QuantidadeFinal decimal(10,4),
	ItemContrato varchar(50)
)	

IF OBJECT_ID('dataprep._DESTINATARIOSFINAL') IS NOT NULL
	DROP TABLE dataprep._DESTINATARIOSFINAL
	
CREATE TABLE dataprep._DESTINATARIOSFINAL
(
	ProcessingID uniqueidentifier,
	CLIENTEID varchar(50),
	PERIODOFACTURACAO int,
	PRECOUNITARIO decimal(10,4),
	PRODUTOID varchar(50),
	PESORECOLHIDO decimal(10,4),
	DESTINATARIOID varchar(50),
	FAMILIA varchar(50),
	APLICADESCONTO bit,
	DESCONTOPROTOCOLO decimal(10,4),
	DESCONTOCOMERCIAL decimal(10,4),
	DESCONTOCAMPANHA decimal(10,4),
	EMPRESAFACTURACAOID varchar(50),
	CLIENTEFACTURACAOID varchar(50),
	ESTABELECIMENTO varchar(50)
)
