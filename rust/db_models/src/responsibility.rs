use sea_orm::entity::prelude::*;

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel)]
#[sea_orm(table_name = "responsibility")]
pub struct Model {
    
    #[sea_orm(primary_key)]
    pub id:          i64,
    
    pub name:        String,
    pub description: String,
}


#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::tag::Entity")]
    Tags,
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        super::responsibility_tag::Relation::Tag.def()
    }

    fn via() -> Option<RelationDef> {
        Some(super::responsibility_tag::Relation::Responsibility.def().rev())
    }
}

impl ActiveModelBehavior for ActiveModel {}