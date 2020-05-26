CREATE TABLE Genders
(
Id int IDENTITY,
NameLocal varchar(500)  NOT NULL,
NameEng varchar(500)
);

INSERT INTO Genders (NameLocal, NameEng)
VALUES ('Мужской', 'Male');
INSERT INTO Genders (NameLocal, NameEng)
VALUES ('Женский',	'Female');
INSERT INTO Genders (NameLocal, NameEng)
VALUES ('Нет информации',	'Unspecified');