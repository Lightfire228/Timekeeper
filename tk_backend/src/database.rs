use diesel::{Connection, associations::HasTable, prelude::*};
use diesel_migrations::{EmbeddedMigrations, MigrationHarness, embed_migrations};

pub const MIGRATIONS: EmbeddedMigrations = embed_migrations!();

use std::{env, path::PathBuf};

use crate::models::{NewTask, Task};


pub fn test_db() {

    use crate::schema::db_tasks::dsl::*;

    let mut file = app_data_path();

    file.push("timekeeper.db");

    let mut conn = SqliteConnection::establish(file.to_str().unwrap()).unwrap();

    conn.run_pending_migrations(MIGRATIONS).unwrap();

    let new_task = NewTask {
        name:        "test",
        description: "test desc"
    };

    diesel::insert_into(db_tasks::table())
        .values (&new_task)
        .execute(&mut conn)
        .expect ("unable to insert task")
    ;

    let results = db_tasks
        .select(Task::as_select())
        .load  (&mut conn)
        .expect("unable to load tasks")
    ;

    for task in results {
        dbg!("{}", task);
    }


}


fn app_data_path() -> PathBuf {
    if cfg!(target_os = "android") {
        app_data_path_android()
    }
    else {
        app_data_path_linux()
    }
}

fn app_data_path_android() -> PathBuf {
    // TODO:
    PathBuf::from("/data/data/tk.timekeeper/files/")
}


fn app_data_path_linux() -> PathBuf {
    let config_dir = env::var("XDG_CONFIG").unwrap_or_else(|_|
        format!("{}/.config/", env::var("HOME").expect("unable to get config dir"))
    );

    let mut config_dir = PathBuf::from(config_dir);
    config_dir.push("timekeeper/");                 // TODO: create folder if missing

    dbg!("{}", &config_dir);

    config_dir
}
