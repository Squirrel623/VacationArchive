CREATE DATABASE vacation_archive;
USE vacation_archive;

CREATE TABLE user
(
id INTEGER NOT NULL AUTO_INCREMENT,
name TEXT,
PRIMARY KEY(id)
);

INSERT INTO user (id, name) VALUES
  (1, 'brady');


CREATE TABLE vacation
(
id INTEGER NOT NULL AUTO_INCREMENT,
created_by INTEGER NOT NULL,
start_date DATE NOT NULL,
end_date DATE,

PRIMARY KEY(id),
FOREIGN KEY(created_by)
  REFERENCES user(id)
);

INSERT INTO vacation(id, created_by, start_date, end_date) VALUES
  (1, 1, '2020-01-01', '2020-01-05');

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