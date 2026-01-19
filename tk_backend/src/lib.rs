use diesel::SqliteConnection;
use serde::{Deserialize, Serialize};
use tauri::{App, Manager, State};

use tokio::sync::Mutex;

mod database;
mod platform;

pub mod schema;
pub mod models;

pub struct AppStateInner {
    pub conn: SqliteConnection,
}

pub type AppState = Mutex<AppStateInner>;

type Void = Result<(), ()>;

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()

        .setup(|app| {
            platform::init_files();

            let mut conn = database::get_db_connection();

            database::init_db(&mut conn);

            app.manage(Mutex::new(AppStateInner {
                conn,
            }));
            Ok(())
        })

        .plugin        (tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![
            greet,
            new_task,
            delete_task,
            print_db,
            get_tasks,
            backup_db,
        ])
        .run           (tauri::generate_context!())
        .expect        ("error while running tauri application")
    ;
}


// Learn more about Tauri commands at https://tauri.app/develop/calling-rust/
#[tauri::command]
fn greet(name: &str) -> String {
    format!("Hello, {}! You've been greeted from Rust!", name)
}

#[tauri::command]
async fn new_task(task: NewTaskInput, state: State<'_, AppState>) -> Void {
    let mut state = state.lock().await;

    database::new_task(task, &mut state.conn).await;

    Ok(())
}

// TODO: soft delete
#[tauri::command]
async fn delete_task(task_id: i64, state: State<'_, AppState>) -> Void {
    let mut state = state.lock().await;

    database::delete_task(task_id, &mut state.conn).await;

    Ok(())
}

#[tauri::command]
async fn print_db(state: State<'_, AppState>) -> Void {
    let mut state = state.lock().await;

    database::print_tasks(&mut state.conn).await;

    Ok(())
}

#[tauri::command]
async fn get_tasks(state: State<'_, AppState>) -> Result<Vec<TaskOutput>, ()> {
    let mut state = state.lock().await;

    Ok(database::get_all_tasks(&mut state.conn)
        .await
        .unwrap()
        .into_iter()
        .map(|t| TaskOutput {
            id:          t.id as f64,
            name:        t.name,
            description: t.description,
        })
        .collect()
    )

}

#[tauri::command]
async fn backup_db(state: State<'_, AppState>) -> Void {
    let mut state = state.lock().await;

    database::backup_db(&mut state.conn).await;

    Ok(())
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
