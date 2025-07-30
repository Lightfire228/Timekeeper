
pub struct Responsibility {
    pub id:          i64,
    pub name:        String,
    pub description: String,
}

pub struct Tag {
    pub id:   i64,
    pub name: String,
}

pub struct ResponsibilityTag {
    pub responsibility_id: i64,
    pub tag_id:            i64,
}
