CREATE TABLE EducationTypes 
(
Id int IDENTITY,
NameLocal varchar(500)  NOT NULL,
NameEng varchar(500) 
);

INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('�������', null);
INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('�������-�����������', null);
INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('������', null);
INSERT INTO EducationTypes (NameLocal, NameEng)
VALUES ('��� ����������',	'Unspecified');