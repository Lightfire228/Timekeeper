-- Your SQL goes here

CREATE TABLE db_tasks (
    id          INTEGER NOT NULL PRIMARY KEY,
    name        TEXT    NOT NULL,
    description TEXT    NOT NULL,

    created_at  DATETIME NOT NULL,
    updated_at  DATETIME,
    deleted_at  DATETIME
);

CREATE TABLE db_tags (
    id          INTEGER NOT NULL PRIMARY KEY,
    name        TEXT    NOT NULL
);

CREATE TABLE db_task_tags (
    task_id INTEGER NOT NULL,
    tag_id  INTEGER NOT NULL,
    PRIMARY KEY (task_id, tag_id),
    FOREIGN KEY (task_id) REFERENCES db_tasks(id),
    FOREIGN KEY (tag_id)  REFERENCES db_tags (id)
);
