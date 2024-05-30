drop database if exists EmprestimoLoginCore;
create database EmprestimoLoginCore;
use EmprestimoLoginCore;

create table Cliente(
Id int auto_increment primary key,
Nome Varchar(50) not null,
Nascimento DateTime not null,
Sexo char(1),
CPF Varchar(11) not null,
Telefone Varchar(14) not null,
Email Varchar(50) not null,
Senha Varchar(8) not null,
ConfirmacaoSenha Varchar(8) not null,
Situacao char(1) not null
);

create table Colaborador(
Id int auto_increment primary key,
Nome Varchar(50) not null,
Email Varchar(50) not null,
Senha Varchar(8) not null,
Tipo Varchar(8) not null
);

create table Livro(
codLivro int primary key auto_increment,
nomeLivro varchar(50) not null,
imagemLivro varchar(255)
);

create table Emprestimo(
codEmp int primary key auto_increment,
Id int,
dataEmp varchar(20)not null,
dataDev varchar(20)not null,
foreign key (Id)  references cliente (Id)
);

create table itensEmp(
codItem int primary key auto_increment,
codLivro int,
codEmp int,
foreign key(codEmp) references Emprestimo(codEmp),
foreign key(codLivro) references Livro(codLivro)
);

