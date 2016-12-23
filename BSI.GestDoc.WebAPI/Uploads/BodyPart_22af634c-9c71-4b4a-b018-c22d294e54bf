
CREATE TABLE dbo.TipoInformacaoCliente (
                TipoInfoCliId INT IDENTITY NOT NULL,
                TipoInfoCliDescricao VARCHAR(30) NOT NULL,
                CONSTRAINT TipoInfoCliId PRIMARY KEY (TipoInfoCliId)
)

-- Comment for table [TipoInformacaoCliente]: Tabela domínio para descrever quais as informações chave que o cliente irá linkas os documentos, exemplo: "Número proposta", "CPF", "RG", "CNPJ", etc...


CREATE TABLE dbo.Cliente (
                ClienteId INT IDENTITY NOT NULL,
                ClienteNome VARCHAR(60) NOT NULL,
                ClientePastaDocumentos VARCHAR(8) NOT NULL,
                ClienteImagemLogoDesktop VARCHAR(30) NOT NULL,
                ClienteImagemLogoMobile VARCHAR(30) NOT NULL,
                ClienteCorPadrao VARCHAR(15) NOT NULL,
                CONSTRAINT ClienteId PRIMARY KEY (ClienteId)
)

-- Comment for table [Cliente]: Contém informações dos clientes (Empresas)

-- Comment for column [ClienteId]: Código do Cliente

-- Comment for column [ClienteNome]: Bradesco Financiamentos, Itaú, etc...

-- Comment for column [ClientePastaDocumentos]: irá guardar a subpasta onde serão salvos os documentos deste cliente no servidor, por exemplo: "bradesco", "prefsp", "itau"
-- No código será concatenado com o nome do servidor de armazenamento de arquivos e ficará da seguinte forma:
-- \\meu_server\docsbsi\bradesco
-- \\meu_server\docsbsi\prefsp
-- \\meu_server\docsbsi\itau

-- Comment for column [ClienteImagemLogoDesktop]: irá guardar o nome do arquivo de logo que será carregado no cabeçalho do sistema, o deial é travar o tamanho do arquivo de logo do cliente parceiro


CREATE TABLE dbo.DocumentoClienteDados (
                DocCliDadosId BIGINT IDENTITY NOT NULL,
                DocCliDadosValor VARCHAR(100) NOT NULL,
                ClienteId INT NOT NULL,
                TipoInfoCliId INT NOT NULL,
                CONSTRAINT DocCliDadosId PRIMARY KEY (DocCliDadosId)
)

-- Comment for table [DocumentoClienteDados]: Tabela onde serão armazenados os dados que farão a amarração dos documentos, por exemplo, se for um CPF o dado guardado nesta tabela será 056698229-35

-- Comment for column [ClienteId]: Código do Cliente


CREATE TABLE dbo.ClienteTipoInformacaoCliente (
                CliTipoInfoCliId INT IDENTITY NOT NULL,
                ClienteId INT NOT NULL,
                TipoInfoCliId INT NOT NULL,
                CONSTRAINT CliTipoInfoCliId PRIMARY KEY (CliTipoInfoCliId)
)

-- Comment for table [ClienteTipoInformacaoCliente]: Tabela de relacionamento N-N

-- Comment for column [ClienteId]: Código do Cliente


CREATE TABLE dbo.UsuarioPerfil (
                UsuPerfilId INT IDENTITY NOT NULL,
                ClienteId INT NOT NULL,
                UsuPerfilNome VARCHAR(50) NOT NULL,
                UsuPerfilDescricao VARCHAR(200) NOT NULL,
                CONSTRAINT UsuPerfilId PRIMARY KEY (UsuPerfilId)
)

-- Comment for table [UsuarioPerfil]: Mantém informações do perfil do usuário

-- Comment for column [UsuPerfilNome]: Nome do Perfil do usuário

-- Comment for column [UsuPerfilDescricao]: Descrição do Perfil do usuário


CREATE TABLE dbo.Usuario (
                UsuarioId BIGINT IDENTITY NOT NULL,
                UsuarioLogin VARCHAR(10) NOT NULL,
                UsuarioNome VARCHAR(200) NOT NULL,
                UsuarioEmail VARCHAR(100) NOT NULL,
                UsuarioSenha VARCHAR(50) NOT NULL,
                UsuarioAtivo BIT NOT NULL,
                UsuPerfilId INT NOT NULL,
                ClienteId INT NOT NULL,
                CONSTRAINT UsuarioId PRIMARY KEY (UsuarioId)
)

-- Comment for table [Usuario]: Contém infomrações de usuários

-- Comment for column [UsuarioId]: Código do usuário

-- Comment for column [UsuarioEmail]: E-mail do usuário logado

-- Comment for column [UsuarioSenha]: md5 hash

-- Comment for column [UsuarioAtivo]: Informa se o usuário esta ativo, no caso de inativo o mesmo não pode acessar o site.


CREATE TABLE dbo.DocumentoClienteTipo (
                DocCliTipoId INT IDENTITY NOT NULL,
                ClienteId INT NOT NULL,
                DocCliTipoNome VARCHAR(50) NOT NULL,
                DocCliTipoDescricao VARCHAR(200) NOT NULL,
                DocCliTipoOrdemApresent SMALLINT NOT NULL,
                CONSTRAINT DocCliTipoId PRIMARY KEY (DocCliTipoId)
)

-- Comment for table [DocumentoClienteTipo]: Mantém informações do tipo de documento do cliente
-- Exempmlo: DOC 1, DOC 2 ...

-- Comment for column [DocCliTipoNome]: Para informar de qual documento se trata, exemplo: "Termo de aceite"; "Termo de adesão"

-- Comment for column [DocCliTipoDescricao]: Descrição

-- Comment for column [DocCliTipoOrdemApresent]: Campo para determinar a ordem de apresentacao dos tipos de documentos em grid
-- Em primeiro momento deixar todos com o valor 1, esta funcionalidade não será implementada agora.


CREATE TABLE dbo.DocumentoClienteSituacao (
                DocCliSituId INT IDENTITY NOT NULL,
                DocCliSituDescricao VARCHAR(200) NOT NULL,
                DocCliTipoId INT NOT NULL,
                DocCliSituOrdemApresent SMALLINT NOT NULL,
                CONSTRAINT DocCliSituacaoId PRIMARY KEY (DocCliSituId)
)

-- Comment for table [DocumentoClienteSituacao]: Para informar o tipo/situação do documento, exemplo: "Template" ou "Não assinado"; "Parc. assinado"; "Assinado";

-- Comment for column [DocCliSituDescricao]: Descrição

-- Comment for column [DocCliSituOrdemApresent]: Campo para determinar a ordem de apresentacao da situacao dos tipos de documentos em grid.
-- Em primeiro momento deixar todos com o valor 1, esta funcionalidade não será implementada agora.


CREATE TABLE dbo.DocumentoCliente (
                DocClienteId BIGINT IDENTITY NOT NULL,
                UsuarioId BIGINT NOT NULL,
                DocClienteNomeArquivoSalvo VARCHAR(300) NOT NULL,
                DocClienteDataUpload DATETIME NOT NULL,
                DocCliSituId INT NOT NULL,
                ClienteId INT NOT NULL,
                DocCliTipoId INT NOT NULL,
                DocClienteNomeArquivoOriginal VARCHAR(300) NOT NULL,
                DocClienteTipoArquivo VARCHAR(50) NOT NULL,
                CONSTRAINT DocClienteId PRIMARY KEY (DocClienteId)
)

CREATE TABLE dbo.DocumentoClienteDadosDoc (
                DocCliDadosDocId BIGINT IDENTITY NOT NULL,
                DocCliDadosId BIGINT NOT NULL,
                DocClienteId BIGINT NOT NULL,
                CONSTRAINT DocCliDadosDocId PRIMARY KEY (DocCliDadosDocId)
)

-- Comment for table [DocumentoClienteDadosDoc]: Tabela de relacionamento N-N


ALTER TABLE dbo.ClienteTipoInformacaoCliente ADD CONSTRAINT TipoInformacaoCliente_ClienteTipoInformacaoCliente_fk
FOREIGN KEY (TipoInfoCliId)
REFERENCES dbo.TipoInformacaoCliente (TipoInfoCliId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoClienteDados ADD CONSTRAINT TipoInformacaoCliente_DocumentoClienteDados_fk
FOREIGN KEY (TipoInfoCliId)
REFERENCES dbo.TipoInformacaoCliente (TipoInfoCliId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoClienteTipo ADD CONSTRAINT Cliente_DocumentoClienteTipo_fk
FOREIGN KEY (ClienteId)
REFERENCES dbo.Cliente (ClienteId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.UsuarioPerfil ADD CONSTRAINT Cliente_UsuarioPerfilL_fk
FOREIGN KEY (ClienteId)
REFERENCES dbo.Cliente (ClienteId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.Usuario ADD CONSTRAINT Cliente_Usuario_fk
FOREIGN KEY (ClienteId)
REFERENCES dbo.Cliente (ClienteId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoCliente ADD CONSTRAINT Cliente_DocumentoCliente_fk
FOREIGN KEY (ClienteId)
REFERENCES dbo.Cliente (ClienteId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.ClienteTipoInformacaoCliente ADD CONSTRAINT Cliente_ClienteTipoInformacaoCliente_fk
FOREIGN KEY (ClienteId)
REFERENCES dbo.Cliente (ClienteId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoClienteDados ADD CONSTRAINT Cliente_DocumentoClienteDados_fk
FOREIGN KEY (ClienteId)
REFERENCES dbo.Cliente (ClienteId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoClienteDadosDoc ADD CONSTRAINT DocumentoClienteDados_DocumentoClienteDadosDoc_fk
FOREIGN KEY (DocCliDadosId)
REFERENCES dbo.DocumentoClienteDados (DocCliDadosId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.Usuario ADD CONSTRAINT UsuarioPerfil_Usuario_fk
FOREIGN KEY (UsuPerfilId)
REFERENCES dbo.UsuarioPerfil (UsuPerfilId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoCliente ADD CONSTRAINT Usuario_DocumentoCliente_fk
FOREIGN KEY (UsuarioId)
REFERENCES dbo.Usuario (UsuarioId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoClienteSituacao ADD CONSTRAINT DocumentoClienteTipo_DocumentoClienteSituacao_fk
FOREIGN KEY (DocCliTipoId)
REFERENCES dbo.DocumentoClienteTipo (DocCliTipoId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoCliente ADD CONSTRAINT DocumentoClienteTipo_DocumentoCliente_fk
FOREIGN KEY (DocCliTipoId)
REFERENCES dbo.DocumentoClienteTipo (DocCliTipoId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoCliente ADD CONSTRAINT DocumentoClienteSituacao_DocumentoBradescoFinanceira_fk
FOREIGN KEY (DocCliSituId)
REFERENCES dbo.DocumentoClienteSituacao (DocCliSituId)
ON DELETE NO ACTION
ON UPDATE NO ACTION

ALTER TABLE dbo.DocumentoClienteDadosDoc ADD CONSTRAINT DocumentoCliente_DocumentoClienteDadosDoc_fk
FOREIGN KEY (DocClienteId)
REFERENCES dbo.DocumentoCliente (DocClienteId)
ON DELETE NO ACTION
ON UPDATE NO ACTION