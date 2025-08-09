
#[derive(Clone)]
pub struct Task {
    pub id:          usize,
    pub name:        String,
    pub description: String,
    pub is_active:   bool,
}

impl Task {
    pub fn new(id: usize, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
            is_active:   true,
        }
    }
}