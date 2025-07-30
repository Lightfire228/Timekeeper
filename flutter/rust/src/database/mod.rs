
use sea_orm::{Database, DbErr};



#[flutter_rust_bridge::frb()]
async fn connect_db() -> Result<(), DbErr> {
    let db = Database::connect("sqlite://home/cass/Code/Personal/timekeeper/timekeeper_rust_flutter/timekeeper.db").await?;

    Ok(())
}
