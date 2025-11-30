use rusqlite::Connection;
use std::{env, path::PathBuf};


#[derive(Debug)]
pub struct Task {
    pub id:          usize,
    pub name:        String,
    pub description: String,
}

#[derive(Debug)]
pub struct Tag {
    pub id:   usize,
    pub name: String,
}


#[derive(Debug)]
pub struct TaskTags {
    pub task_id: usize,
    pub tag_id:  usize,
}


pub fn test_db() {

    // TODO
    let mut file = PathBuf::from(env::current_dir().unwrap());

    file.push("timekeeper.db");


    let conn = Connection::open(file).unwrap();

    create_table_if_exists(&conn,
        "Task",
        "CREATE TABLE Task (
            id          INTEGER PRIMARY KEY,
            name        TEXT NOT NULL,
            description TEXT NOT NULL
        );"
    );

    create_table_if_exists(&conn,
        "Tag",
        "CREATE TABLE Tag (
            id          INTEGER PRIMARY KEY,
            name        TEXT NOT NULL
        );"
    );

    create_table_if_exists(&conn,
        "TaskTags",
        "CREATE TABLE TaskTags (
            task_id INTEGER,
            tag_id  INTEGER,
            PRIMARY KEY (task_id, tag_id),
            FOREIGN KEY (task_id) REFERENCES Task(id),
            FOREIGN KEY (tag_id)  REFERENCES Tag (id)
        );"
    );

}

fn create_table_if_exists(conn: &Connection, table: &str, sql: &str) {
    if conn.table_exists(None, table).unwrap() {
        return;
    }

    conn.execute(sql, ()).unwrap();

}
