-- Your SQL goes here

CREATE TABLE db_tasks (
    id          INTEGER PRIMARY KEY,
    name        TEXT NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE db_tags (
    id          INTEGER PRIMARY KEY,
    name        TEXT NOT NULL
);

CREATE TABLE db_task_tags (
    task_id INTEGER,
    tag_id  INTEGER,
    PRIMARY KEY (task_id, tag_id),
    FOREIGN KEY (task_id) REFERENCES db_tasks(id),
    FOREIGN KEY (tag_id)  REFERENCES db_tags (id)
);
