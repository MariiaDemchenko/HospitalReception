CREATE TABLE EmploymentTypes 
(
Id int IDENTITY,
NameLocal varchar(500)  NOT NULL,
NameEng varchar(500)
);

INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('��������', null);
INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('�� ����������', null);
INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('����������', null);
INSERT INTO EmploymentTypes (NameLocal, NameEng)
VALUES ('��� ����������',	'Unspecified');