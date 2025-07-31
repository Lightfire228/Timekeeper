pub mod frb_api;
pub mod models;
pub mod database;
mod frb_generated;

use futures::executor::block_on;
use sea_orm::DbErr;
use dotenvy;

// NOTE: the main entry point for the app will be in dart
//       this is just here for ease of development
pub fn main() {

    dotenvy::dotenv().unwrap();

    if let Err(err) = block_on(run()) {
        panic!("{err}")
    }
}

async fn run() -> Result<(), DbErr> {
    let db_url = database::db_url_pc();
    database::connect_db(db_url).await
}