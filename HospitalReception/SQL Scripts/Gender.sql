CREATE TABLE Genders
(
Id int IDENTITY,
NameLocal varchar(500)  NOT NULL,
NameEng varchar(500)
);

INSERT INTO Genders (NameLocal, NameEng)
VALUES ('�������', 'Male');
INSERT INTO Genders (NameLocal, NameEng)
VALUES ('�������',	'Female');
INSERT INTO Genders (NameLocal, NameEng)
VALUES ('��� ����������',	'Unspecified');