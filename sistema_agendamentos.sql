CREATE DATABASE IF NOT EXISTS sistema_agendamentos;

USE sistema_agendamentos;

CREATE TABLE IF NOT EXISTS agendamentos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(11) NOT NULL,
    nome_servico VARCHAR(100) NOT NULL,
    data_agendamento DATETIME NOT NULL,
    UNIQUE (cpf)
);
