mod database;
pub mod schema;
pub mod models;

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .plugin        (tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![greet, test_db])
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
#[allow(unused)]
fn test_db() {
    database::test_db();
}
