use std::path::Path;
use std::fs;

use diesel::{QueryDsl, RunQueryDsl, SelectableHelper};
use serde::Serialize;

use crate::{database::Conn, models::Task};
use crate::schema::db_tasks::dsl::*;
use chrono::{Local, NaiveDateTime, format};

const SCHEMA_VER: &str = "0.0.0";

const FILE_NAME:  &str = "timekeeper_data.json";

#[derive(Serialize, Debug)]
struct DbBackup {
    schema_ver: &'static str,
    tasks:      Vec<Task>,
}


pub async fn backup_db(conn: &mut Conn) {

    let backup   = get_backup(conn).await;
    let json     = serde_json::to_string(&backup).unwrap();
    let filename = get_filename().await;


    fs::write(&filename, json.as_bytes()).unwrap();
}

async fn get_filename() -> String {
    let date = Local::now();
    let date = date.format("%Y-%m-%d_%H-%M-%S").to_string();

    // TODO: not this
    let dir  = Path::new("../").canonicalize().unwrap();
    let path = format!("{}/exclude/{date}_{FILE_NAME}", dir.to_str().unwrap());

    path
}

async fn get_backup(conn: &mut Conn) -> DbBackup {
    DbBackup {
        schema_ver: SCHEMA_VER,
        tasks:      get_tasks(conn).await,
    }
}

async fn get_tasks(conn: &mut Conn) -> Vec<Task> {
    db_tasks
        .select(Task::as_select())
        .load  (conn)
        .unwrap()
}
