mod backup;

use chrono::{Local};
use diesel::{Connection, associations::HasTable, prelude::*};
use diesel_migrations::{EmbeddedMigrations, MigrationHarness, embed_migrations};

pub const MIGRATIONS: EmbeddedMigrations = embed_migrations!();

use crate::{NewTaskInput, models::{NewTask, Task, TaskId}, platform};

type Conn = SqliteConnection;

use crate::schema::db_tasks::dsl::*;

pub use backup::*;

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

    let now = Local::now().naive_local();

    if count < 1 {
        diesel::insert_into(db_tasks::table())
            .values (vec![
                NewTask { name: "test task 1", description: "test desc", created_at: now },
                NewTask { name: "test task 2", description: "test desc", created_at: now },
                NewTask { name: "test task 3", description: "test desc", created_at: now },
                NewTask { name: "test task 4", description: "test desc", created_at: now },
            ])
            .execute(conn)
            .expect ("unable to insert test tasks")
        ;
    }

}

pub async fn new_task(task: NewTaskInput, conn: &mut Conn) {
    let new_task = NewTask {
        name:        &task.name,
        description: &task.description,
        created_at:  Local::now().naive_local(),
    };


    diesel::insert_into(db_tasks::table())
        .values (&new_task)
        .execute(conn)
        .expect ("unable to insert task")
    ;
}

pub async fn delete_task(task_id: TaskId, conn: &mut Conn) {

    let date = Local::now().naive_local();

    diesel::update(db_tasks)
        .filter (id        .eq(task_id))
        .set    (deleted_at.eq(date))
        // .set    (name.eq("test"))
        .execute(conn)
        .expect ("unable to delete task")
    ;
}


pub async fn print_tasks(conn: &mut Conn) {

    let results = get_all_tasks(conn).await.expect("unable to load tasks");

    for task in results {
        dbg!("{}", task);
    }
}

pub async fn get_all_tasks(conn: &mut Conn) -> QueryResult<Vec<Task>> {

     db_tasks
        .select(Task::as_select())
        .filter(deleted_at.is_null())
        .load  (conn)
}
