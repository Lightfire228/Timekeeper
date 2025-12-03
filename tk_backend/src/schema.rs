
use diesel;

diesel::table! {
    db_tasks (id) {
        id          -> Int8,
        name        -> VarChar,
        description -> VarChar,
    }
}

diesel::table! {
    db_tags (id) {
        id   -> Int8,
        name -> VarChar,
    }
}

diesel::table! {
    db_task_tags (task_id, tag_id) {
        task_id -> Int8,
        tag_id  -> Int8,
    }
}
