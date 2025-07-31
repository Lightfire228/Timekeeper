
use std::{env};

use sea_orm::{Database, DbErr};

pub async fn connect_db(db_url: String) -> Result<(), DbErr> {

    let db_url = format!("sqlite://{db_url}?mode=rwc");

    println!("rust db url: {}", db_url);
    let db = Database::connect(db_url).await?;

    Ok(())
}

pub fn db_url_pc() -> String {
    env::var("DB_URL").expect("Env var 'DB_URL' must be set")
}