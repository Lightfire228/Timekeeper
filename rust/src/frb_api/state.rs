use std::collections::HashSet;

use flutter_rust_bridge::frb;

use crate::models::task::Task;


#[frb(ui_state)]
pub struct RustState {
    pub input_text:    String,
    pub filter:        Filter,
    pub current_word:  String,

    tasks:          Vec<Task>,
    words:          HashSet<String>,
    next_id:        usize,
}


#[derive(Debug, Clone, Copy, PartialEq, Eq)]
pub enum Filter {
    All,
    Active,
    Completed,
}


#[frb(ui_mutation)]
impl RustState {
    pub fn add(&mut self) {

        let id        = self.next_id;
        self.next_id += 1;

        self.tasks.push(Task::new(
            id, 
            self.input_text.clone(), 
            "".to_string(),
        ));

        self.input_text.clear();
    }

    pub fn set_word(&mut self, word_pair: String) {
        self.current_word  = word_pair;
    }

    pub fn toggle_favourite_word(&mut self) {

        if self.words.contains(&self.current_word) {
            self.words.remove(&self.current_word);
        }
        else {
            self.words.insert(self.current_word.clone());
        }
    }

    pub fn remove(&mut self, id: usize) {
        self.tasks.retain(|x| x.id != id);
    }

    pub fn toggle(&mut self, id: usize) {
        let entry = self.tasks
            .iter_mut()
            .find    (|x| x.id == id)
            .unwrap  ()
        ;

        entry.is_active ^= true;
    }
}


impl RustState {
    #[frb(sync)]
    pub fn new() -> Self {
        Self {
            tasks:        vec![],
            words:        HashSet::new(),
            input_text:   "".to_string(),
            current_word: "".to_string(),
            filter:       Filter::All,
            next_id:      0,
            base_state:   Default::default(),
        }
    }

    #[frb(sync)]
    pub fn filtered_items(&self) -> Vec<Task> {
        self.tasks
            .iter   ()
            .filter (|x| self.filter.check(x))
            .cloned ()
            .collect()
    }

    #[frb(sync)]
    pub fn is_favourited(&self) -> bool {
        self.words.contains(&self.current_word)
    }
}

impl Filter {

    fn check(&self, task: &Task) -> bool {

        match self {
            Self::All       => true,
            Self::Active    =>  task.is_active,
            Self::Completed => !task.is_active,
        }
    }
}