use chrono::{DateTime, Utc};


pub struct Event {
    pub id:         usize,
    pub task_id:    usize,
    pub type_:      EventType,
    pub datetime:   DateTime<Utc>,
    pub created_on: DateTime<Utc>,
}

pub enum EventType {
    Completion,
    Due,
    Reminder,
}