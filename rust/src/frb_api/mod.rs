use flutter_rust_bridge::{frb, setup_default_user_utils};
use sea_orm::{DbErr};

use crate::database;

pub mod state;

#[frb(sync)] // Synchronous mode for simplicity of the demo
pub fn greet(name: String) -> String {
    format!("Hello, {name}!")
}

#[frb(init)]
pub fn init_app() {
    // Default utilities - feel free to customize
    setup_default_user_utils();

}

#[frb()]
pub async fn connect_db(db_url: String) -> Result<(), DbErr> {
    let _db = database::connect_db(db_url).await?;

    Ok(())
}
