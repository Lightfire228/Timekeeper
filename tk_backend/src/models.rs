
use crate::schema::db_tasks;
use diesel::prelude::*;
use serde::Serialize;


#[derive(Queryable, Selectable, Debug, Serialize)]
#[diesel(table_name = crate::schema::db_tasks)]
pub struct Task {
    pub id:          i64,
    pub name:        String,
    pub description: String,
}

#[derive(Insertable)]
#[diesel(table_name = db_tasks)]
pub struct NewTask<'a> {
    pub name:        &'a str,
    pub description: &'a str,
}

#[derive(Queryable, Selectable, Debug, Serialize)]
#[diesel(table_name = crate::schema::db_tags)]
pub struct Tag {
    pub id:          i64,
    pub name:        String,
}

#[derive(Queryable, Selectable, Debug, Serialize)]
#[diesel(table_name = crate::schema::db_task_tags)]
pub struct TaskTag {
    pub task_id:          i64,
    pub tag_id:           i64,
}
