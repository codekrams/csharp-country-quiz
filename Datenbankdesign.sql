//Datenbankdesign, Martina Schwerdtfeger

create database laenderquizms;

use laenderquizms;

create table land(
	landid int primary key auto_increment not null,
	landname varchar(50),
	capital varchar(50)
);

create table quizuser(
	userid int primary key auto_increment not null,
	username varchar(50),
	passwort varchar(50),
	highscore int
);

insert into quizuser
values(NULL, "martina", "bcde", 20),
	  (NULL, "sarah", "cdef", 18);
	  
	  
insert into land
values (NULL, "Deutschland", "Berlin"),
	   (NULL, "Frankreich", "Paris"),
	   (NULL, "England", "London"),
	   (NULL, "Bulgarien", "Sofia"),
	   (NULL, "Italien", "Rom"),
	   (NULL, "Belgien", "Brüssel"),
	   (NULL, "Tschechien", "Prag"),
	   (NULL, "Oesterreich", "Wien"),
	   (NULL, "Spanien", "Madrid"),
	   (NULL, "Portugal", "Lissabon"),
	   (NULL, "Schweden", "Stockholm"),
	   (NULL, "Norwegen", "Oslo"),
	   (NULL, "Finnland", "Helsinki");
	  
CREATE USER 'quizuser' IDENTIFIED BY 'quizuser';
GRANT SELECT ON laenderquizms.land TO 'quizuser';
GRANT ALL ON laenderquizms.quizuser TO 'quizuser';