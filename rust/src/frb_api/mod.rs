use sea_orm::DbErr;

use crate::database;

#[flutter_rust_bridge::frb(sync)] // Synchronous mode for simplicity of the demo
pub fn greet(name: String) -> String {
    format!("Hello, {name}!")
}

#[flutter_rust_bridge::frb(init)]
pub fn init_app() {
    // Default utilities - feel free to customize
    flutter_rust_bridge::setup_default_user_utils();

}
#[flutter_rust_bridge::frb()]
pub async fn connect_db(db_url: String) -> Result<(), DbErr> {
    database::connect_db(db_url).await?;

    Ok(())
}
