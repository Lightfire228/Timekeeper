// @generated automatically by Diesel CLI.

diesel::table! {
    db_tags (id) {
        id -> Integer,
        name -> Text,
    }
}

diesel::table! {
    db_task_tags (task_id, tag_id) {
        task_id -> Integer,
        tag_id -> Integer,
    }
}

diesel::table! {
    db_tasks (id) {
        id -> Integer,
        name -> Text,
        description -> Text,
        created_at -> Timestamp,
        updated_at -> Nullable<Timestamp>,
        deleted_at -> Nullable<Timestamp>,
    }
}

diesel::joinable!(db_task_tags -> db_tags (tag_id));
diesel::joinable!(db_task_tags -> db_tasks (task_id));

diesel::allow_tables_to_appear_in_same_query!(db_tags, db_task_tags, db_tasks,);
