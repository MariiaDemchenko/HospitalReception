CREATE TABLE EmploymentTypes 
(
Id int IDENTITY,
NameLocal varchar(500)  NOT NULL,
NameEng varchar(500)
);

INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('Учащийся', null);
INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('Не работающий', null);
INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('Работающий', null);
INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('Нет информации',	'Unspecified');