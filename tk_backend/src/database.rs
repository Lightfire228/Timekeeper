use std::{fs, path};

use diesel::{Connection, associations::HasTable, prelude::*};
use diesel_migrations::{EmbeddedMigrations, MigrationHarness, embed_migrations};

pub const MIGRATIONS: EmbeddedMigrations = embed_migrations!();

use crate::{NewTaskInput, models::{NewTask, Task}, platform};

type Conn = SqliteConnection;

use crate::schema::db_tasks::dsl::*;

pub fn get_db_connection() -> Conn {
    let mut file = platform::get_app_data_dir();

    file.push("timekeeper.db");

    let file_name = file.to_str().unwrap();

    Conn::establish(file_name).unwrap_or_else(|err| panic!("Unable to open db file: '{}': {}", file_name, err))
}

pub fn init_db(conn: &mut Conn) {
    conn.run_pending_migrations(MIGRATIONS).unwrap();

    let count: i64 = db_tasks
        .count()
        .get_result(conn)
        .expect("unable to count tasks")
    ;

    if count < 1 {
        diesel::insert_into(db_tasks::table())
            .values (vec![
                NewTask { name: "test task 1", description: "test desc", },
                NewTask { name: "test task 2", description: "test desc", },
                NewTask { name: "test task 3", description: "test desc", },
                NewTask { name: "test task 4", description: "test desc", },
            ])
            .execute(conn)
            .expect ("unable to insert test tasks")
        ;
    }
    
}

pub fn new_task(task: NewTaskInput, conn: &mut Conn) {
    let new_task = NewTask {
        name:        &task.name,
        description: &task.description,
    };


    diesel::insert_into(db_tasks::table())
        .values (&new_task)
        .execute(conn)
        .expect ("unable to insert task")
    ;
}


pub fn print_tasks(conn: &mut Conn) {

    let results = get_all_tasks(conn).expect("unable to load tasks");

    for task in results {
        dbg!("{}", task);
    }
}

pub fn get_all_tasks(conn: &mut Conn) -> QueryResult<Vec<Task>> {

     db_tasks
        .select(Task::as_select())
        .load  (conn)
}
