CREATE DATABASE GLEARNING;

USE GLEARNING;

/*TABELA DE ROUPAS*/
CREATE TABLE TB_ROUPA(
	
	ROUPA_ID BIGINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	ROUPA_NOME VARCHAR(50) NOT NULL,
	ROUPA_PRECO DOUBLE NOT NULL,
	ROUPA_NOMEID VARCHAR(50) NOT NULL

)ENGINE=INNODB;

/*TABELA DE ACESSORIOS*/
CREATE TABLE TB_ACESSORIO(
	
	ACESSORIO_ID BIGINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	ACESSORIO_NOME VARCHAR(50) NOT NULL,
	ACESSORIO_PRECO DOUBLE NOT NULL,
	ACESSORIO_NOMEID VARCHAR(50) NOT NULL

)ENGINE=INNODB;

/*TABELA DE USUARIOS ADMNINS*/
CREATE TABLE TB_USER_ADM (	
	
	USER_ADM_CPF BIGINT NOT NULL PRIMARY KEY,
	USER_ADM_NOME VARCHAR(50) NOT NULL,
	USER_ADM_LOGIN VARCHAR(50) NOT NULL,
	USER_ADM_SENHA VARCHAR(50) NOT NULL,
	USER_ADM_EMAIL VARCHAR(50) NOT NULL

)ENGINE=INNODB;

/*TABELA DE USUARIOS PROFESSORES*/
CREATE TABLE TB_USER_PROFESSOR (

	USER_PROFESSOR_CPF BIGINT NOT NULL PRIMARY KEY,
	USER_PROFESSOR_NOME VARCHAR(50) NOT NULL,
	USER_PROFESSOR_LOGIN VARCHAR(50) NOT NULL,
	USER_PROFESSOR_SENHA VARCHAR(50) NOT NULL,
	USER_PROFESSOR_EMAIL VARCHAR(50) NOT NULL

)ENGINE=INNODB;

/*TABELA DE USUARIOS MONITORES*/
CREATE TABLE TB_USER_MONITOR (

	USER_MONITOR_CPF BIGINT NOT NULL PRIMARY KEY,
	USER_MONITOR_NOME VARCHAR(50) NOT NULL,
	USER_MONITOR_LOGIN VARCHAR(50) NOT NULL,
	USER_MONITOR_SENHA VARCHAR(50) NOT NULL,
	USER_MONITOR_EMAIL VARCHAR(50) NOT NULL

)ENGINE=INNODB;

/*TABELA DE USUARIOS ESTUDANTES*/
CREATE TABLE TB_USER_ESTUDANTE (
		
	USER_ESTUDANTE_CPF BIGINT NOT NULL PRIMARY KEY,
	USER_ESTUDANTE_NOME VARCHAR(50) NOT NULL,
	USER_ESTUDANTE_LOGIN VARCHAR(50) NOT NULL,
	USER_ESTUDANTE_SENHA VARCHAR(50) NOT NULL,
	USER_ESTUDANTE_EMAIL VARCHAR(50) NOT NULL,
	USER_ESTUDANTE_NIVEL BIGINT NOT NULL,
	USER_ESTUDANTE_PTOTAIS BIGINT NOT NULL,
	USER_ESTUDANTE_PSEMESTRE BIGINT NOT NULL,
	USER_ESTUDANTE_PATUAIS BIGINT NOT NULL,
	COD_ROUPA BIGINT NOT NULL,
	COD_ACESSORIO BIGINT NOT NULL,
	CONSTRAINT FOREIGN KEY (COD_ROUPA) REFERENCES TB_ROUPA(ROUPA_ID),
	CONSTRAINT FOREIGN KEY (COD_ACESSORIO) REFERENCES TB_ACESSORIO(ACESSORIO_ID)

)ENGINE=INNODB;

/*TABELA DE SALAS*/
CREATE TABLE TB_SALA (

	SALA_ID BIGINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	SALA_CODIGO VARCHAR(12) NOT NULL,
	SALA_NOME VARCHAR(50) NOT NULL,
	COD_USER_PROFESSOR BIGINT NOT NULL,
	CONSTRAINT FOREIGN KEY (COD_USER_PROFESSOR) REFERENCES TB_USER_PROFESSOR(USER_PROFESSOR_CPF)

)ENGINE=INNODB;

/*TABELA DE RELAÇÃO ESTUDANTES E SALAS*/
CREATE TABLE TB_ESTUDANTE_SALA (

	ESTUDANTE_SALA_ID BIGINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	COD_ESTUDANTE BIGINT NOT NULL,
	COD_SALA BIGINT NOT NULL,	
	CONSTRAINT FOREIGN KEY (COD_ESTUDANTE) REFERENCES TB_USER_ESTUDANTE(USER_ESTUDANTE_CPF),
	CONSTRAINT FOREIGN KEY (COD_SALA) REFERENCES TB_SALA(SALA_ID)

)ENGINE=INNODB;

CREATE TABLE TB_CONTEUDOS(
	CONTEUDO_ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	CONTEUDO_TEXTO VARCHAR(500) NOT NULL,
	CONTEUDO_TIPO VARCHAR(50) NOT NULL,
	CONTEUDO_TAG1 VARCHAR(50),
	CONTEUDO_TAG2 VARCHAR(50),
	CONTEUDO_TAG3 VARCHAR(50),
	CONTEUDO_TAG4 VARCHAR(50),
	CONTEUDO_TAG5 VARCHAR(50)
)ENGINE=INNODB;

CREATE TABLE TB_ATIVIDADES (
	ATIVIDADE_ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	ATIVIDADE_LICAO VARCHAR(50) NOT NULL,
	ATIVIDADE_NUMERO VARCHAR(50) NOT NULL,
	ATIVIDADE_NOME VARCHAR(50) NOT NULL
)ENGINE=INNODB;

CREATE TABLE TB_NIVEL_ATIVIDADE (
	NIVEL_ATIVIDADE_ID INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	COD_ESTUDANTE BIGINT NOT NULL,
	COD_ATIVIDADE INT NOT NULL,
	NIVEL_ATIVIDADE INT NOT NULL,
	CONSTRAINT FOREIGN KEY (COD_ESTUDANTE) REFERENCES TB_USER_ESTUDANTE(USER_ESTUDANTE_CPF),
	CONSTRAINT FOREIGN KEY (COD_ATIVIDADE) REFERENCES TB_ATIVIDADES(ATIVIDADE_ID)
)ENGINE=INNODB;

/*  ----------- INSERTS ------------- */

INSERT INTO TB_ROUPA(ROUPA_NOME, ROUPA_PRECO, ROUPA_NOMEID) VALUES
('Roupa normal Masculina', 	10, 	'male_1'),
('Roupa normal Feminina', 	10, 	'female_1');

INSERT INTO TB_ACESSORIO(ACESSORIO_NOME, ACESSORIO_PRECO, ACESSORIO_NOMEID) VALUES
('Nada', 		0,	 	'acessory_0'),
('Chapeu', 		10, 	'acessory_1'),
('Gravata', 	10, 	'acessory_2'),
('Flor', 		10, 	'acessory_3');

INSERT INTO TB_USER_ADM(USER_ADM_CPF, USER_ADM_NOME, USER_ADM_LOGIN, USER_ADM_SENHA, USER_ADM_EMAIL) VALUES 
(10000000001, 'ADMINISTRADOR', 'admin', 'admin', 'admin@gmail.com');

INSERT INTO TB_USER_PROFESSOR(USER_PROFESSOR_CPF, USER_PROFESSOR_NOME, USER_PROFESSOR_LOGIN, USER_PROFESSOR_SENHA, USER_PROFESSOR_EMAIL) VALUES 
(20000000001, 'Professor 1', 'professor1', 'professor1', 'professor1@gmail.com'),
(20000000002, 'Professor 2', 'professor2', 'professor2', 'professor2@gmail.com');

INSERT INTO TB_USER_MONITOR(USER_MONITOR_CPF, USER_MONITOR_NOME, USER_MONITOR_LOGIN, USER_MONITOR_SENHA, USER_MONITOR_EMAIL) VALUES 
(30000000001, 'Monitor 1', 'monitor1', 'monitor1', 'monitor1@gmail.com'),
(30000000002, 'Monitor 2', 'monitor2', 'monitor2', 'monitor2@gmail.com');

INSERT INTO TB_USER_ESTUDANTE(USER_ESTUDANTE_CPF, USER_ESTUDANTE_NOME, USER_ESTUDANTE_LOGIN, USER_ESTUDANTE_SENHA, USER_ESTUDANTE_EMAIL, USER_ESTUDANTE_NIVEL, USER_ESTUDANTE_PTOTAIS, USER_ESTUDANTE_PSEMESTRE, USER_ESTUDANTE_PATUAIS, COD_ROUPA, COD_ACESSORIO) VALUES 
(40000000001, 'Aluno 1', 'aluno1', 'aluno1', 'aluno1@gmail.com', 1, 10, 5, 5, 1, 1),
(40000000002, 'Aluno 2', 'aluno2', 'aluno2', 'aluno2@gmail.com', 1, 10, 5, 5, 2, 1),
(40000000003, 'Aluno 3', 'aluno3', 'aluno3', 'aluno3@gmail.com', 1, 10, 5, 5, 2, 1),
(40000000004, 'Aluno 4', 'aluno4', 'aluno4', 'aluno4@gmail.com', 1, 10, 5, 5, 2, 1),
(40000000005, 'Aluno 5', 'aluno5', 'aluno5', 'aluno5@gmail.com', 1, 10, 5, 5, 1, 1),
(40000000006, 'Aluno 6', 'aluno6', 'aluno6', 'aluno6@gmail.com', 1, 10, 5, 5, 1, 1);

INSERT INTO TB_SALA(SALA_CODIGO, SALA_NOME, COD_USER_PROFESSOR) VALUES
('1234', 'SALA 1', 20000000001),
('4321', 'SALA 2', 20000000002);

INSERT INTO TB_ESTUDANTE_SALA(COD_ESTUDANTE, COD_SALA) VALUES
(40000000001, 1),
(40000000002, 1),
(40000000003, 1),
(40000000004, 2),
(40000000005, 2),
(40000000006, 2);

INSERT INTO TB_ATIVIDADES(ATIVIDADE_LICAO, ATIVIDADE_NUMERO, ATIVIDADE_NOME) VALUES
('Lição 1', '1', 'Lição 1 Atividade 1'),
('Lição 2', '1', 'Lição 2 Atividade 1');

INSERT INTO TB_NIVEL_ATIVIDADE(COD_ESTUDANTE, COD_ATIVIDADE, NIVEL_ATIVIDADE) VALUES
(40000000001, 1, 5),
(40000000002, 1, 12),
(40000000003, 1, 25),
(40000000004, 1, 10),
(40000000005, 1, 22),
(40000000006, 1, 14),
(40000000001, 2, 5),
(40000000002, 2, 12),
(40000000003, 2, 25),
(40000000004, 2, 10),
(40000000005, 2, 22),
(40000000006, 2, 14);

INSERT INTO TB_CONTEUDOS(CONTEUDO_TEXTO, CONTEUDO_TIPO, CONTEUDO_TAG1, CONTEUDO_TAG2, CONTEUDO_TAG3, CONTEUDO_TAG4, CONTEUDO_TAG5) VALUES
('Liam', 	'Nome', 'Masculino', null, null, null, null),
('Noah', 	'Nome', 'Masculino', null, null, null, null),
('William',	'Nome', 'Masculino', null, null, null, null),
('James', 	'Nome', 'Masculino', null, null, null, null),
('Jacob', 	'Nome', 'Masculino', null, null, null, null),
('Lucas', 	'Nome', 'Masculino', null, null, null, null),
('Michael',	'Nome', 'Masculino', null, null, null, null),
('Samuel', 	'Nome', 'Masculino', null, null, null, null),
('David', 	'Nome', 'Masculino', null, null, null, null),
('Gabriel',	'Nome', 'Masculino', null, null, null, null),
('Emma', 	'Nome', 'Feminino', null, null, null, null),
('Olivia', 	'Nome', 'Feminino', null, null, null, null),
('Isabella',	'Nome', 'Feminino', null, null, null, null),
('Sophia', 	'Nome', 'Feminino', null, null, null, null),
('Camila', 	'Nome', 'Feminino', null, null, null, null),
('Penelope',	'Nome', 'Feminino', null, null, null, null),
('Chloe', 	'Nome', 'Feminino', null, null, null, null),
('Victoria',	'Nome', 'Feminino', null, null, null, null),
('Zoe', 	'Nome', 'Feminino', null, null, null, null),
('Claire', 	'Nome', 'Feminino', null, null, null, null),
('Smith',	'Sobrenome', null, null, null, null, null),
('Miller',	'Sobrenome', null, null, null, null, null),
('Walker',	'Sobrenome', null, null, null, null, null),
('Turner',	'Sobrenome', null, null, null, null, null),
('Wood',	'Sobrenome', null, null, null, null, null),
('Flores',	'Sobrenome', null, null, null, null, null),
('Collins',	'Sobrenome', null, null, null, null, null),
('Scott',	'Sobrenome', null, null, null, null, null),
('Garcia',	'Sobrenome', null, null, null, null, null),
('Lopez',	'Sobrenome', null, null, null, null, null),
('Stewart',	'Sobrenome', null, null, null, null, null),
('Cortez',	'Sobrenome', null, null, null, null, null),
('Silva',	'Sobrenome', null, null, null, null, null),
('West',	'Sobrenome', null, null, null, null, null),
('Sanders',	'Sobrenome', null, null, null, null, null),
('Fisher',	'Sobrenome', null, null, null, null, null),
('Morey',	'Sobrenome', null, null, null, null, null),
('Gomez',	'Sobrenome', null, null, null, null, null),
('Larsen',	'Sobrenome', null, null, null, null, null),
('Frost',	'Sobrenome', null, null, null, null, null),
('Arabic',	'Nacionalidade', null, null, null, null, null),
('Australian',	'Nacionalidade', null, null, null, null, null),
('Chinese',	'Nacionalidade', null, null, null, null, null),
('Filipino',	'Nacionalidade', null, null, null, null, null),
('Georgian',	'Nacionalidade', null, null, null, null, null),
('Uruguayan',	'Nacionalidade', null, null, null, null, null),
('Indonesian',	'Nacionalidade', null, null, null, null, null),
('Persian',	'Nacionalidade', null, null, null, null, null),
('Japanese',	'Nacionalidade', null, null, null, null, null),
('Korean',	'Nacionalidade', null, null, null, null, null),
('Mongolian',	'Nacionalidade', null, null, null, null, null),
('Mexican',	'Nacionalidade', null, null, null, null, null),
('Pakistani',	'Nacionalidade', null, null, null, null, null),
('Venezuelan',	'Nacionalidade', null, null, null, null, null),
('Bulgarian',	'Nacionalidade', null, null, null, null, null),
('Costa',	'Nacionalidade', null, null, null, null, null),
('Rican',	'Nacionalidade', null, null, null, null, null),
('Croatian',	'Nacionalidade', null, null, null, null, null),
('Peruvian',	'Nacionalidade', null, null, null, null, null),
('British',	'Nacionalidade', null, null, null, null, null),
('Dutch',	'Nacionalidade', null, null, null, null, null),
('English',	'Nacionalidade', null, null, null, null, null),
('Estonian',	'Nacionalidade', null, null, null, null, null),
('Ecuadorian',	'Nacionalidade', null, null, null, null, null),
('French',	'Nacionalidade', null, null, null, null, null),
('Frisian',	'Nacionalidade', null, null, null, null, null),
('German',	'Nacionalidade', null, null, null, null, null),
('Greek',	'Nacionalidade', null, null, null, null, null),
('Hungarian',	'Nacionalidade', null, null, null, null, null),
('Argentinian',	'Nacionalidade', null, null, null, null, null),
('Irish',	'Nacionalidade', null, null, null, null, null),
('Italian',	'Nacionalidade', null, null, null, null, null),
('Brazilian',	'Nacionalidade', null, null, null, null, null),
('Chilean',	'Nacionalidade', null, null, null, null, null),
('Macedonian',	'Nacionalidade', null, null, null, null, null),
('Canadian',	'Nacionalidade', null, null, null, null, null),
('Colombian',	'Nacionalidade', null, null, null, null, null),
('Polish',	'Nacionalidade', null, null, null, null, null),
('Portuguese',	'Nacionalidade', null, null, null, null, null),
('Romanian',	'Nacionalidade', null, null, null, null, null),
('Russian',	'Nacionalidade', null, null, null, null, null),
('Cuban',	'Nacionalidade', null, null, null, null, null),
('Scottish',	'Nacionalidade', null, null, null, null, null),
('Iranian',	'Nacionalidade', null, null, null, null, null),
('Swiss',	'Nacionalidade', null, null, null, null, null),
('Spanish',	'Nacionalidade', null, null, null, null, null),
('Swedish',	'Nacionalidade', null, null, null, null, null),
('Ukrainian',	'Nacionalidade', null, null, null, null, null),
('American',	'Nacionalidade', null, null, null, null, null),
('African',	'Nacionalidade', null, null, null, null, null),
('Nome Empresa 1', 	'Nome','Empresa', 'TAG 1', null, null, null),
('Nome Empresa 2', 	'Nome','Empresa', 'TAG 2', null, null, null),
('Nome Empresa 3', 	'Nome','Empresa', 'TAG 3', null, null, null),
('Nome Empresa 4', 	'Nome','Empresa', 'TAG 4', null, null, null),
('Nome Empresa 5', 	'Nome','Empresa', 'TAG 5', null, null, null),
('Nome Empresa 6', 	'Nome','Empresa', 'TAG 6', null, null, null),
('Descricao 1 adapsdfh sadf aspfdhasfh poashdfpashf paspdf hpsad',	'Descricao', 'Empresa', 'TAG 1', null, null, null),
('Descricao 2 adapsdfh sadf aspfdhasfh poashdfpashf paspdf hpsad',	'Descricao', 'Empresa', 'TAG 2', null, null, null),
('Descricao 3 adapsdfh sadf aspfdhasfh poashdfpashf paspdf hpsad',	'Descricao', 'Empresa', 'TAG 3', null, null, null),
('Descricao 4 adapsdfh sadf aspfdhasfh poashdfpashf paspdf hpsad',	'Descricao', 'Empresa', 'TAG 4', null, null, null),
('Descricao 5 adapsdfh sadf aspfdhasfh poashdfpashf paspdf hpsad',	'Descricao', 'Empresa', 'TAG 5', null, null, null),
('Descricao 6 adapsdfh sadf aspfdhasfh poashdfpashf paspdf hpsad',	'Descricao', 'Empresa', 'TAG 6', null, null, null),
('1_1', 'Logo', '1', null, null, null, null),
('1_2', 'Logo', '1', null, null, null, null),
('1_3', 'Logo', '1', null, null, null, null),
('2_1', 'Logo', '2', null, null, null, null),
('2_2', 'Logo', '2', null, null, null, null),
('2_3', 'Logo', '2', null, null, null, null),
('3_1', 'Logo', '3', 'TAG 1', null, null, null),
('3_2', 'Logo', '3', 'TAG 2', null, null, null),
('3_3', 'Logo', '3', 'TAG 3', null, null, null),
('3_4', 'Logo', '3', 'TAG 4', null, null, null),
('3_5', 'Logo', '3', 'TAG 5', null, null, null),
('3_6', 'Logo', '3', 'TAG 6', null, null, null);