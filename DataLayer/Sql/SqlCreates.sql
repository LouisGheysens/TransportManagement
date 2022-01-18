CREATE TABLE Chauffeur(
Personeelsnummer NVARCHAR(50) NOT NULL PRIMARY KEY,
Voornaam NVARCHAR(200) NOT NULL,
Achternaam NVARCHAR(350) NOT NULL,
Geboortedatum DATE NOT NULL,
Internationaal BIT NOT NULL,
Vrachtwagenchassisnummer NVARCHAR(17) NULL
);


CREATE TABLE Vrachtwagen(
Chassisnummer NVARCHAR(17) NOT NULL PRIMARY KEY,
Merk NVARCHAR(100) NOT NULL,
Model NVARCHAR(100) NOT NULL,
Gewicht FLOAT NOT NULL,
Aanhangwagen BIT NOT NULL,
Brandstof NVARCHAR(1000),
Chauffeur NVARCHAR(50) NULL
);

CREATE TABLE Ongeval(
Vrachtwagen NVARCHAR(100) NOT NULL,
Chauffeur NVARCHAR(100) NOT NULL,
Arbeidsongeval BIT NOT NULL,
Plaats NVARCHAR(350),
Datum Date NOT NULL,
Pertetotal BIT NOT NULL,
Graad NVARCHAR(100),
PRIMARY KEY (Vrachtwagen,Chauffeur)
);

ALTER TABLE Chauffeur ADD CONSTRAINT FK_Vrachtwagen_Chassisnummer FOREIGN KEY (Vrachtwagenchassisnummer) REFERENCES Vrachtwagen(Chassisnummer);
ALTER TABLE Vrachtwagen ADD CONSTRAINT FK_Vrachtwagen_Chauffeur FOREIGN KEY (Chauffeur) REFERENCES Chauffeur(Personeelsnummer);