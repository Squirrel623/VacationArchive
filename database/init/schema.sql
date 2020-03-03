CREATE DATABASE vacation_archive;
USE vacation_archive;

CREATE TABLE user
(
id INTEGER NOT NULL AUTO_INCREMENT,
first_name TEXT NOT NULL,
last_name TEXT NOT NULL,
email TEXT NOT NULL,

PRIMARY KEY(id)
);

INSERT INTO user (id, first_name, last_name, email) VALUES
  (1, 'brady', 'hutchins', 'squirrel623@yahoo.com');


CREATE TABLE vacation
(
id INTEGER NOT NULL AUTO_INCREMENT,
created_by INTEGER NOT NULL,
title TEXT NOT NULL,
start_date DATE NOT NULL,
end_date DATE,

PRIMARY KEY(id),
FOREIGN KEY(created_by)
  REFERENCES user(id)
);

INSERT INTO vacation(id, created_by, title, start_date, end_date) VALUES
  (1, 1, 'My First Vacation', '2020-01-01', '2020-01-05');

CREATE TABLE vacation_activity
(
id INTEGER NOT NULL AUTO_INCREMENT,
vacation_id INTEGER NOT NULL,
title TEXT NOT NULL,
date DATE NOT NULL,

PRIMARY KEY(id),
FOREIGN KEY(vacation_id)
  REFERENCES vacation(id)
);

INSERT INTO vacation_activity(id, vacation_id, title, date) VALUES
  (1, 1, 'My first activity', '2020-01-02');