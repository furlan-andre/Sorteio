CREATE DATABASE Sorteio;
GO

USE Sorteio;
GO

CREATE TABLE Pessoa (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,    
    Cpf CHAR(11) NOT NULL,
    DataNascimento DATE NOT NULL
);
GO

INSERT INTO Pessoa (Nome, Cpf, DataNascimento) VALUES
('André Furlan', '01875801108', '1991-01-05')
GO
