use std::sync::Mutex;

use diesel::SqliteConnection;
use serde::{Deserialize, Serialize};
use tauri::{Manager, State};

use crate::models::Task;

mod database;
pub mod schema;
pub mod models;

pub struct AppStateInner {
    pub conn: SqliteConnection,
}

pub type AppState = Mutex<AppStateInner>;

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .setup(|app| {
            app.manage(Mutex::new(AppStateInner {
                conn: database::get_db_connection(),
            }));
            Ok(())
        })
        .plugin        (tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![greet, test_db, new_task, print_db, get_tasks])
        .run           (tauri::generate_context!())
        .expect        ("error while running tauri application")
    ;

    database::test_db();
}


// Learn more about Tauri commands at https://tauri.app/develop/calling-rust/
#[tauri::command]
fn greet(name: &str) -> String {
    format!("Hello, {}! You've been greeted from Rust!", name)
}

#[tauri::command]
fn test_db() {
    database::test_db();
}

#[tauri::command]
fn new_task(task: NewTaskInput, state: State<'_, AppState>) {
    let mut state = state.lock().unwrap();

    database::new_task(task, &mut state.conn);
}

#[tauri::command]
fn print_db(state: State<'_, AppState>) {
    let mut state = state.lock().unwrap();

    database::print_tasks(&mut state.conn);
}

#[tauri::command]
fn get_tasks(state: State<'_, AppState>) -> Vec<TaskOutput> {
    let mut state = state.lock().unwrap();

    database::get_all_tasks(&mut state.conn)
        .unwrap()
        .into_iter()
        .map(|t| TaskOutput {
            id:          t.id as f64,
            name:        t.name,
            description: t.description,
        })
        .collect()

}

#[derive(Deserialize, Serialize)]
pub struct NewTaskInput {
    pub name:        String,
    pub description: String,
}

#[derive(Deserialize, Serialize)]
pub struct TaskOutput {
    pub id:          f64,
    pub name:        String,
    pub description: String,
}
