create TABLE tbl_Empresas(
	Id INT IDENTITY Primary Key NOT NULL,
	Nome varchar(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	Telefone varchar(20) NOT NULL,
	Cep varchar(20) NOT NULL,
	Endereco VARCHAR(100) NOT NULL,
	Numero INT,
	Complemento VARCHAR(50),
	Bairro VARCHAR(50) NOT NULL,
	Cidade VARCHAR(50) NOT NULL,
	Estado VARCHAR(20) NOT NULL,
	Ativo BIT NOT NULL
) 

