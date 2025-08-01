use sea_orm::entity::prelude::*;

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel)]
#[sea_orm(table_name = "responsibility_tag")]
pub struct Model {

    #[sea_orm(primary_key)]
    pub responsibility_id: i64,

    #[sea_orm(primary_key)]
    pub tag_id:            i64,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {

    #[sea_orm(
        belongs_to = "super::tag::Entity",
        from       = "Column::TagId",
        to         = "super::tag::Column::Id",
    )]
    Tag,
    
    
    #[sea_orm(
        belongs_to = "super::responsibility::Entity",
        from       = "Column::TagId",
        to         = "super::responsibility::Column::Id",
    )]
    Responsibility,
}

impl ActiveModelBehavior for ActiveModel {}