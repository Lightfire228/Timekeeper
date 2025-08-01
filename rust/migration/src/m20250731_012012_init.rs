use sea_orm_migration::{prelude::*, schema::*};

#[derive(DeriveMigrationName)]
pub struct Migration;

#[async_trait::async_trait]
impl MigrationTrait for Migration {
    async fn up(&self, manager: &SchemaManager) -> Result<(), DbErr> {

        manager
            .create_table(
                Table::create()
                    .if_not_exists()
                    .table(Res::Table)
                        .col(pk_auto(Res::Id))
                        .col(string (Res::Name))
                        .col(string (Res::Description))
                    .to_owned(),
            )
            .await?
        ;

        manager
            .create_table(
                Table::create()
                    .if_not_exists()
                    .table(Tag::Table)
                        .col(pk_auto(Tag::Id))
                        .col(string (Tag::Name))
                    .to_owned(),
            )
            .await?
        ;

        manager
            .create_table(
                Table::create()
                    .if_not_exists()
                    .table(RT::Table)
                        .col(integer(RT::ResponsibilityId))
                        .col(integer(RT::TagId)           )
                        .primary_key(Index::create()
                            .col(RT::ResponsibilityId)
                            .col(RT::TagId)
                        )
                    .to_owned(),
            )
            .await?
        ;

        Ok(())
    }

    async fn down(&self, manager: &SchemaManager) -> Result<(), DbErr> {
        // Replace the sample below with your own migration scripts

        manager
            .drop_table(Table::drop().table(Responsibility::Table).to_owned())
            .await?
        ;

        manager
            .drop_table(Table::drop().table(Tag           ::Table).to_owned())
            .await?
        ;

        manager
            .drop_table(Table::drop().table(RT            ::Table).to_owned())
            .await?
        ;

        Ok(())
    }
}

#[derive(DeriveIden)]
enum Responsibility {
    Table,
    Id,
    Name,
    Description,
}

#[derive(DeriveIden)]
enum Tag {
    Table,
    Id,
    Name,
}

#[derive(DeriveIden)]
enum ResponsibilityTag {
    Table,
    ResponsibilityId,
    TagId,
}

type RT  = ResponsibilityTag;
type Res = Responsibility;