
use crate::schema::db_tasks;
use chrono::{NaiveDateTime};
use diesel::prelude::*;
use serde::Serialize;

pub type TaskId = i32;
pub type TagId  = i32;


#[derive(Queryable, Selectable, Debug, Serialize)]
#[diesel(table_name = crate::schema::db_tasks)]
pub struct Task {
    pub id:          TaskId,
    pub name:        String,
    pub description: String,

    pub created_at:  NaiveDateTime,
    pub updated_at:  Option<NaiveDateTime>,
    pub deleted_at:  Option<NaiveDateTime>,
}

#[derive(Insertable)]
#[diesel(table_name = db_tasks)]
pub struct NewTask<'a> {
    pub name:        &'a str,
    pub description: &'a str,

    pub created_at:  NaiveDateTime,
}

#[derive(Queryable, Selectable, Debug, Serialize)]
#[diesel(table_name = crate::schema::db_tags)]
pub struct Tag {
    pub id:          TagId,
    pub name:        String,
}

#[derive(Queryable, Selectable, Debug, Serialize)]
#[diesel(table_name = crate::schema::db_task_tags)]
pub struct TaskTag {
    pub task_id:          TaskId,
    pub tag_id:           TagId,
}
