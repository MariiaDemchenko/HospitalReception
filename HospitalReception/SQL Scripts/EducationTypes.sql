CREATE TABLE EducationTypes 
(
Id int IDENTITY,
NameLocal varchar(500)  NOT NULL,
NameEng varchar(500) 
);

INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('Среднее', null);
INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('Среднее-специальное', null);
INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('Высшее', null);
INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('Нет информации',	'Unspecified');