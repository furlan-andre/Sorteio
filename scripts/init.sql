
USE Sorteio;
GO

CREATE TABLE Pessoa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Cpf CHAR(11) NOT NULL,
    DataNascimento DATE NOT NULL,
    Renda DECIMAL(18,2) NOT NULL DEFAULT 0
);
GO

CREATE TABLE Familia (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ResponsavelId INT NOT NULL,
    ConjugeId INT NULL,    
);
GO

ALTER TABLE Pessoa
ADD FamiliaId INT NULL,
    CONSTRAINT FK_Pessoa_Familia FOREIGN KEY (FamiliaId) REFERENCES Familia(Id) ON DELETE SET NULL;
GO

ALTER TABLE Familia
ADD CONSTRAINT FK_Familia_Responsavel FOREIGN KEY (ResponsavelId) REFERENCES Pessoa(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Familia_Conjuge FOREIGN KEY (ConjugeId) REFERENCES Pessoa(Id) ON DELETE SET NULL;
GO

INSERT INTO Pessoa (Nome, Cpf, DataNascimento, renda) VALUES
    ('André Furlan', '79863067008', '1991-01-05', 1000)
GO
