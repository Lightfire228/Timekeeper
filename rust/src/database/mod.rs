use std::{env};

use migration::{Migrator, MigratorTrait};
use sea_orm::{Database, DatabaseConnection, DbErr};

pub async fn connect_db(db_url: String) -> Result<DatabaseConnection, DbErr> {

    let db_url = format!("sqlite://{db_url}?mode=rwc");

    // println!("rust db url: {}", db_url);
    let db = Database::connect(db_url).await?;

    migrate_all(&db).await?;

    Ok(db)
}

pub async fn migrate_all(db: &DatabaseConnection) -> Result<(), DbErr> {
    Migrator::up(db, None).await
}

pub fn db_url_pc() -> String {
    env::var("DB_URL").expect("Env var 'DB_URL' must be set")
}