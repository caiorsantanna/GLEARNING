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
	USER_ESTUDANTE_PELE VARCHAR(50) NOT NULL,
	USER_ESTUDANTE_ROUPA VARCHAR(50) NOT NULL,
	USER_ESTUDANTE_CABELO VARCHAR(50) NOT NULL,
	USER_ESTUDANTE_ACESSORIO VARCHAR(50) NOT NULL

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

INSERT INTO TB_USER_ADM(USER_ADM_CPF, USER_ADM_NOME, USER_ADM_LOGIN, USER_ADM_SENHA, USER_ADM_EMAIL) VALUES 
(10000000001, 'ADMINISTRADOR', 'admin', 'admin', 'admin@gmail.com');

INSERT INTO TB_USER_PROFESSOR(USER_PROFESSOR_CPF, USER_PROFESSOR_NOME, USER_PROFESSOR_LOGIN, USER_PROFESSOR_SENHA, USER_PROFESSOR_EMAIL) VALUES 
(20000000001, 'Professor 1', 'professor1', 'professor1', 'professor1@gmail.com'),
(20000000002, 'Professor 2', 'professor2', 'professor2', 'professor2@gmail.com');

INSERT INTO TB_USER_MONITOR(USER_MONITOR_CPF, USER_MONITOR_NOME, USER_MONITOR_LOGIN, USER_MONITOR_SENHA, USER_MONITOR_EMAIL) VALUES 
(30000000001, 'Monitor 1', 'monitor1', 'monitor1', 'monitor1@gmail.com'),
(30000000002, 'Monitor 2', 'monitor2', 'monitor2', 'monitor2@gmail.com');

INSERT INTO TB_USER_ESTUDANTE(USER_ESTUDANTE_CPF, USER_ESTUDANTE_NOME, USER_ESTUDANTE_LOGIN, USER_ESTUDANTE_SENHA, USER_ESTUDANTE_EMAIL, USER_ESTUDANTE_NIVEL, USER_ESTUDANTE_PTOTAIS, USER_ESTUDANTE_PSEMESTRE, USER_ESTUDANTE_PATUAIS, USER_ESTUDANTE_PELE, USER_ESTUDANTE_ROUPA, USER_ESTUDANTE_CABELO, USER_ESTUDANTE_ACESSORIO) VALUES 
(40000000001, 'Aluno 1', 'aluno1', 'aluno1', 'aluno1@gmail.com', 1, 10, 5, 5, "B_M", "R_M_Escritorio_1", "C_M_1", "A_1"),
(40000000002, 'Aluno 2', 'aluno2', 'aluno2', 'aluno2@gmail.com', 1, 10, 5, 5, "B_F", "R_F_Escritorio_1", "C_F_1", "A_2"),
(40000000003, 'Aluno 3', 'aluno3', 'aluno3', 'aluno3@gmail.com', 1, 10, 5, 5, "B_M", "R_M_Escritorio_2", "C_M_2", "A_3"),
(40000000004, 'Aluno 4', 'aluno4', 'aluno4', 'aluno4@gmail.com', 1, 10, 5, 5, "B_F", "R_F_Escritorio_2", "C_F_2", "A_4"),
(40000000005, 'Aluno 5', 'aluno5', 'aluno5', 'aluno5@gmail.com', 1, 10, 5, 5, "B_M", "R_M_Escritorio_3", "C_M_3", "A_5"),
(40000000006, 'Aluno 6', 'aluno6', 'aluno6', 'aluno6@gmail.com', 1, 10, 5, 5, "B_F", "R_F_Escritorio_3", "C_F_3", "A_6");

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
('Lição 2', '1', 'Lição 2 Atividade 1'),
('Lição 3', '1', 'Lição 3 Atividade 1'),
('Lição 4', '1', 'Lição 4 Atividade 1'),
('Lição 5', '1', 'Lição 5 Atividade 1'),
('Lição 6', '1', 'Lição 6 Atividade 1');

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
(40000000006, 2, 14),
(40000000001, 3, 5),
(40000000002, 3, 12),
(40000000003, 3, 25),
(40000000004, 3, 10),
(40000000005, 3, 22),
(40000000006, 3, 14),
(40000000001, 4, 5),
(40000000002, 4, 12),
(40000000003, 4, 25),
(40000000004, 4, 10),
(40000000005, 4, 22),
(40000000006, 4, 14),
(40000000001, 5, 5),
(40000000002, 5, 12),
(40000000003, 5, 25),
(40000000004, 5, 10),
(40000000005, 5, 22),
(40000000006, 5, 14),
(40000000001, 6, 5),
(40000000002, 6, 12),
(40000000003, 6, 25),
(40000000004, 6, 10),
(40000000005, 6, 22),
(40000000006, 6, 14);

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
('Special Seeds', 	'Nome','Empresa', 'Seeds', null, null, null),
('Jenny\'s Coffee Shop', 	'Nome','Empresa', 'Coffee', null, null, null),
('Field Machines', 	'Nome','Empresa', 'Field Machines', null, null, null),
('Green Pesticides', 	'Nome','Empresa', 'Pesticides', null, null, null),
('Star Vet Products', 	'Nome','Empresa', 'Animal Pharmacy', null, null, null),
('Frozen Fresh', 	'Nome','Empresa', 'Frozen Fruits', null, null, null),
('We import the best grains and seeds for crops. We work in the whole country',	'Descricao', 'Empresa', 'Seeds', null, null, null),
('Here you find coffee, sandwiches, soups and fast meals. Don’t miss our scones',	'Descricao', 'Empresa', 'Coffee', null, null, null),
('This company produces high technology machines, tractors with no drivers and huge tree cutting machines',	'Descricao', 'Empresa', 'Field Machines', null, null, null),
('This company breeds insects in labs to control devastation in the crops',	'Descricao', 'Empresa', 'Pesticides', null, null, null),
('It is a specialized pharmacy shop for animal care. We work with pills, syrups and shots',	'Descricao', 'Empresa', 'Animal Pharmacy', null, null, null),
('Our company works with providing fresh and high quality frozen fruits and vegetables.',	'Descricao', 'Empresa', 'Frozen Fruits', null, null, null),
('1_1', 'Logo', '1', null, null, null, null),
('1_2', 'Logo', '1', null, null, null, null),
('1_3', 'Logo', '1', null, null, null, null),
('2_1', 'Logo', '2', null, null, null, null),
('2_2', 'Logo', '2', null, null, null, null),
('2_3', 'Logo', '2', null, null, null, null),
('3_1', 'Logo', '3', 'Seeds', null, null, null),
('3_2', 'Logo', '3', 'Coffee', null, null, null),
('3_3', 'Logo', '3', 'Field Machines', null, null, null),
('3_4', 'Logo', '3', 'Pesticides', null, null, null),
('3_5', 'Logo', '3', 'Animal Pharmacy', null, null, null),
('3_6', 'Logo', '3', 'Frozen Fruits', null, null, null),
('uma Agua', 'Bebida', 'Licao5_Atv1', 'still water', 'Entrada', null, null),
('um Cha', 'Bebida', 'Licao5_Atv1', 'iced tea', 'Entrada', null, null),
('uma Agua com Gás', 'Bebida', 'Licao5_Atv1', 'sparkling water', 'Entrada', null, null),
('um Cafe', 'Bebida', 'Licao5_Atv1', 'coffee', 'Entrada', null, null),
('um Coquetel de Martini', 'Bebida', 'Licao5_Atv1', 'martini', 'Entrada', null, null),
('uma Agua de Coco', 'Bebida', 'Licao5_Atv1', 'coconut water', 'Entrada', null, null),
('um Suco de Laranja', 'Bebida', 'Licao5_Atv1', 'orange juice', 'Almoco', null, null),
('um Refrigerante', 'Bebida', 'Licao5_Atv1', 'soda', 'Almoco', null, null),
('um Vinho Tinto', 'Bebida', 'Licao5_Atv1', 'red wine', 'Almoco', null, null),
('um Suco de Polpa', 'Bebida', 'Licao5_Atv1', 'fruit juice ', 'Almoco', null, null),
('um Suco de Frutas com Hortelã', 'Bebida', 'Licao5_Atv1', 'fruit juice with mint', 'Almoco', null, null),
('um Refrigerante sem Açucar', 'Bebida', 'Licao5_Atv1', 'diet soda', 'Almoco', null, null),
('uma Salada', 'Comida', 'Licao5_Atv1', 'caesar salad', 'Entrada', null, null),
('uma Porção de Fritas', 'Comida', 'Licao5_Atv1', 'french fries', 'Entrada', null, null),
('um Camarão', 'Comida', 'Licao5_Atv1', 'shrimp', 'Entrada', null, null),
('uma Salada de Galinha', 'Comida', 'Licao5_Atv1', 'chicken salad', 'Entrada', null, null),
('um Shushi', 'Comida', 'Licao5_Atv1', 'sushi roles', 'Entrada', null, null),
('uns Cogumelos', 'Comida', 'Licao5_Atv1', 'mushrooms', 'Entrada', null, null),
('uma Frango Frito', 'Comida', 'Licao5_Atv1', 'fried chichen', 'Almoco', null, null),
('uma Macarrão', 'Comida', 'Licao5_Atv1', 'pasta', 'Almoco', null, null),
('um Risoto', 'Comida', 'Licao5_Atv1', 'risotto', 'Almoco', null, null),
('um Costelas de Porco', 'Comida', 'Licao5_Atv1', 'pork ribs', 'Almoco', null, null),
('um Bife com Batatas', 'Comida', 'Licao5_Atv1', 'steak with potatoes', 'Almoco', null, null),
('um Batata Assada', 'Comida', 'Licao5_Atv1', 'baked potato', 'Almoco', null, null);
