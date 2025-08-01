pub use sea_orm_migration::prelude::*;

mod m20250731_012012_init;

pub struct Migrator;

#[async_trait::async_trait]
impl MigratorTrait for Migrator {

    // Override the name of migration table
    fn migration_table_name() -> sea_orm::DynIden {
        Alias::new("__seaql_migrations").into_iden()
    }

    fn migrations() -> Vec<Box<dyn MigrationTrait>> {
        vec![
            Box::new(m20250731_012012_init::Migration),
        ]
    }
}
